using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using parc.Helpers;
using parc.Models;
using parc.Services;

namespace parc.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController: ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<UserController> _logger;
    private readonly IHttpContextAccessor _httpContext;

    public UserController(UserService userService,
        ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _userService = userService;
        _httpContext = httpContextAccessor;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomUser))]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<CustomUser> Post(CustomUser user)
    {
        try
        {
            return _userService.Add(user);
        } 
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An error occurred while creating user", error = ex.Message });

        }
    }

 
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        try
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An error occurred while trying to authenticate", error = ex.Message });

        }
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        try
        {
            CustomUser user = (CustomUser)_httpContext.HttpContext.Items["User"];
            _logger.LogInformation($"User {user.Email} logged in");
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}