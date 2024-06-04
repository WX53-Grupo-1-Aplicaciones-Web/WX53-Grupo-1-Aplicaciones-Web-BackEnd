using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1.API.Request;
using _1.API.Response;
using _2.Domain.ArtisanDomain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ArtisanController : ControllerBase
    {
        private IArtisanData _artisanData;
        private IArtisanDomain _artisanDomain;
        private IMapper _mapper;
        public ArtisanController(IArtisanData artisanData, IArtisanDomain artisanDomain, IMapper mapper)
        {
            _artisanData= artisanData;
            _artisanDomain = artisanDomain;
            _mapper = mapper;
        }
        // GET: api/Customer
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _artisanData.GetAllAsync();
            var result = _mapper.Map<List<Artisan>, List<ArtisanResponse>>(data);
            return Ok(result);
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "GetArtisan")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _artisanData.GetByIdAsync(id);
            var result = _mapper.Map<Artisan,ArtisanResponse>(data);
            return Ok(result);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> PostAsync( ArtisanRequest data)
        {
            try
            {
                var artisan = _mapper.Map<ArtisanRequest, Artisan>(data);
                var result = await _artisanDomain.SaveAsync(artisan);
                return Ok(result);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ArtisanRequest data)
        {
            var artisan = _mapper.Map<ArtisanRequest, Artisan>(data);
            var result = await _artisanDomain.UpdateAsync(artisan, id);
            return Ok(result);
        }


        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _artisanDomain.DeleteAsync(id);
            return Ok(result);
        }
    }
}