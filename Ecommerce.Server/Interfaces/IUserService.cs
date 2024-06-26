﻿using Ecommerce.Server.Dtos;

namespace Ecommerce.Server.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    Task<UserDTO> GetUserByIdAsync(int id);
    Task UpdateUserAsync(UserDTO userDTO);
    Task DeleteUserAsync(int id);
}