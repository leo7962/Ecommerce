using Ecommerce.Server.Dtos;
using Ecommerce.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IJwtService jwtService;

        public UsersController(IUserService userService, IJwtService jwtService)
        {
            this.userService = userService;
            this.jwtService = jwtService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var customers = await userService.GetAllUsersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var customer = await userService.GetUserByIdAsync(id);
            return Ok(customer);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO customerDTO)
        {
            var customerCreated = await userService.CreateUserAsync(customerDTO);
            return CreatedAtAction(nameof(GetUser), new { id = customerDTO.Id }, customerCreated);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProduct(int id, UserDTO customerDTO)
        {
            if (id != customerDTO.Id)
            {
                return BadRequest();
            }
            await userService.UpdateUserAsync(customerDTO);
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
}
