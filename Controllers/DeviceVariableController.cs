using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;
using parc.Models;
using parc.Services;

namespace parc.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceVariableController: ControllerBase
{
    private readonly DeviceVariableService _service;

    public DeviceVariableController(DeviceVariableService service)
    {
        _service = service;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DeviceVariable))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<DeviceVariable> Post(DeviceVariable data)
    {
        try
        {
            return Ok(_service.Add(data));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet(Name = "Get all devices variables")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DeviceVariable>))]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<List<DeviceVariable>> Get(int parcId)
    {
        CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
        // _logger.LogInformation("Get all parcs for current user");
        return _service.GetAll(parcId);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceVariable))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetOne(int id)
    {
        try
        {
            return Ok(_service.GetById(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}