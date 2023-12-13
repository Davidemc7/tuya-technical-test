using Application.Models;

namespace Application.IServices
{
    public interface ICustomerService
    {
        Task<CustomerDTO> GetByIdAsync(int id);
        Task<List<CustomerDTO>> GetAllAsync();
        Task AddAsync(CustomerDTO customer);
        Task UpdateAsync(CustomerDTO customer);
        Task DeleteByIdAsync(int id);
        Task DeleteLogicallyByIdAsync(int id);
    }
}
