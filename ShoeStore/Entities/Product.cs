using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Ürün ID'si

        [Required]
        [StringLength(100)]
        public string Name { get; set; }  // Ürün adı

        [StringLength(500)]
        public string? Description { get; set; }  // Ürün açıklaması

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }  // Ürün fiyatı

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }  // Ürün stoğu

        public ICollection<ProductImage>? ProductImages { get; set; }  // Ürüne ait görseller

        [Required]
        public int CategoryId { get; set; }  // Ürünün kategorisi (ID ile)

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }  // Kategoriye referans

        public ICollection<OrderItem>? OrderItems { get; set; }  // Siparişlerdeki ürünler
    }


}
