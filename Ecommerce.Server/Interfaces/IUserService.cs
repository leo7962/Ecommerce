using Ecommerce.Server.Dtos;
using Ecommerce.Server.Helpers;

namespace Ecommerce.Server.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    Task<UserDTO> GetUserByIdAsync(int id);    
    Task UpdateUserAsync(UserDTO userDTO);
    Task DeleteUserAsync(int id);
}