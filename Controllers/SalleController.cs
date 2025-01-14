using System.Net.Mime;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using parc.Helpers;
using parc.Models;
using parc.Services;

namespace parc.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(requiredRole: "ParcAdmin")]

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
    public ActionResult<Salle> Post(Salle salle)
    {
        return _salleService.Add(salle);
    }
    
    [HttpGet(Name = "Get all salles")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Parc>))]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<List<Salle>> Get()
    {
        // _logger.LogInformation("Get all parcs for current user");
        return _salleService.GetAll();
    }
    
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Salle))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Salle> Get(int id)
    {
        // CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
        return _salleService.GetById(id);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Salle))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(int id, Salle _salle)
    {
        var salle = _salleService.GetById(id);
        if (salle == null)
        {
            return NotFound(new { message = "can not found with this id." });
        }
        await _salleService.Update(id, _salle);
        return Ok(new { message = $" Todo Item  with id {id} successfully updated" });
    }
}