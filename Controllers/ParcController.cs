using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using parc.Helpers;
using parc.Models;
using parc.Services;

namespace parc.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(requiredRole: "ParcAdmin")]
public class ParcController: ControllerBase
{
    private readonly ILogger<ParcController> _logger;
    private readonly ParcService _parcService;

    public ParcController(ParcService parcService)
    {
        // _logger = logger;
        _parcService = parcService;
    }

    [HttpGet(Name = "Get all parcs for current user")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Parc>))]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<List<Parc>> Get()
    {
        // _logger.LogInformation("Get all parcs for current user");
        CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
        return _parcService.GetAll(currentUser);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Parc))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Parc> Get(int id)
    {
        CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
        return _parcService.GetById(id, currentUser);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Parc))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<Parc> Post(Parc parc)
    {
        CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
        return _parcService.Add(parc, currentUser.Id);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Parc))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<Parc> Put(int id, Parc parc)
    {
        return ParcService.Update(id, parc);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<bool> Delete(int id)
    {
        return ParcService.Delete(id);
    }
}   