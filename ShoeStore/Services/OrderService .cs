using AutoMapper;
using ShoeStore.DTO;
using ShoeStore.Entities;
using ShoeStore.Enumarations;
using ShoeStore.Repositories.Implementation;
using ShoeStore.Services.Implementation;

namespace ShoeStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> orderRepo, IMapper mapper)
        {
            this._orderRepository = orderRepo;
            this._mapper = mapper;
        }
        public async Task AddOrderAsync(OrderDTO dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.OrderCreateStatus = OrderCreateStatus.Created;  // Durumu "Oluşturuluyor" olarak ayarla.

            await _orderRepository.AddAsync(order);
        }
        public async Task DeleteOrderAsync(int id)
        {
            if (_orderRepository.GetByIdAsync(id) == null)
            {
                throw new Exception("Order not found");
            }
            await _orderRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<OrderDTO>> GetAllOrderAsync()
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            Order order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderDTO>(order);
        }
        public async Task UpdateOrderAsync(OrderDTO dto)
        {
            if (GetOrderByIdAsync(dto.Id) == null)
            {
                throw new Exception("Order not found");
            }
            await _orderRepository.UpdateAsync(_mapper.Map<Order>(dto));
        }
        public async Task<List<OrderDTO>> GetOrderListByStatus(OrderStatus status)
        {
            List<Order> orders = await _orderRepository.FindByConditionAsync(order => order.OrderStatus == status, p => p.OrderStatus);
            return _mapper.Map<List<OrderDTO>>(orders);
        }
    }
}
