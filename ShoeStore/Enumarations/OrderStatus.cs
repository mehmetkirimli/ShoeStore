namespace ShoeStore.Enumarations
{
    public enum OrderStatus
    {
        Pending,    // Beklemede            //İlk başta bu şekilde olacak
        Confirmed,  // Onaylandı            //Ödeme yapılınca bu şekilde olacak
        Shipped,    // Kargoya verildi      //Kargo ile haberleşip onay gelince böyle olacak
        Delivered,  // Teslim edildi        //Teslimat yapıldığında bu şekilde olacak
        Cancelled   // İptal edildi         //İptal edildiğinde bu şekilde olacak
    }
}
