using ShoeStore.DTO;
using ShoeStore.Entities;

namespace ShoeStore.Services.Implementation
{
    public interface IAddressService
    {
        Task AddAddress(AddressDTO dto);
        Task DeleteAddress(int id);
        Task<AddressDTO> UpdateAddress(AddressDTO dTO);
        Task<ICollection<AddressDTO>> GetAddressesByCityAsync(string city);
        Task<ICollection<AddressDTO>> GetAllAddress();
        Task<AddressDTO> GetAddressByIdAsync(int id);
    }
}
