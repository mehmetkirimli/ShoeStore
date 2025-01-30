using ShoeStore.DTO;

namespace ShoeStore.Services.Implementation
{
    public interface IOrderManagerService
    {
        Task AddOrderItemAsync(int orderId, OrderItemDTO dto);
        Task<OrderDTO> AddPaymentAsync(int orderId, PaymentDTO dto);
    }
}
