using Microsoft.EntityFrameworkCore;
using parc.Models;

namespace parc.Services;

public class TicketService
{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Ticket> Add(Ticket ticket, CustomUser user)
    {
        ticket.CreatedDated = DateTime.UtcNow;
        ticket.UpdatedDated = DateTime.UtcNow;
        ticket.UserId = user.Id;
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }
    
    public List<Ticket> GetAllTicketsAsync()
    {
        return  _context.Tickets.ToList();
    }
    
    public async Task<Ticket> GetTicketByIdAsync(int id)
    {
        return await _context.Tickets.FindAsync(id);
    }
    
    public  List<Ticket> GetTicketByDeviceIdAsync(int deviceId)
    {
        return  _context.Tickets.Where(ticket => ticket.DeviceId == deviceId).ToList();
        //return await _context.Tickets.Where(ticket => ticket.DeviceId == deviceId).ToListAsync();
    }
    
    public async Task<Ticket> UpdateTicketAsync(int id, Ticket ticket)
    {
        var existingTicket = await _context.Tickets.FindAsync(id);
        if (existingTicket == null) return null;

        existingTicket.Title = ticket.Title;
        existingTicket.Description = ticket.Description;
        existingTicket.Priority = ticket.Priority;
        existingTicket.UpdatedDated = DateTime.UtcNow;

        _context.Tickets.Update(existingTicket);
        await _context.SaveChangesAsync();
        return existingTicket;
    }
    
    public async Task<bool> DeleteTicketAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null) return false;

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
        return true;
    }

}