using ShoeStore.DTO;

namespace ShoeStore.Services.Implementation
{
    public interface IOrderItemService
    {
        Task AddOrderItemAsync(OrderItemDTO dto);
        Task DeleteOrderItemAsync(int id);
        Task<IEnumerable<OrderItemDTO>> GetAllOrderItemsAsync();
        Task<OrderItemDTO> GetOrderItemByIdAsync(int id);
        Task UpdateOrderItemAsync(OrderItemDTO dto);
        Task<IEnumerable<OrderItemDTO>> GetOrderItemsByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderItemDTO>> GetOrderItemsByProductIdAsync(int productId);
    }
}
