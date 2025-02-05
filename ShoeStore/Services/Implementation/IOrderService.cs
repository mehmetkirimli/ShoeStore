using ShoeStore.DTO;
using ShoeStore.Entities;
using ShoeStore.Enumarations;

namespace ShoeStore.Services.Implementation
{
    public interface IOrderService
    {
        Task<OrderDTO> GetOrderByIdAsync(int id);
        Task UpdateOrderAsync(OrderDTO orderDTO);
        Task DeleteOrderAsync(int id);
        Task<IEnumerable<OrderDTO>> GetAllOrderAsync();
        Task<List<OrderDTO>> GetOrderListByStatus(OrderStatus status);
        Task AddOrderAsync(OrderDTO dto);
        Task<Order> GetOrderEntityByIdAsync(int orderId);
        Task SaveChanges();
    }
}
