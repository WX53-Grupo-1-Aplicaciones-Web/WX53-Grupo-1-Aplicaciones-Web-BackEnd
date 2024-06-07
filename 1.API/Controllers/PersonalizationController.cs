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

        // GET: api/Personalization
        /// <summary>
        /// Obtiene todas las personalizaciones.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// GET api/personalization
        /// </remarks>
        /// <returns>
        /// Una lista de todas las personalizaciones.
        /// </returns>
        /// <response code="200">Retorna la lista de personalizaciones.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _personalizationData.GetAllAsync();
            var result = _mapper.Map<List<Personalization>, List<PersonalizationResponse>>(data);
            return Ok(result);
        }

        // GET: api/Personalization/5
        /// <summary>
        /// Obtiene una personalización específica por su ID.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// GET api/personalization/5
        /// </remarks>
        /// <param name="id">El ID de la personalización a obtener.</param>
        /// <returns>
        /// La personalización con el ID especificado.
        /// </returns>
        /// <response code="200">Retorna la personalización solicitada.</response>
        /// <response code="404">Si la personalización con el ID especificado no se encuentra.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpGet("{id}", Name = "GetPersonalization")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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