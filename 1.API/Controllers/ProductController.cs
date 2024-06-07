using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1.API.Request;
using _1.API.Response;
using _2.Domain.ProductDomain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductData _productData;
        private IProductDomain _productDomain;
        private IMapper _mapper;
        public ProductController(IProductData productData, IProductDomain productDomain, IMapper mapper)
        {
            _productData = productData;
            _productDomain = productDomain;
            _mapper = mapper;
        }

        // GET: api/Product
        /// <summary>
        /// Obtiene todos los productos disponibles.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// GET api/product
        /// </remarks>
        /// <returns>
        /// Una lista de productos.
        /// </returns>
        /// <response code="200">Retorna la lista de productos.</response>
        /// <response code="404">Si no se encuentran productos.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _productData.GetAllAsync();
            var result = _mapper.Map<List<Product>, List<ProductResponse>>(data);
            return Ok(result);
        }

        // GET: api/Product/5
        /// <summary>
        /// Obtiene un producto específico por su ID.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// GET api/product/5
        /// </remarks>
        /// <param name="id">El ID del producto a obtener.</param>
        /// <returns>
        /// El producto con el ID especificado.
        /// </returns>
        /// <response code="200">Retorna el producto solicitado.</response>
        /// <response code="404">Si el producto con el ID especificado no se encuentra.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _productData.GetByIdAsync(id);
            var result = _mapper.Map<Product, ProductResponse>(data);
            return Ok(result);
        }

        // POST: api/Product
        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// POST api/product
        /// 
        /// Las reglas para la creación de un producto son las siguientes:
        /// - El nombre del producto no puede tener más de 50 caracteres.
        /// - El precio unitario del producto debe ser positivo y tener un formato decimal válido (hasta dos decimales).
        /// - El stock del producto no puede ser negativo.
        /// - La descripción del producto no puede tener más de 300 caracteres.
        /// </remarks>
        /// <param name="data">El producto a crear.</param>
        /// <returns>
        /// El producto creado.
        /// </returns>
        /// <response code="201">Retorna el producto creado.</response>
        /// <response code="400">Si el producto es nulo, el nombre es demasiado largo, el precio unitario es negativo o tiene un formato inválido, el stock es negativo, o la descripción es demasiado larga.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(ProductRequest data)
        {
            try
            {
                var product = _mapper.Map<ProductRequest, Product>(data);
                var result = await _productDomain.SaveAsync(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        // PUT: api/Product/5
        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// PUT api/product/5
        ///
        /// Las reglas para la actualización de un producto son las siguientes:
        /// - El nombre del producto no puede tener más de 50 caracteres.
        /// - El precio unitario del producto debe ser positivo y tener un formato decimal válido (hasta dos decimales).
        /// - El stock del producto no puede ser negativo.
        /// - La descripción del producto no puede tener más de 300 caracteres.
        /// </remarks>
        /// <param name="id">El ID del producto a actualizar.</param>
        /// <param name="data">Los nuevos datos del producto.</param>
        /// <returns>
        /// El producto actualizado.
        /// </returns>
        /// <response code="200">Retorna el producto actualizado.</response>
        /// <response code="400">Si el producto es nulo, el nombre es demasiado largo, el precio unitario es negativo o tiene un formato inválido, el stock es negativo, o la descripción es demasiado larga.</response>
        /// <response code="404">Si no se encuentra el producto con el ID especificado.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] ProductRequest data)
        {
            var product = _mapper.Map<ProductRequest, Product>(data);
            var result = await _productDomain.UpdateAsync(product, id);
            return Ok(result);
        }

        // DELETE: api/Product/5
        /// <summary>
        /// Elimina un producto existente.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// DELETE api/product/5
        /// </remarks>
        /// <param name="id">El ID del producto a eliminar.</param>
        /// <returns>
        /// Retorna un mensaje de éxito si el producto fue eliminado correctamente.
        /// </returns>
        /// <response code="200">Retorna un mensaje de éxito si el producto fue eliminado correctamente.</response>
        /// <response code="404">Si no se encuentra el producto con el ID especificado.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productDomain.DeleteAsync(id);
            return Ok(result);
        }
    }
}