using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using parc.Helpers;
using parc.Models;
using parc.Services;

namespace parc.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(requiredRole: "ParcAdmin")]
public class TicketController: ControllerBase
{
    private readonly TicketService ticketService;

    public TicketController(TicketService ticketService)
    {
        this.ticketService = ticketService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Ticket))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Ticket>> Post(Ticket ticket)
    {
        try
        {
            CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
            var createdTicket = await ticketService.Add(ticket, currentUser);
            return Ok(createdTicket);
        }
        catch (UnauthorizedAccessException)
        {
            return StatusCode(403, new { message = "You are not authorized to add salles to this parc." });

            // return Forbid("You are not authorized to add salles to this parc.");
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"An error occurred while trying to create salle", error = e.Message });
        }
    }

    [HttpGet(Name = "Get all tickets")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Ticket>))]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<List<Ticket>> Get([FromQuery] int? deviceId)
    {
        if (deviceId.HasValue)
        {
            return Ok(ticketService.GetTicketByDeviceIdAsync(deviceId.Value));
        }
        
        return Ok(ticketService.GetAllTicketsAsync());
    }

    // [HttpGet("by-device", Name = "Get ticket by device id")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Ticket>))]
    // [Produces(MediaTypeNames.Application.Json)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public ActionResult<List<Ticket>> GetByDeviceId([FromQuery] int deviceId)
    // {
    //     return Ok(ticketService.GetTicketByDeviceIdAsync(deviceId));
    // }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        try
        {
            CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
            var success = await ticketService.DeleteTicketAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"An error occurred while trying to delete ticket", error = e.Message });
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicket(int id, [FromBody] Ticket ticket)
    {
        if (ticket == null || id != ticket.Id)
        {
            return BadRequest();
        }

        var updatedTicket = await ticketService.UpdateTicketAsync(id, ticket);
        if (updatedTicket == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}