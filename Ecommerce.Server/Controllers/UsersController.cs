using Ecommerce.Server.Dtos;
using Ecommerce.Server.Helpers;
using Ecommerce.Server.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
public class UsersController : ControllerBase
{
    private readonly IJwtService jwtService;
    private readonly UserManager<IdentityUser> userManager;
    private readonly IConfiguration configuration;
    private readonly IUserService userService;

    public UsersController(UserManager<IdentityUser> userManager, IConfiguration configuration, IUserService userService, IJwtService jwtService)
    {
        this.userManager = userManager;
        this.configuration = configuration;
        this.userService = userService;
        this.jwtService = jwtService;
    }

    [HttpGet]
    [Authorize]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
    {
        var users = await userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<UserDTO>> GetUser(int id)
    {
        var user = await userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutProduct(int id, UserDTO userDTO)
    {
        if (id != userDTO.Id) return BadRequest();
        await userService.UpdateUserAsync(userDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await userService.DeleteUserAsync(id);
        return NoContent();
    }

    [HttpPost("register", Name ="registerUser")]
    public async Task<ActionResult<AuthenticationResponse>> Register(UserCredential userCredential)
    {
        var user = new IdentityUser { UserName = userCredential.Email, Email = userCredential.Email };
        var result = await userManager.CreateAsync(user, userCredential.Password);

        if (result.Succeeded) return await BuildToken(userCredential);

        return BadRequest(result.Errors);
    }

    private async Task<AuthenticationResponse> BuildToken(UserCredential userCredential)
    {
        var claim = new List<Claim>
        {
            new("email", userCredential.Email)
        };

        var user = await userManager.FindByEmailAsync(userCredential.Email);
        var claimsDb = await userManager.GetClaimsAsync(user);

        claim.AddRange(claimsDb);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddMinutes(30);

        var securityToken = new JwtSecurityToken(null, null, claim, expires: expiration,
            signingCredentials: creds);

        return new AuthenticationResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
            Expiration = expiration
        };
    }
}