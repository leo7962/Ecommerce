using Ecommerce.Server.Dtos;
using Ecommerce.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IJwtService jwtService;
    private readonly IUserService userService;

    public UsersController(IUserService userService, IJwtService jwtService)
    {
        this.userService = userService;
        this.jwtService = jwtService;
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

    [HttpPost("register")]
    public async Task<ActionResult> Register(UserDTO userDTO)
    {
        var user = await userService.CreateUserAsync(userDTO);
        var token = await jwtService.GenerateToken(user);
        return Ok(new { token });
    }
}