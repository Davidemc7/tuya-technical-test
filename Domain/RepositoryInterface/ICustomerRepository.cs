using Domain.Entities;

namespace Domain.RepositoryInterface
{
    public interface ICustomerRepository
    {
        Customer GetById(int id);
        Task<Customer> GetByIdAsync(int id);
        ICollection<Customer> GetAll();
        Task<ICollection<Customer>> GetAllAsync();
        void Add(Customer customer);
        Task AddAsync(Customer customer);
        void Update(Customer customer);
        Task UpdateAsync(Customer customer);
        void Delete(Customer customer);
        Task DeleteAsync(Customer customer);
        void DeleteById(int id);
        Task DeleteByIdAsync(int id);
        void DeleteLogicallyById(int id);
        Task DeleteLogicallyByIdAsync(int id);
    }
}
