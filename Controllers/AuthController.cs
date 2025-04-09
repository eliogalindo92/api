namespace api.Controllers;
using Microsoft.AspNetCore.Authorization;
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
    // Defines a constant name for permission's claim type 
    private const string PermissionClaimType = "permission";
    
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody]AuthDto authDto)
    {
        var user = await authService.Authenticate(authDto);

        if (user is null) return Unauthorized(new { Message = "Invalid credentials" });
        
        // Extract roles and permissions lists
        var roles = user.Roles
            .Select(role => role.Denomination)
            .Distinct()
            .ToList();
       
        var permissions = user.Roles // For each user's role
            .SelectMany(role => role.Permissions) // Gets all permissions (flatten the list)
            .Select(permission => permission.Denomination) // Takes each permission name
            .Distinct() // Secures that each permission is not repeated
            .ToList();
       
        // Create claims for the cookie
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
        };
        // Adds roles as standard claims
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        // Adds unique permissions as custom claims
        claims.AddRange(permissions.Select(permission => new Claim(PermissionClaimType, permission)));

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
        
        var userInfo = new UserInfoDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Roles = roles,
            Permissions = permissions
            
        };
        return Ok(userInfo);
    }

    [HttpPost("sign-out")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return NoContent();
    }
    
    [Authorize] 
    [HttpGet("status")]
    public IActionResult GetAuthStatus()
    {

        // Extracts the claims information already in HttpContext.User
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var userInfo = new UserInfoDto
        {
            Id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)),
            Username = User.FindFirstValue(ClaimTypes.Name) ?? string.Empty,
            Email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty,
            Roles = User.FindAll(ClaimTypes.Role).Select(claim => claim.Value).ToList(),
            Permissions = User.FindAll(PermissionClaimType).Select(claim => claim.Value).ToList(),
        };

        return Ok(userInfo);
    }
}