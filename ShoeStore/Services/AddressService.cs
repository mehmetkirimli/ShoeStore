using AutoMapper;
using ShoeStore.Data;
using ShoeStore.DTO;
using ShoeStore.Entities;
using ShoeStore.Repositories;
using ShoeStore.Repositories.Implementation;
using ShoeStore.Services.Implementation;

namespace ShoeStore.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository, IMapper mapper)
        {
            this._addressRepository = addressRepository;
            this._mapper = mapper;
        }

        public async Task AddAddress(AddressDTO dto)
        {
            if (dto != null)
            {
                await _addressRepository.AddAsync(_mapper.Map<Address>(dto));
            }
        }

        public async Task DeleteAddress(int id)
        {
            if(_addressRepository.GetByIdAsync(id) != null)
            {
                await _addressRepository.DeleteAsync(id);
            }
        }

        public async Task<AddressDTO> UpdateAddress(AddressDTO dto)
        {
            if(await _addressRepository.GetByIdAsync(dto.Id) != null)
            {
                await _addressRepository.UpdateAsync(_mapper.Map<Address>(dto));
                return dto;
            }
            return null;
        }

        public async Task<ICollection<AddressDTO>> GetAddressesByCityAsync(string city)
        {
            ICollection<Address> addresses = await _addressRepository.GetAddressesByCityAsync(city);
            return _mapper.Map<ICollection<AddressDTO>>(addresses);
        }

        public async Task<ICollection<AddressDTO>> GetAllAddress()
        {
            return _mapper.Map<ICollection<AddressDTO>>(await _addressRepository.GetAllAsync());
        }

        public async Task<AddressDTO> GetAddressByIdAsync(int id)
        {
            var address = await _addressRepository.GetByIdAsync(id);
            return _mapper.Map<AddressDTO>(address);
        }
    }
}
