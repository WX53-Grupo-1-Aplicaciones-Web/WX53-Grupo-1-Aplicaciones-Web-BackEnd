using _1.API.Request;
using _1.API.Response;
using _2.Domain.OrderDomain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderData _orderData;
        private IOrderDomain _orderDomain;
        private IMapper _mapper;
    
        public OrderController(IOrderData orderData, IOrderDomain orderDomain, IMapper mapper)
        {
            _orderData = orderData;
            _orderDomain = orderDomain;
            _mapper = mapper;
        }
        
        // GET: api/Order
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _orderData.GetAllAsync();
            var result = _mapper.Map<List<Order>, List<OrderResponse>>(data);
            return Ok(result);
        }
        
        // GET: api/Order/5
        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _orderData.GetByIdAsync(id);
            var result = _mapper.Map<Order, OrderResponse>(data);
            return Ok(result);
        }
        
        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> PostAsync(OrderRequest data)
        {
            try
            {
                var order = _mapper.Map<OrderRequest, Order>(data);
                var result = await _orderDomain.SaveAsync(order);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OrderRequest data)
        {
            var order = _mapper.Map<OrderRequest, Order>(data);
            var result = await _orderDomain.UpdateAsync(order, id);
            return Ok(result);
        }
        
        // DELETE: api/order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderDomain.DeleteAsync(id);
            return Ok(result);
        }
    }
}