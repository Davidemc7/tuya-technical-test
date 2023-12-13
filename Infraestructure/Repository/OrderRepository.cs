using Domain.Entities;
using Domain.RepositoryInterface;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Order GetById(int id)
        {
            return _dbContext.Orders.Where(w => !w.Retired).Include(x => x.Customer).FirstOrDefault(x => x.OrderId.Equals(id));
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _dbContext.Orders.Where(w => !w.Retired).Include(x => x.Customer).FirstOrDefaultAsync(x => x.OrderId.Equals(id));
        }

        public ICollection<Order> GetAll()
        {
            return _dbContext.Orders.Where(x => !x.Retired).Include(x => x.Customer).ToList();
        }

        public async Task<ICollection<Order>> GetAllAsync()
        {
            return await _dbContext.Orders.Where(x => !x.Retired).Include(x => x.Customer).ToListAsync();
        }

        public void Add(Order Order)
        {
            _dbContext.Orders.Add(Order);
            _dbContext.SaveChanges();
        }

        public async Task AddAsync(Order Order)
        {
            await _dbContext.Orders.AddAsync(Order);
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Order Order)
        {
            _dbContext.Entry(Order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(Order Order)
        {
            _dbContext.Entry(Order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(Order Order)
        {
            _dbContext.Orders.Remove(Order);
            _dbContext.Entry(Order).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(Order Order)
        {
            _dbContext.Orders.Remove(Order);
            _dbContext.Entry(Order).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }

        public void DeleteById(int id)
        {
            var Order = _dbContext.Orders.Find(id);
            if (Order == null)
            {
                _dbContext.Orders.Remove(Order);
                _dbContext.Entry(Order).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            var Order = await _dbContext.Orders.FindAsync(id);
            if (Order == null)
            {
                _dbContext.Orders.Remove(Order);
                _dbContext.Entry(Order).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }
        }

        public void DeleteLogicallyById(int id)
        {
            var Order = _dbContext.Orders.Find(id);
            if (Order != null)
            {
                Order.Retired = true;
                Order.RetiredBy = 1;
                Order.DateRetired = DateTime.Now;
                _dbContext.Entry(Order).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

        public async Task DeleteLogicallyByIdAsync(int id)
        {
            var Order = await _dbContext.Orders.FindAsync(id);
            if (Order != null)
            {
                Order.Retired = true;
                Order.RetiredBy = 1;
                Order.DateRetired = DateTime.Now;
                _dbContext.Entry(Order).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
