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
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _productData.GetAllAsync();
            var result = _mapper.Map<List<Product>, List<ProductResponse>>(data);
            return Ok(result);
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _productData.GetByIdAsync(id);
            var result = _mapper.Map<Product, ProductResponse>(data);
            return Ok(result);
        }

        // POST: api/Product
        [HttpPost]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductRequest data)
        {
            var product = _mapper.Map<ProductRequest, Product>(data);
            var result = await _productDomain.UpdateAsync(product, id);
            return Ok(result);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productDomain.DeleteAsync(id);
            return Ok(result);
        }
    }
}