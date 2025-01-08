using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoeStore.Entities
{
    public class ProductImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Görsel ID'si

        [Required]
        public string ImageUrl { get; set; }  // Görselin URL'si

        [Required]
        public int ProductId { get; set; }  // Ürüne ait Foreign Key

        [ForeignKey("ProductId")]
        public Product Product { get; set; }  // Ürüne referans
    }

}
