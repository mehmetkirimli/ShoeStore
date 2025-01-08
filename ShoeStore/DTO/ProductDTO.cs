namespace ShoeStore.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }  // Ürün ID'si
        public string Name { get; set; }  // Ürün adı
        public string Description { get; set; }  // Ürün açıklaması
        public decimal Price { get; set; }  // Ürün fiyatı
        public int Stock { get; set; }  // Ürün stoğu
        public int CategoryId { get; set; }  // Kategori ID'si
        public List<string> ImageUrls { get; set; }  // Görsel URL'leri
    }


}
