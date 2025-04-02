using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using parc.Helpers;
using parc.Hubs;

namespace parc.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(requiredRole: "ParcAdmin")]
public class Actions: ControllerBase
{
    private readonly IHubContext<CommandHub> _hubContext;


    public Actions(IHubContext<CommandHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("shutdown")]
    public async Task<IActionResult> Shutdown(int deviceId)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveCommand", deviceId,  "shutdown -s -t 0");
        return Ok("Shutdown command sent via SignalR.");
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(int deviceId)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveCommand", deviceId, "shutdown -l");
        return Ok("Logout command sent via SignalR.");
    }
}