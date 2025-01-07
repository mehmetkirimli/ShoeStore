using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Kategori ID'si
        [Required]
        public string Name { get; set; }  // Kategori adı
        public ICollection<Product> Products { get; set; }  // Bu kategorideki ürünler
    }

}
