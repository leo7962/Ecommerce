using Ecommerce.Server.Dtos;

namespace Ecommerce.Server.Interfaces;

public interface IDetailOrderService
{
    Task<IEnumerable<DetailOrderDTO>> GetAllDetailOrderAsync();
    Task<DetailOrderDTO> GetDetailOrderByIdAsync(int id);
    Task<DetailOrderDTO> CreateDetailOrderAsync(DetailOrderDTO detailOrderDTO);
    Task UpdateDetailOrderAsync(int id, DetailOrderDTO detailOrderDTO);
    Task DeleteDetailOrderAsync(int id);
}