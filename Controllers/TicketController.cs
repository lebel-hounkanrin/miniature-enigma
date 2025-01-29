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
    public ActionResult<Ticket> Post(Ticket ticket)
    {
        try
        {
            CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
            return ticketService.Add(ticket, currentUser);
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
        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Salle))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Ticket> Get(int id)
    {
        return Ok();
    }

    
}