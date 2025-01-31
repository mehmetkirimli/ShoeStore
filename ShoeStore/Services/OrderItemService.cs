using AutoMapper;
using ShoeStore.DTO;
using ShoeStore.Entities;
using ShoeStore.Repositories.Implementation;
using ShoeStore.Services.Implementation;

namespace ShoeStore.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderItemService(IRepository<OrderItem> orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public async Task<OrderItemDTO> AddOrderItemAsync(OrderItemDTO dto)
        {
            var orderItem = _mapper.Map<OrderItem>(dto);
            await _orderItemRepository.AddAsync(orderItem); // Kaydı DB'ye ekliyoruz

            return _mapper.Map<OrderItemDTO>(orderItem); // Kaydedilen nesneyi geri döndür
        }


        public Task DeleteOrderItemAsync(int id)
        {
            var orderItem = _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
            {
                return null;
            }
            return _orderItemRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrderItemDTO>> GetAllOrderItemsAsync()
        {
            var orderItems = await _orderItemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderItemDTO>>(orderItems);
        }

        public async Task<OrderItemDTO> GetOrderItemByIdAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
            {
                return null;
            }
            return  _mapper.Map<OrderItemDTO>(orderItem);
        }

        public async Task<IEnumerable<OrderItemDTO>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            var orderItems = await _orderItemRepository.FindByConditionAsync(x => x.OrderId == orderId);
            return _mapper.Map<IEnumerable<OrderItemDTO>>(orderItems);
        }

        public async Task<IEnumerable<OrderItemDTO>> GetOrderItemsByProductIdAsync(int productId)
        {
            var orderItems = await _orderItemRepository.FindByConditionAsync(x => x.ProductId == productId);
            return _mapper.Map<IEnumerable<OrderItemDTO>>(orderItems);
        }

        public async Task UpdateOrderItemAsync(OrderItemDTO dto)
        {
            if(GetOrderItemByIdAsync(dto.Id) == null) 
            {
                throw new Exception("OrderItem not found");
            }
            await _orderItemRepository.UpdateAsync(_mapper.Map<OrderItem>(dto));
        }
    }
}
