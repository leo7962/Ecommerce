using AutoMapper;
using Ecommerce.Server.Data;
using Ecommerce.Server.Dtos;
using Ecommerce.Server.Entities;
using Ecommerce.Server.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services;

public class UserService : IUserService
{
    private readonly IConfiguration configuration;
    private readonly DataContext context;
    private readonly IMapper mapper;
    private readonly UserManager<IdentityUser> userManager;

    public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration, DataContext context,
        IMapper mapper)
    {
        this.userManager = userManager;
        this.configuration = configuration;
        this.context = context;
        this.mapper = mapper;
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user != null)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var users = await context.Users.ToListAsync();
        return mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        return mapper.Map<UserDTO>(user);
    }

    public async Task UpdateUserAsync(UserDTO userDTO)
    {
        var user = mapper.Map<User>(userDTO);
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}