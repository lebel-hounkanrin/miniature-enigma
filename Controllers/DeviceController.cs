using System.Net.Mime;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using parc.Models;
using parc.Services;

namespace parc.Controllers;


[ApiController]
[Route("[controller]")]
public class DeviceController: ControllerBase
{
    
    private readonly ILogger<SalleController> _logger;
    private readonly DeviceService _deviceService;

    public DeviceController(ILogger<SalleController> logger, DeviceService deviceService)
    {
        _logger = logger;
        _deviceService = deviceService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Device))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<Device> Post(Device device)
    {
        return _deviceService.Add(device);
    }
    
    [HttpGet(Name = "Get all devices")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Device>))]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<List<Device>> Get()
    {
        // _logger.LogInformation("Get all parcs for current user");
        return _deviceService.GetAll();
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Device))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(int id, Device _device)
    {
        var device = _deviceService.GetById(id);
        if (device == null)
        {
            return NotFound(new { message = "can not found device with this id." });
        }
        await _deviceService.Update(id, _device);
        return Ok(new { message = $" Todo Item  with id {id} successfully updated" });
    }
}