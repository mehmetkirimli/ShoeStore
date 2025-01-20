using AutoMapper;
using ShoeStore.Data;
using ShoeStore.DTO;
using ShoeStore.Entities;
using ShoeStore.Enumarations;
using ShoeStore.Repositories;
using ShoeStore.Repositories.Implementation;
using ShoeStore.Services.Implementation;

namespace ShoeStore.Services
{
     public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(IRepository<Payment> paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task AddPayment(PaymentDTO dto)
        {
            if(dto == null)
            {
                throw new Exception("Payment is null");
            }
            var payment = _mapper.Map<Payment>(dto);
            await _paymentRepository.AddAsync(payment);
        }

        public async Task DeletePayment(int id)
        {
            if(GetPaymentByIdAsync(id) == null)
            {
                throw new Exception("Payment not found");
            }
            await _paymentRepository.DeleteAsync(id);
        }

        public async Task<ICollection<PaymentDTO>> GetAllPayment()
        {
            var payments = await _paymentRepository.GetAllAsync();
            if(payments == null)
            {
                throw new Exception("Payments not found");
            }
            return _mapper.Map<ICollection<PaymentDTO>>(payments);

        }

        public async Task<PaymentDTO> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                throw new Exception("Payment not found");
            }
            return _mapper.Map<PaymentDTO>(payment);
        }

        public async Task<List<PaymentDTO>> GetPaymentListByStatus(PaymentStatus paymentStatus)
        {
            var payments = await _paymentRepository.GetAllAsync(); 
            if (payments == null)
            {
                throw new Exception("Payments not found");
            }
            var filteredPayments = payments.Where(p => p.PaymentStatus == paymentStatus).ToList();
            //TODO repo'daki ortak dinamik metotta kullanılabilir veya bu şekilde filtremele yapılabilir.Performans karlışatırılmalı.
            return _mapper.Map<List<PaymentDTO>>(filteredPayments);
        }

        public async Task<ICollection<PaymentDTO>> GetPaymentsByUserAsync(int userId)
        {
            var payments = await _paymentRepository.FindByConditionAsync(p => p.Order.UserId == userId);
            if (payments == null)
            {
                throw new Exception("Payments not found");
            }
            return _mapper.Map<ICollection<PaymentDTO>>(payments);
        }

        public async Task<PaymentDTO> UpdatePayment(PaymentDTO dto)
        {
            if (await GetPaymentByIdAsync(dto.Id) != null)
            {
                await _paymentRepository.UpdateAsync(_mapper.Map<Payment>(dto));
                return dto;
            }
            throw new Exception("Payment not found");
        }
    }
}
