using ShoeStore.DTO;
using ShoeStore.Entities;
using ShoeStore.Enumarations;

namespace ShoeStore.Services.Implementation
{
    public interface IPaymentService
    {
        Task AddPayment(PaymentDTO dto);
        Task DeletePayment(int id);
        Task<PaymentDTO> UpdatePayment(PaymentDTO dTO);
        Task<ICollection<PaymentDTO>> GetPaymentsByUserAsync(int userId);
        Task<ICollection<PaymentDTO>> GetAllPayment();
        Task<PaymentDTO> GetPaymentByIdAsync(int id);
        Task<List<PaymentDTO>> GetPaymentListByStatus(PaymentStatus paymentStatus);
        Task<PaymentResultDTO> ProcessPaymentAsync(PaymentDTO dto);
    }
}
