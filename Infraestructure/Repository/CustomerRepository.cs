using Domain.Entities;
using Domain.RepositoryInterface;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer GetById(int id)
        {
            return _dbContext.Customers.Find(id);
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public ICollection<Customer> GetAll()
        {
            return _dbContext.Customers.Where(x => !x.Retired).ToList();
        }

        public async Task<ICollection<Customer>> GetAllAsync()
        {
            return await _dbContext.Customers.Where(x => !x.Retired).ToListAsync();
        }

        public void Add(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }

        public async Task AddAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Customer customer)
        {
            _dbContext.Entry(customer).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _dbContext.Entry(customer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(Customer customer)
        {
            _dbContext.Customers.Remove(customer);
            _dbContext.Entry(customer).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(Customer customer)
        {
            _dbContext.Customers.Remove(customer);
            _dbContext.Entry(customer).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }

        public void DeleteById(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer == null)
            {
                _dbContext.Customers.Remove(customer);
                _dbContext.Entry(customer).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                _dbContext.Entry(customer).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }
        }

        public void DeleteLogicallyById(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer != null)
            {
                customer.Retired = true;
                customer.RetiredBy = 1;
                customer.DateRetired = DateTime.Now;
                _dbContext.Entry(customer).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

        public async Task DeleteLogicallyByIdAsync(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer != null)
            {
                customer.Retired = true;
                customer.RetiredBy = 1;
                customer.DateRetired = DateTime.Now;
                _dbContext.Entry(customer).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
