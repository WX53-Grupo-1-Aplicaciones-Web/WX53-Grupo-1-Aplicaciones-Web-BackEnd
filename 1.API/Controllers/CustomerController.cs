using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1.API.Request;
using _1.API.Response;
using _2.Domain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _customerData.GetAllAsync();
            var result = _mapper.Map<List<Customer>, List<CustomerResponse>>(data);
            return Ok(result);
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _customerData.GetByIdAsync(id);
            var result = _mapper.Map<Customer,CustomerResponse>(data);
            return Ok(result);
        }

        // POST: api/Customer
        [HttpPost]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CustomerRequest data)
        {
            var tutorial = _mapper.Map<CustomerRequest, Customer>(data);
            var result = await _customerDomain.UpdateAsync(tutorial, id);
            return Ok(result);
        }


        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerDomain.DeleteAsync(id);
            return Ok(result);
        }
    }
}
