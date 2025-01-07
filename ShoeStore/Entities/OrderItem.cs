using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Entities
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Sipariş ürünü ID'si

        [Required]
        public int OrderId { get; set; }  // Sipariş ID'si

        [ForeignKey("OrderId")]
        public Order Order { get; set; }  // Sipariş

        [Required]
        public int ProductId { get; set; }  // Ürün ID'si

        [ForeignKey("ProductId")]
        public Product Product { get; set; }  // Ürün

        [Required]
        public int Quantity { get; set; }  // Ürün adedi

        [Required]
        public decimal Price { get; set; }  // Ürünün sipariş esnasındaki fiyatı
    }

}
