using AutoMapper;
using Ecommerce.Server.Data;
using Ecommerce.Server.Dtos;
using Ecommerce.Server.Entities;
using Ecommerce.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services;

public class UserService : IUserService
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public UserService(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
    {
        var user = mapper.Map<User>(userDTO);
        //user.Password = HashPassword(userDTO.Password);
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return mapper.Map<UserDTO>(userDTO);
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