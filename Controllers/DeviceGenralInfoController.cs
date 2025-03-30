using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using parc.Models;
using parc.Services;

namespace parc.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceGenralInfoController: ControllerBase
{
    private readonly ILogger<DeviceGenralInfoController> _logger;
    private readonly DeviceGenralInfoService _deviceGenralInfoService;

    public DeviceGenralInfoController(ILogger<DeviceGenralInfoController> logger,
        DeviceGenralInfoService deviceGenralInfoService)
    {
        _logger = logger;
        _deviceGenralInfoService = deviceGenralInfoService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DeviceGenralInfo))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<DeviceGenralInfo> Post(DeviceGenralInfo device)
    {
        try
        {
            return Ok(_deviceGenralInfoService.Add(device));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet(Name = "Get all devices")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Device>))]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<List<DeviceGenralInfo>> Get()
    {
        CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
        // _logger.LogInformation("Get all parcs for current user");
        return _deviceGenralInfoService.GetAll(currentUser);
    }
    
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceGenralInfo))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(int id, DeviceGenralInfo data)
    {
        try
        {
            return Ok(_deviceGenralInfoService.Update(id, data));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceGenralInfo))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetOne(int id)
    {
        try
        {
           return Ok(_deviceGenralInfoService.GetById(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            CustomUser currentUser = HttpContext.Items["User"] as CustomUser;
            return Ok(_deviceGenralInfoService.DeleteAsync(id, currentUser));
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = $"An error occurred while trying to delete device", error = e.Message });
        }
    }

}

