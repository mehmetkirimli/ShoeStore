namespace ShoeStore.DTO
{
    public class OrderWithPaymentDTO
    {
        public int UserId { get; set; }  // Siparişi oluşturan kullanıcı
        public OrderDTO orderDto { get; set; }  // Sipariş bilgileri
        public PaymentDTO paymentDto { get; set; }  // Ödeme bilgileri
    }
}
