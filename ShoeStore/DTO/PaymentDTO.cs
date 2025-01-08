using ShoeStore.Enumarations;

namespace ShoeStore.DTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Foreign Key
        public decimal Amount { get; set; } // Zorunlu
        public PaymentMethod Method { get; set; } // Enum
        public PaymentStatus Status { get; set; } // Enum
    }

}
