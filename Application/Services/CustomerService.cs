using Application.IServices;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterface;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomerDTO> GetByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            return _mapper.Map<Customer, CustomerDTO>(customer);
        }

        public async Task<List<CustomerDTO>> GetAllAsync()
        {
            var customers = await _repository.GetAllAsync();
            return _mapper.Map<List<Customer>, List<CustomerDTO>>(customers.ToList());
        }

        public async Task AddAsync(CustomerDTO customer)
        {
            var customerEntity = _mapper.Map<CustomerDTO, Customer>(customer);

            customerEntity.Creator = 1;
            customerEntity.DateCreated = DateTime.Now;
            await _repository.AddAsync(customerEntity);
        }

        public async Task UpdateAsync(CustomerDTO customer)
        {
            var customerEntity = _mapper.Map<CustomerDTO, Customer>(customer);
            await _repository.UpdateAsync(customerEntity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        public async Task DeleteLogicallyByIdAsync(int id)
        {
            await _repository.DeleteLogicallyByIdAsync(id);
        }
    }
}
