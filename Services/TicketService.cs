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
    
    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        return await _context.Tickets.ToListAsync();
    }
    
    public async Task<Ticket> GetTicketByIdAsync(int id)
    {
        return await _context.Tickets.FindAsync(id);
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