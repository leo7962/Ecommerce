using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.Server.Data;
using Ecommerce.Server.Dtos;
using Ecommerce.Server.Entities;
using Ecommerce.Server.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Server.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration configuration;
    private readonly DataContext context;

    public JwtService(IConfiguration configuration, DataContext context)
    {
        this.configuration = configuration;
        this.context = context;
    }

    public async Task<string> GenerateToken(UserDTO userDTO)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userDTO.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //new Claim(ClaimTypes.Role, userDTO.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<User> ValidateUser(LoginDTO loginDTO)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == loginDTO.UserName);
        if (user != null && VerifyPassword(loginDTO.Password, user.Password)) return user;
        return null;
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}