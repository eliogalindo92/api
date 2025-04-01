namespace api.Controllers;
using Dtos.Auth;
using Services;
using Microsoft.AspNetCore.Authentication.Cookies;


using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

[ApiController]
[Route("[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody]AuthDto authDto)
    {
        var user = await authService.Authenticate(authDto);

        if (user is null) return Unauthorized(new { Message = "Invalid credentials" });
        
        // Generate JWT token
        var jwtToken = authService.GenerateJwtToken(user);

        // Create claims for the cookie
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new("jwt", jwtToken) // Storages the JWT token in a claim
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, // Makes the cookie persistent
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30), // Sets the expiration time for the cookie
            AllowRefresh = true, // Allows the cookie to be refreshed
        };

        // Creates the authentication cookie
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return Ok(new { Message = "User successfully signed in" });
    }

    [HttpPost("sign-out")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { Message = "User successfully signed out" });
    }
}