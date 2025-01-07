using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoeStore.Enumarations;

namespace ShoeStore.Entities
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Ödeme ID'si

        [Required]
        public int OrderId { get; set; }  // Sipariş ID'si

        [ForeignKey("OrderId")]
        public Order Order { get; set; }  // Sipariş

        [Required]
        public decimal Amount { get; set; }  // Ödenen tutar

        [Required]
        public PaymentMethod PaymentMethod { get; set; }  // Ödeme yöntemi (örneğin "Kredi Kartı", "Kapıda Ödeme")
        public DateTime PaymentDate { get; set; }  // Ödeme tarihi

        [Required]
        public PaymentStatus PaymentStatus { get; set; }  // Ödeme durumu (örneğin "Başarılı", "Başarısız")
    }

}
