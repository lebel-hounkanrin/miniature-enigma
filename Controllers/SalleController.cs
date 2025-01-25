using System.Net.Mime;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using parc.Helpers;
using parc.Models;
using parc.Services;

namespace parc.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(requiredRole: "ParcAdmin")]

public class SalleController: ControllerBase
{
    private readonly ILogger<SalleController> _logger;
    private readonly SalleService _salleService;

    public SalleController(ILogger<SalleController> logger, SalleService salleService)
    {
        _logger = logger;
        _salleService = salleService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Salle))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ActionResult<Salle> Post(Salle salle)
    {
        try
        {
            CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
            return _salleService.Add(salle, currentUser);
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
    
    [HttpGet(Name = "Get all salles")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Parc>))]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<List<Salle>> Get([FromQuery] int? parcId)
    {
        // _logger.LogInformation("Get all parcs for current user");
        try
        {
            CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
            if (parcId.HasValue)
            {
                return Ok(_salleService.GetAllForParc(currentUser, parcId.Value));

            }
            else
            {
                return Ok(_salleService.GetAll(currentUser));
            }
            
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"An error occurred while trying to get salles", error = e.Message });
        }
    }
    
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Salle))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Salle> GetSingle(int id)
    {
        CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
        return _salleService.GetById(currentUser, id);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Salle))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(int id, Salle _salle)
    {
        CustomUser currentUser = HttpContext.Items["User"] as CustomUser;

        var salle = _salleService.GetById(currentUser, id);
        if (salle == null)
        {
            return NotFound(new { message = "can not found with this id." });
        }
        await _salleService.Update(id, _salle);
        return Ok(new { message = $" Todo Item  with id {id} successfully updated" });
    }
}