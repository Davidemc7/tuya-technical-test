using Application.IServices;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterface;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderDTO> GetByIdAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            return _mapper.Map<Order, OrderDTO>(order);
        }

        public async Task<List<OrderDTO>> GetAllAsync()
        {
            var orders = await _repository.GetAllAsync();
            return _mapper.Map<List<Order>, List<OrderDTO>>(orders.ToList());
        }

        public async Task AddAsync(OrderDTO order)
        {
            var orderEntity = _mapper.Map<OrderDTO, Order>(order);

            orderEntity.Creator = 1;
            orderEntity.DateCreated = DateTime.Now;
            orderEntity.OrderDate = DateTime.Now;
            await _repository.AddAsync(orderEntity);
        }

        public async Task UpdateAsync(OrderDTO order)
        {
            var orderDB = await _repository.GetByIdAsync(order.OrderId.Value);
            if (orderDB == null)
                throw new Exception("Customer not found");

            var orderEntity = _mapper.Map(order, orderDB);
            await _repository.UpdateAsync(orderEntity);
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
