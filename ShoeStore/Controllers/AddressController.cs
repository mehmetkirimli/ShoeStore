using Microsoft.AspNetCore.Mvc;
using ShoeStore.DTO;
using ShoeStore.Services.Implementation;

namespace ShoeStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDTO>> GetAddressById(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }

        [HttpPost]
        public async Task<ActionResult<AddressDTO>> CreateAddress(AddressDTO addressDto)
        {
            await _addressService.AddAddress(addressDto);
            return CreatedAtAction(nameof(GetAddressById), new { id = addressDto.Id }, addressDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, AddressDTO addressDto)
        {
            if (id != addressDto.Id)
            {
                return BadRequest();
            }
            await _addressService.UpdateAddress(addressDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            await _addressService.DeleteAddress(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ICollection<AddressDTO>>> GetAddressesByCity(string city)
        {
            var addresses = await _addressService.GetAddressesByCityAsync(city);
            if (addresses == null)
            {
                return NotFound();
            }
            return Ok(addresses);
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<AddressDTO>>> GetAllAddresses()
        {
            return Ok(await _addressService.GetAllAddress());
        }

    }
}
