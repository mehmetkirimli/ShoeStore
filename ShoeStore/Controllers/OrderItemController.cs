using Microsoft.AspNetCore.Mvc;
using ShoeStore.DTO;
using ShoeStore.Services;
using ShoeStore.Services.Implementation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoeStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrderItem([FromBody] OrderItemDTO dto)
        {
            await _orderItemService.AddOrderItemAsync(dto);
            return CreatedAtAction(nameof(GetOrderItemById), new { id = dto.Id }, dto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderItem(int id)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            await _orderItemService.DeleteOrderItemAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetAllOrderItems()
        {
            var orderItems = await _orderItemService.GetAllOrderItemsAsync();
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDTO>> GetOrderItemById(int id)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        [HttpGet("by-order/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetOrderItemsByOrderId(int orderId)
        {
            var orderItems = await _orderItemService.GetOrderItemsByOrderIdAsync(orderId);
            return Ok(orderItems);
        }

        [HttpGet("by-product/{productId}")]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetOrderItemsByProductId(int productId)
        {
            var orderItems = await _orderItemService.GetOrderItemsByProductIdAsync(productId);
            return Ok(orderItems);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrderItem([FromBody] OrderItemDTO dto)
        {
            var existingOrderItem = await _orderItemService.GetOrderItemByIdAsync(dto.Id);
            if (existingOrderItem == null)
            {
                return NotFound();
            }
            await _orderItemService.UpdateOrderItemAsync(dto);
            return NoContent();
        }
    }
}
