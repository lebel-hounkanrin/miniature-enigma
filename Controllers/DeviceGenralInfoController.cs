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
    public ActionResult<Device> Post(DeviceGenralInfo device)
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
}