using Domain.DTO.Requests;
using Domain.DTO.Responses;
using Domain.Entities;
using Domain.Interfaces.Services.Transactional;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServiceTransactional _orderServiceTransactional;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderServiceTransactional orderServiceTransactional, ILogger<OrderController> logger)
        {
            _orderServiceTransactional = orderServiceTransactional;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] OrderCreateRequest request)
        {
            try
            {
                var result = await _orderServiceTransactional.CreateOrderAsync(request);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("today")]
        public async Task<ActionResult<IEnumerable<TodayOrderResponse>>> GetTodayOrders()
        {
            try
            {
                var orders = await _orderServiceTransactional.GetTodayOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving today's orders.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}