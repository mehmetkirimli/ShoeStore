using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Ürün ID'si
        public string Name { get; set; }  // Ürün adı
        public string Description { get; set; }  // Ürün açıklaması
        [Required]
        public decimal Price { get; set; }  // Ürün fiyatı
        [Required]
        public int Stock { get; set; }  // Ürün stoğu
        public string ImageUrl { get; set; }  // Ürün görseli
        public ICollection<OrderItem> OrderItems { get; set; }  // Siparişlerdeki ürünler
        public ICollection<Category> Categories { get; set; }  // Ürünün kategorileri
    }

}
