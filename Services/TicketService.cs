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

}