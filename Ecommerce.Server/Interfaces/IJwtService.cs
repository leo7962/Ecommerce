using Ecommerce.Server.Dtos;
using Ecommerce.Server.Entities;

namespace Ecommerce.Server.Interfaces
{
    public interface IJwtService
    {        
        Task<User> ValidateUser(LoginDTO loginDTO);
        Task<string> GenerateToken(UserDTO userDTO);
    }
}
