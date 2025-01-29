namespace ShoeStore.Enumarations
{
    public enum OrderCreateStatus
    {
        Created = 1,         // Sepet oluşturuldu
        ProductsAdded = 2,   // Ürünler eklendi, ödeme yapılmadı
        Completed = 3,        // Sepet tamamlandı, ödeme yapıldı
        Fail = 4             // Sepet oluşturulamadı
    }
}
