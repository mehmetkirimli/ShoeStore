namespace ShoeStore.DTO
{
    public class UserDTO
    {
        public int Id { get; set; } // Kullanıcının ID'si
        public string? FullName { get; set; } // Kullanıcının adı
        public string? Email { get; set; } // Email (Zorunlu)
        public string? Phone { get; set; } // Telefon numarası (11 haneli zorunlu)
        public List<AddressDTO>? Addresses { get; set; } // Kullanıcının adres bilgileri
    }

}
