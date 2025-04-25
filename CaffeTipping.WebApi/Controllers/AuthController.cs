using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CaffeTipping.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CaffeTipping.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    [HttpPost("login")]
    public ActionResult<string> Login([FromBody] LoginModel model)
    {
        if (model is { Username: "AppLifting", Password: "LetsBeSecure" })
        {
            var token = GenerateAccessToken(model.Username);
            return new OkObjectResult(new { AccessToken = new JwtSecurityTokenHandler().WriteToken(token)});
        }
        
        return new UnauthorizedObjectResult("Invalid credentials");
    }

    // Generating token based on user information
    private JwtSecurityToken GenerateAccessToken(string userName)
    {
        // Create user claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userName),
            // Add additional claims as needed (e.g., roles, etc.)
        };

        // Create a JWT
        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10), // Token expiration time
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}