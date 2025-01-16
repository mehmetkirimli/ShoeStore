namespace ShoeStore.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } // Zorunlu
        public List<ProductDTO>? Products { get; set; } //AutoMapper'da ignore ediliyor !!!  
    }

}
