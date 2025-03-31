using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using parc.Helpers;
using parc.Models;
using parc.Services;

namespace parc.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(requiredRole: "ParcAdmin")]

public class DeviceNetworkInfoController : ControllerBase
{
    private readonly DeviceNetworkInfoService _service;

    public DeviceNetworkInfoController(DeviceNetworkInfoService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DeviceNetworkInfo))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<DeviceNetworkInfo> Post(DeviceNetworkInfo data)
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

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceNetworkInfo))]
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

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceNetworkInfo))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(int id, DeviceNetworkInfo data)
    {
        try
        {
            return Ok(_service.Update(id, data));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}