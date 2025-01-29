using AutoMapper;
using ShoeStore.DTO;
using ShoeStore.Entities;
using ShoeStore.Enumarations;
using ShoeStore.Repositories.Implementation;
using ShoeStore.Services.Implementation;

namespace ShoeStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(IRepository<Order> orderRepo, IMapper mapper, IPaymentService paymentService)
        {
            this._orderRepository = orderRepo;
            this._mapper = mapper;
            this._paymentService = paymentService;
        }

        public async Task AddOrderAsync(OrderDTO dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.OrderCreateStatus = OrderCreateStatus.Created;  // Durumu "Oluşturuluyor" olarak ayarla.

            await _orderRepository.AddAsync(order);
        }

        public async Task AddOrderItemAsync(int orderId, OrderItemDTO dto)
        {
            var orderItem = _mapper.Map<OrderItem>(dto);
            orderItem.OrderId = orderId;  // İlgili order'a eklenmesi sağlanır.

            var orderDto = GetOrderByIdAsync(orderId);  // Order'a eklenir.
            var order = _mapper.Map<Order>(orderDto);
            if(order.OrderItems == null)
            {
                order.OrderItems = new List<OrderItem>();
            }
            order.OrderItems.Add(orderItem);
            order.OrderCreateStatus = OrderCreateStatus.ProductsAdded;  // Durumu "Ürünler Eklendi,Ödeme yapılmadı" olarak ayarla.

            await _orderRepository.UpdateAsync(order);
        }

        public async Task<OrderDTO> AddPaymentAsync(int orderId, PaymentDTO dto)
        {
            // Ödeme işlemi yapılır.
            dto.OrderId = orderId;  // Ödeme ile ilgili Order'ın Id'si belirtilir.

            await _paymentService.AddPayment(dto); // burada süreç uzun olacak , ödeme işlemi yapılacak. Banka ile haberleşecek , ödeme başarılı olursa aşağıdaki işlemler yapılacak.
                                                   // Ödeme başarılıysa, Order'ın durumunu "Onaylandı" yapabiliriz.
            var paymentDTO = await _paymentService.GetPaymentByIdAsync(dto.Id);

            if (paymentDTO.Status == PaymentStatus.Completed)
            {
                var orderToUpdate = await _orderRepository.GetByIdAsync(orderId);
                orderToUpdate.OrderStatus = OrderStatus.Confirmed;  // Durumu "Onaylandı" olarak ayarla.
                orderToUpdate.OrderCreateStatus = OrderCreateStatus.Completed;  // Durumu "Ödeme Tamamlandı" olarak ayarla.
                await _orderRepository.UpdateAsync(orderToUpdate);
            }

            var finalOrder = await _orderRepository.GetByIdAsync(orderId);
            return _mapper.Map<OrderDTO>(finalOrder);
        }

        public async Task DeleteOrderAsync(int id)
        {
            if (_orderRepository.GetByIdAsync(id) == null)
            {
                throw new Exception("Order not found");
            }
            await _orderRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrderAsync()
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            Order order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task UpdateOrderAsync(OrderDTO dto)
        {
            if (GetOrderByIdAsync(dto.Id) == null)
            {
                throw new Exception("Order not found");
            }
            await _orderRepository.UpdateAsync(_mapper.Map<Order>(dto));
        }

        public async Task<List<OrderDTO>> GetOrderListByStatus(OrderStatus status)
        {
            List<Order> orders = await _orderRepository.FindByConditionAsync(order => order.OrderStatus == status, p => p.OrderStatus);
            return _mapper.Map<List<OrderDTO>>(orders);
        }
    }
}
