using ShoeStore.Enumarations;

namespace ShoeStore.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign Key
        public List<OrderItemDTO> OrderItems { get; set; }
        public OrderStatus Status { get; set; } // Enum
        public DateTime OrderDate { get; set; }
    }

}
