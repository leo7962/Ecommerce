using Ecommerce.Server.Dtos;

namespace Ecommerce.Server.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO> GetCustomerByIdAsync(int id);
        Task<CustomerDTO> CreateCustomerAsync(CustomerDTO customerDTO);
        Task UpdateCustomerAsync(int id, CustomerDTO customerDTO);
        Task DeleteCustomerAsync(int id);
    }
}
