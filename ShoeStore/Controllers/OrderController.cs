using Microsoft.AspNetCore.Mvc;
using ShoeStore.DTO;
using ShoeStore.Entities;
using ShoeStore.Enumarations;
using ShoeStore.Services.Implementation;

namespace ShoeStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
       

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody]OrderDTO dto)
        {
            await _orderService.AddOrderAsync(dto);
            return CreatedAtAction(nameof(GetOrderById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDTO orderDTO)
        {
            if (id != orderDTO.Id)
            {
                return BadRequest();
            }
            await _orderService.UpdateOrderAsync(orderDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrderAsync();
            return Ok(orders);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrderListByStatus(OrderStatus status)
        {
            var orders = await _orderService.GetOrderListByStatus(status);
            return Ok(orders);
        }


    }
}
