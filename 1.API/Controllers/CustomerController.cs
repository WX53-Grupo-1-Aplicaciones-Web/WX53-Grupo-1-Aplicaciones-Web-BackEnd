using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using _1.API.Request;
using _1.API.Response;
using _2.Domain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace _1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerData _customerData;
        private ICustomerDomain _customerDomain;
        private IMapper _mapper;
        public CustomerController(ICustomerData customerData, ICustomerDomain customerDomain, IMapper mapper)
        {
            _customerData= customerData;
            _customerDomain = customerDomain;
            _mapper = mapper;
        }
        
        // GET: api/Customer
        /// <summary>
        /// Obtiene todos los clientes disponibles.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// GET api/customer
        /// </remarks>
        /// <returns>
        /// Una lista de clientes.
        /// </returns>
        /// <response code="200">Retorna la lista de clientes.</response>
        /// <response code="404">Si no se encuentran clientes.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _customerData.GetAllAsync();
            var result = _mapper.Map<List<Customer>, List<CustomerResponse>>(data);
            return Ok(result);
        }

        // GET: api/Customer/5
        /// <summary>
        /// Obtiene un cliente específico por su ID.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// GET api/customer/5
        /// </remarks>
        /// <param name="id">El ID del cliente a obtener.</param>
        /// <returns>
        /// El cliente con el ID especificado.
        /// </returns>
        /// <response code="200">Retorna el cliente solicitado.</response>
        /// <response code="404">Si el cliente con el ID especificado no se encuentra.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public async Task<IActionResult> Get(int id)
        {
            var data = await _customerData.GetByIdAsync(id);
            var result = _mapper.Map<Customer,CustomerResponse>(data);
            return Ok(result);
        }

        // POST: api/Customer
        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// POST api/customer
        /// 
        /// Las reglas para la creación de un cliente son las siguientes:
        /// - El correo electrónico debe tener un formato válido.
        /// - El número de teléfono debe tener un formato válido.
        /// - El correo electrónico no debe estar vinculado con una cuenta existente.
        /// </remarks>
        /// <param name="data">El cliente a crear.</param>
        /// <returns>
        /// El cliente creado.
        /// </returns>
        /// <response code="201">Retorna el cliente creado.</response>
        /// <response code="400">Si el cliente es nulo, el correo electrónico o el número de teléfono no tienen un formato válido, o el correo electrónico está vinculado con una cuenta existente.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> PostAsync( CustomerRequest data)
        {
            try
            {
                var tutorial = _mapper.Map<CustomerRequest, Customer>(data);
                var result = await _customerDomain.SaveAsync(tutorial);
                return Ok(result);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/Customer/5
        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// PUT api/customer/5
        /// </remarks>
        /// <param name="id">El ID del cliente a actualizar.</param>
        /// <param name="data">Los nuevos datos del cliente.</param>
        /// <returns>
        /// El cliente actualizado.
        /// </returns>
        /// <response code="200">Retorna el cliente actualizado.</response>
        /// <response code="400">Si el cliente es nulo o el ID no coincide con el del cliente.</response>
        /// <response code="404">Si no se encuentra el cliente con el ID especificado.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]     
        public async Task<IActionResult> Put(int id, [FromBody] CustomerRequest data)
        {
            var tutorial = _mapper.Map<CustomerRequest, Customer>(data);
            var result = await _customerDomain.UpdateAsync(tutorial, id);
            return Ok(result);
        }


        // DELETE: api/Customer/5
        /// <summary>
        /// Marca un cliente como inactivo.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// DELETE api/customer/5
        /// </remarks>
        /// <param name="id">El ID del cliente a marcar como inactivo.</param>
        /// <returns>
        /// Retorna un mensaje de éxito si el cliente fue marcado como inactivo correctamente.
        /// </returns>
        /// <response code="200">Retorna un mensaje de éxito si el cliente fue marcado como inactivo correctamente.</response>
        /// <response code="404">Si no se encuentra el cliente con el ID especificado.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerDomain.DeleteAsync(id);
            return Ok(result);
        }
    }
}
