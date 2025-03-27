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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Parc>))]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<List<Salle>> Get()
    {
        return Ok(ticketService.GetAllTicketsAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Salle))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Ticket>> Get(int id)
    {
        var ticket = await ticketService.GetTicketByIdAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<bool> Delete(int id)
    {
        try
        {
            CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
            return true;
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
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var success = await ticketService.DeleteTicketAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    
}