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

        public async Task AddOrderAsync(OrderWithPaymentDTO dto)
        {
            using (var transaction = await _orderRepository.BeginTransactionAsync())
            {
                try
                {

                    //create order
                    var order = _mapper.Map<Order>(dto.orderDto);
                    await _orderRepository.AddAsync(order);

                    //create payment and associate with orderId
                    var payment = _mapper.Map<Payment>(dto.paymentDto);
                    payment.OrderId = order.Id;
                    await _paymentService.AddPayment(_mapper.Map<PaymentDTO>(payment));


                    order.Payment = payment;
                    await _orderRepository.UpdateAsync(order);

                    await transaction.CommitAsync();



                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Transaction failed : " + e.Message);
                }
            }
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
