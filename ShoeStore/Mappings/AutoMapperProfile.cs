using AutoMapper;
using ShoeStore.DTO;
using ShoeStore.Entities;

namespace ShoeStore.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            //Entity to DTO
            CreateMap<User,UserDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Category, CategoryDTO>().ForMember(member => member.Products, opt => opt.Ignore());
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap<Payment, PaymentDTO>();
            CreateMap<Address, AddressDTO>();




            //DTO to Entity
            CreateMap<UserDTO, User>();
            CreateMap<ProductDTO, Product>();
            CreateMap<CategoryDTO, Category>().ForMember(member => member.Products, opt => opt.Ignore());
            CreateMap<OrderDTO, Order>();
            CreateMap<OrderItemDTO, OrderItem>();
            CreateMap<PaymentDTO, Payment>();
            CreateMap<AddressDTO, Address>();

            //TODO Bu yapıyı Program.cs'daki ConfigureServices metodunda eklemeyi unutma
        }
    }
}
