using Application.Models;

namespace Application.IServices
{
    public interface IOrderService
    {
        Task<OrderDTO> GetByIdAsync(int id);
        Task<List<OrderDTO>> GetAllAsync();
        Task AddAsync(OrderDTO order);
        Task UpdateAsync(OrderDTO order);
        Task DeleteByIdAsync(int id);
        Task DeleteLogicallyByIdAsync(int id);
    }
}
