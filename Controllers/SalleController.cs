using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using parc.Models;
using parc.Services;

namespace parc.Controllers;

[ApiController]
[Route("[controller]")]
public class SalleController
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
}