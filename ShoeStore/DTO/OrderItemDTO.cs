namespace ShoeStore.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; } // Foreign Key
        public int OrderId { get; set; } // Foreign Key
        public decimal Price { get; set; } // Zorunlu
        public int Quantity { get; set; } // adet
    }

}
