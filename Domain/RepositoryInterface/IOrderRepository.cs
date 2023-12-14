using Domain.Entities;

namespace Domain.RepositoryInterface
{
    public interface IOrderRepository
    {
        Order GetById(int id);
        Task<Order> GetByIdAsync(int id);
        ICollection<Order> GetAll();
        Task<ICollection<Order>> GetAllAsync();
        void Add(Order order);
        Task AddAsync(Order order);
        void Update(Order order);
        Task UpdateAsync(Order order);
        void Delete(Order order);
        Task DeleteAsync(Order order);
        void DeleteById(int id);
        Task DeleteByIdAsync(int id);
        void DeleteLogicallyById(int id);
        Task DeleteLogicallyByIdAsync(int id);
    }
}
