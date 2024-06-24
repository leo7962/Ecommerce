using Ecommerce.Server.Dtos;
using Ecommerce.Server.Helpers;
using Ecommerce.Server.Interfaces;
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
public class UsersController : ControllerBase
{
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly UserManager<IdentityUser> userManager;
    private readonly IConfiguration configuration;
    private readonly IUserService userService;

    public UsersController(UserManager<IdentityUser> userManager, IConfiguration configuration, IUserService userService, SignInManager<IdentityUser> signInManager)
    {
        this.userManager = userManager;
        this.configuration = configuration;
        this.userService = userService;
        this.signInManager = signInManager;
    }

    [HttpGet]
    [Authorize]    
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

    [HttpPost("register")] //api/Users/register
    public async Task<ActionResult<AuthenticationResponse>> Register(UserCredential userCredential)
    {
        var user = new IdentityUser { UserName = userCredential.Email, Email = userCredential.Email };
        var result = await userManager.CreateAsync(user, userCredential.Password);

        if (result.Succeeded) return await BuildToken(userCredential);

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> Login(UserCredential userCredential)
    {
        var result = await signInManager.PasswordSignInAsync(userCredential.Email, userCredential.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return await BuildToken(userCredential);
        }
        else
        {
            return BadRequest("Bad login ");
        }
    }

    private async Task<AuthenticationResponse> BuildToken(UserCredential userCredential)
    {
        var claim = new List<Claim>
        {
            new("email", userCredential.Email),
            new("userName", userCredential.UserName)
        };

        var user = await userManager.FindByEmailAsync(userCredential.Email);
        var claimsDb = await userManager.GetClaimsAsync(user);

        claim.AddRange(claimsDb);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var expiration = DateTime.UtcNow.AddDays(1);

        var securityToken = new JwtSecurityToken(null, null, claim, expires: expiration,
            signingCredentials: creds);

        return new AuthenticationResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
            Expiration = expiration
        };
    }
}