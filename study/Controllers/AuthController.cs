using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using study.Model.DTOs;
using study.Services.Interfaces;

namespace study.Controllers;
[ApiController]
[Route("")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        var res = authService.Login(req.Username, req.Password);
        return res is null ? Unauthorized() : Ok(res);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register([FromBody] RegisterRequest req)
    {
        if (string.IsNullOrEmpty(req.Username) || string.IsNullOrEmpty(req.Password))
            return BadRequest("Username/password is required");
        var check = authService.Register(req.Username,req.Password);
        if (!check) return Conflict("Username is existed or invalid");
        return Ok($"Registered: {req.Username}");
    }


    [HttpDelete("delete")]
    [Authorize(Roles = "admin")]
    public IActionResult Delete([FromBody] DeleteRequest req) 
    {
        if (string.IsNullOrEmpty(req.Username) || string.IsNullOrEmpty(req.Password))
            return BadRequest("Username/password is required");
        var check = authService.Delete(req.Username);
        if (!check) return Conflict("Username is existed or invalid");
        return Ok($"Deleted: {req.Username}");
    }


    [HttpGet("secure")]
    [Authorize]
    public IActionResult Secure() => Ok("Secure OK");

    [HttpGet("admin")]
    [Authorize(Roles = "admin")]
    public IActionResult admin() => Ok("Admin OK");
}
