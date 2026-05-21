using Encode.Application.DTO.Auth;
using Encode.Application.DTOs.Auth;
using Encode.Application.Interfaces;
using Encode.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Encode.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly AppDbContext _context;
    public AuthController(
        IAuthService authService,
        AppDbContext context)
    {
        _authService = authService;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult>
    Register([FromBody]RegisterRequest request)
    {
        var token = await _authService.Register(request);

        return Ok(new
        {
            token
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult>
    Login([FromBody] LoginRequest request)
    {
        var token = await _authService.Login(request);

        return Ok(new
        {
            token
        });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Guid.Parse(userId));

        if (user == null)
            return Unauthorized();

        return Ok(new UserMeResponse
        {
            Nombre = user.Nombre,
            Email = user.Email,
            Equipo = user.Equipo
        });
    }
}