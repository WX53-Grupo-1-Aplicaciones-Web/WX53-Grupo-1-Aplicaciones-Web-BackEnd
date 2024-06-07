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
        /// <summary>
        /// Obtiene todas las órdenes disponibles.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// GET api/order
        /// </remarks>
        /// <returns>
        /// Una lista de órdenes.
        /// </returns>
        /// <response code="200">Retorna la lista de órdenes.</response>
        /// <response code="404">Si no se encuentran órdenes.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAsync()
        {
            var data = await _orderData.GetAllAsync();
            var result = _mapper.Map<List<Order>, List<OrderResponse>>(data);
            return Ok(result);
        }
        
        // GET: api/Order/5
        /// <summary>
        /// Obtiene una orden específica por su ID.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// GET api/order/5
        /// </remarks>
        /// <param name="id">El ID de la orden a obtener.</param>
        /// <returns>
        /// La orden con el ID especificado.
        /// </returns>
        /// <response code="200">Retorna la orden solicitada.</response>
        /// <response code="404">Si la orden con el ID especificado no se encuentra.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpGet("{id}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _orderData.GetByIdAsync(id);
            var result = _mapper.Map<Order, OrderResponse>(data);
            return Ok(result);
        }
        
        // POST: api/Order
        /// <summary>
        /// Crea una nueva orden.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// POST api/order
        /// </remarks>
        /// <param name="data">La orden a crear.</param>
        /// <returns>
        /// La orden creada.
        /// </returns>
        /// <response code="201">Retorna la orden creada.</response>
        /// <response code="400">Si la orden es nula o los datos proporcionados son inválidos.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(OrderRequest data)
        {
            if (string.IsNullOrEmpty(data.DeliveryAddress))
            {
                return BadRequest("Delivery address cannot be null or empty.");
            }

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
        /// <summary>
        /// Actualiza una orden existente.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// PUT api/order/5
        /// </remarks>
        /// <param name="id">El ID de la orden a actualizar.</param>
        /// <param name="data">Los nuevos datos de la orden.</param>
        /// <returns>
        /// La orden actualizada.
        /// </returns>
        /// <response code="200">Retorna la orden actualizada.</response>
        /// <response code="400">Si la orden es nula o los datos proporcionados son inválidos.</response>
        /// <response code="404">Si no se encuentra la orden con el ID especificado.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] OrderRequest data)
        {
            if (string.IsNullOrEmpty(data.DeliveryAddress))
            {
                return BadRequest("Delivery address cannot be null or empty.");
            }

            var order = _mapper.Map<OrderRequest, Order>(data);
            var result = await _orderDomain.UpdateAsync(order, id);
            return Ok(result);
        }
        
        // DELETE: api/Order/5
        /// <summary>
        /// Elimina una orden existente.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// DELETE api/order/5
        /// </remarks>
        /// <param name="id">El ID de la orden a eliminar.</param>
        /// <returns>
        /// Retorna un mensaje de éxito si la orden fue eliminada correctamente.
        /// </returns>
        /// <response code="200">Retorna un mensaje de éxito si la orden fue eliminada correctamente.</response>
        /// <response code="404">Si no se encuentra la orden con el ID especificado.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderDomain.DeleteAsync(id);
            return Ok(result);
        }
    }
}