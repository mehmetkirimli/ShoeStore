namespace ShoeStore.DTO
{
    public class PaymentResultDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; } // Banka işlem numarası (simüle)
        public string ErrorCode { get; set; } // Ödeme başarısız olursa hata kodu
    }

}
