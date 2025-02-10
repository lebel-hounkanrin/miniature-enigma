using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using parc.Models;
using parc.Services;

namespace parc.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceTechnicalSpecsController: ControllerBase
{
    private readonly DeviceTechnicalSpecsService _service;
    
    public DeviceTechnicalSpecsController(DeviceTechnicalSpecsService service)
    {
        _service = service;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DeviceTechnicalSpecs))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<DeviceGenralInfo> Post(DeviceTechnicalSpecs data)
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
}