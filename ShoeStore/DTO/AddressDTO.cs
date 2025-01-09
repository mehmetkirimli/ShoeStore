namespace ShoeStore.DTO
{
    public class AddressDTO
    {
        public int Id { get; set; } // Adres ID'si
        public string? City { get; set; } // Şehir
        public string? Street { get; set; } // Sokak
        public string? PostalCode { get; set; } // Posta kodu
        public int UserId { get; set; } // Kullanıcı ID'si (Foreign Key)
    }

}
