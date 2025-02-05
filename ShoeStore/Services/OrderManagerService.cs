using AutoMapper;
using ShoeStore.DTO;
using ShoeStore.Entities;
using ShoeStore.Enumarations;
using ShoeStore.Services.Implementation;

namespace ShoeStore.Services
{
    public class OrderManagerService : IOrderManagerService
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly IPaymentService _paymentService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public OrderManagerService(IOrderService orderService, IOrderItemService orderItemService, IPaymentService paymentService, IProductService productService ,IMapper mapper)
        {
            this._orderService = orderService;
            this._orderItemService = orderItemService;
            this._paymentService = paymentService;
            this._productService = productService;
            this._mapper = mapper;
        }

        /*
        *  Sepet Senaryoları için gerekli metodlar burada yazılacak.
        *  OrderItem önce DB'ye eklenecek daha sonra sepete eklenecek. Sepet iptal olursa OrderItemde iptal olur.
        *  OrderItem sepete eklenince Stoktan düşülecek.
        *  Ödeme kısmı detaylı olmayacak, sadece ödeme işlemi yapılıp yapılmadığı kontrol edilecek.
        */

        public async Task AddOrderItemAsync(int orderId, OrderItemDTO dto)
        {
            var order = await _orderService.GetOrderEntityByIdAsync(orderId);

            if(order == null) 
            {
                throw new Exception("Order not found !");
            }

            //orderItem
            var orderItem = _mapper.Map<OrderItem>(dto);
            orderItem.OrderId = orderId;

            //await _orderItemService.AddOrderItemAsync(dto);  // Db'ye OrderItem eklenir.

            //orderService
            if (order.OrderItems == null)
            {
                order.OrderItems = new List<OrderItem>();
            }

            order.TotalAmount += (dto.Quantity * dto.Price);
            order.OrderItems.Add(orderItem); // yukarıdaki durumu bu zaten ekliyor EF bunu bizim yerimize yapıyor.
            order.OrderCreateStatus = OrderCreateStatus.ProductsAdded;  // Durumu "Ürünler Eklendi,Ödeme yapılmadı" olarak ayarla.

            await _orderService.SaveChanges();
        }



        public async Task<OrderDTO> AddPaymentAsync(int orderId, PaymentDTO dto)
        {
            // **1. Siparişi getir**
            var orderDto = await _orderService.GetOrderByIdAsync(orderId);
            if (orderDto == null)
                throw new Exception("Sipariş bulunamadı!");

            // **2. Stok kontrolü yap**
            foreach (var item in orderDto.OrderItems)
            {
                bool isStockAvailable = await _productService.CheckStockAsync(item.ProductId, item.Quantity);
                if (!isStockAvailable)
                    throw new Exception($"Üzgünüz, {item.ProductId} ID numaralı üründen stokta yeterli miktarda bulunmuyor!");
            }

            // **3. Ödeme sürecini başlat**
            dto.OrderId = orderId;
            var paymentResult = await _paymentService.ProcessPaymentAsync(dto); // Gerçek bir ödeme sürecini simüle ettiğimizi varsayalım.

            if (!paymentResult.IsSuccess)
                throw new Exception($"Ödeme başarısız! Lütfen tekrar deneyin. Hata Mesajı : {paymentResult.Message} - Hata Kodu : {paymentResult.ErrorCode}");

            // **4. Ödeme başarılıysa order'ı güncelle**
            var paymentDto = _paymentService.AddPayment(dto); //TODO burayı asenkron tutup await ile bekletebiliriz.
            var orderToUpdate = await _orderService.GetOrderByIdAsync(orderId);

            orderToUpdate.Status = OrderStatus.Confirmed;  // Sipariş onaylandı
            orderToUpdate.OrderCreateStatus = OrderCreateStatus.Completed;  // Ödeme tamamlandı

            await _orderService.UpdateOrderAsync(orderToUpdate);

            // **5. Stokları güncelle**
            foreach (var item in orderToUpdate.OrderItems)
            {
                await _productService.DecreaseStockAsync(item.ProductId, item.Quantity);
            }

            // **6. Güncellenmiş siparişi döndür**
            var finalOrder = await _orderService.GetOrderByIdAsync(orderId);
            return _mapper.Map<OrderDTO>(finalOrder);
        }

       





    }
}
