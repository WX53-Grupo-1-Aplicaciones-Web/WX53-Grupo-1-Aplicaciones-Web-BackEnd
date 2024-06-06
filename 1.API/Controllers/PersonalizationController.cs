using _1.API.Response;
using _2.Domain.PersonalizationDomain;
using _3.Data;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalizationController : ControllerBase
    {
        private IPersonalizationData _personalizationData;
        private IPersonalizationDomain _personalizationDomain;
        private IMapper _mapper;

        public PersonalizationController(IPersonalizationData personalizationData, IPersonalizationDomain personalizationDomain, IMapper mapper)
        {
            _personalizationData = personalizationData;
            _personalizationDomain = personalizationDomain;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _personalizationData.GetAllAsync();
            var result = _mapper.Map<List<Personalization>, List<PersonalizationResponse>>(data);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "Get Personalization")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _personalizationData.GetByIdAsync(id);

            if (data == null)
            {
                return NotFound("Personazation with id " + id + " not found");
            }

            var result = _mapper.Map<Personalization, PersonalizationResponse>(data);
            return Ok(result);
        }
    }
}