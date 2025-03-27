using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using parc.Helpers;
using parc.Models;
using parc.Models.shared;

namespace parc.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(requiredRole: "ParcAdmin")]
public class ReservationsController: ControllerBase
{
    private readonly AppDbContext _context;

    public ReservationsController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Reservation))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Reservation>> CreateReservation(Reservation reservation)
    {
        if (reservation.StartTime >= reservation.EndTime)
        {
            return BadRequest("L'heure de début doit être avant l'heure de fin.");
        }

        if (reservation.Date < DateTime.Now.Date || 
            (reservation.Date == DateTime.Now.Date && reservation.StartTime < DateTime.Now.TimeOfDay))
        {
            return BadRequest("La réservation ne peut pas être effectuée dans le passé.");
        }

        if (reservation.IsRoomReservation)
        {
            var conflictingRoomReservation = await _context.Reservations
                .Where(r => r.RoomId == reservation.RoomId && r.Date == reservation.Date)
                .Where(r => (reservation.StartTime < r.EndTime && reservation.EndTime > r.StartTime)) 
                .FirstOrDefaultAsync();
            if (conflictingRoomReservation != null)
            {
                return Conflict("La salle est déjà réservée à cette date et heure.");
            }
            var room = await _context.Salles.Include(r => r.Devices).FirstOrDefaultAsync(r => r.Id == reservation.RoomId);
            if (room == null)
            {
                return NotFound("La salle spécifiée n'existe pas.");
            }
            
            foreach (var device in room.Devices)
            {
                var conflictingDeviceReservation = await _context.Reservations
                    .Where(r => r.DeviceId == device.Id && r.Date == reservation.Date)
                    .Where(r => (reservation.StartTime < r.EndTime && reservation.EndTime > r.StartTime))
                    .FirstOrDefaultAsync();

                if (conflictingDeviceReservation != null)
                {
                    return Conflict($"Le périphérique '{device.Name}' est déjà réservé à cette date et heure.");
                }
            }
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            foreach (var device in room.Devices)
            {
                device.Status = DeviceStatus.Reserved.GetDisplayName();
            }
            await _context.SaveChangesAsync(); 
            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }
        else
        {
            var conflictingDeviceReservation = await _context.Reservations
                .Where(r => r.DeviceId == reservation.DeviceId && r.Date == reservation.Date)
                .Where(r => (reservation.StartTime < r.EndTime && reservation.EndTime > r.StartTime)) 
                .FirstOrDefaultAsync();
            if (conflictingDeviceReservation != null)
            {
                return Conflict("Le périphérique est déjà réservé à cette date et heure.");
            }
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            var device = await _context.DeviceGenralInfos.FindAsync(reservation.DeviceId);
            if (device != null)
            {
                device.Status = DeviceStatus.Reserved.GetDisplayName();
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }
    }
    
    
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Reservation))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Reservation>> GetReservation(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);

        if (reservation == null)
        {
            return NotFound();
        }

        return reservation;
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
        {
            return NotFound();
        }
        if (reservation.IsRoomReservation)
        {
            var room = await _context.Salles.Include(r => r.Devices).FirstOrDefaultAsync(r => r.Id == reservation.RoomId);
            if (room != null)
            {
                foreach (var d in room.Devices)
                {
                    d.Status = DeviceStatus.Active.GetDisplayName();
                }

                await _context.SaveChangesAsync();
            }
        }
        else
        {
            var device = await _context.DeviceGenralInfos.FindAsync(reservation.DeviceId);
            if (device != null)
            {
                device.Status = DeviceStatus.Active.GetDisplayName();
                await _context.SaveChangesAsync();
            }
        }
        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool ReservationExists(int id)
    {
        return _context.Reservations.Any(e => e.Id == id);
    }
}