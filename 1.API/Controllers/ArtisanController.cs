using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
        // GET: api/Artisan
        /// <summary>
        /// Obtiene todos los artesanos disponibles.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// GET api/artisan
        /// </remarks>
        /// <returns>
        /// Una lista de artesanos.
        /// </returns>
        /// <response code="200">Retorna la lista de artesanos.</response>
        /// <response code="404">Si no se encuentran artesanos.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _artisanData.GetAllAsync();
            var result = _mapper.Map<List<Artisan>, List<ArtisanResponse>>(data);
            return Ok(result);
        }

        // GET: api/Customer/5
        // GET: api/Artisan/5
        /// <summary>
        /// Obtiene un artesano específico por su ID.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// GET api/artisan/5
        /// </remarks>
        /// <param name="id">El ID del artesano a obtener.</param>
        /// <returns>
        /// El artesano con el ID especificado.
        /// </returns>
        /// <response code="200">Retorna el artesano solicitado.</response>
        /// <response code="404">Si el artesano con el ID especificado no se encuentra.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpGet("{id}", Name = "GetArtisan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _artisanData.GetByIdAsync(id);
            var result = _mapper.Map<Artisan,ArtisanResponse>(data);
            return Ok(result);
        }

        // POST: api/Artisan
        /// <summary>
        /// Crea un nuevo artesano.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// POST api/artisan
        /// 
        /// Las reglas para la creación de un artesano son las siguientes:
        /// - El correo electrónico debe tener un formato válido.
        /// - El nombre debe tener entre 2 y 50 caracteres.
        /// - El apellido debe tener entre 2 y 50 caracteres.
        /// - El correo electrónico no debe estar vinculado con una cuenta existente.
        /// - El número de teléfono no debe estar vinculado con una cuenta existente.
        /// </remarks>
        /// <param name="data">El artesano a crear.</param>
        /// <returns>
        /// El artesano creado.
        /// </returns>
        /// <response code="201">Retorna el artesano creado.</response>
        /// <response code="400">Si el artesano es nulo, el correo electrónico o el número de teléfono no tienen un formato válido, o el correo electrónico o el número de teléfono están vinculados con una cuenta existente.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
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

        // PUT: api/Artisan/5
        /// <summary>
        /// Actualiza un artesano existente.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// PUT api/artisan/5
        ///
        /// Las reglas para la actualización de un artesano son las siguientes:
        /// - El correo electrónico debe tener un formato válido.
        /// - El nombre debe tener entre 2 y 50 caracteres.
        /// - El apellido debe tener entre 2 y 50 caracteres.
        /// - El correo electrónico no debe estar vinculado con una cuenta existente.
        /// - El número de teléfono no debe estar vinculado con una cuenta existente.
        /// </remarks>
        /// <param name="id">El ID del artesano a actualizar.</param>
        /// <param name="data">Los nuevos datos del artesano.</param>
        /// <returns>
        /// El artesano actualizado.
        /// </returns>
        /// <response code="200">Retorna el artesano actualizado.</response>
        /// <response code="400">Si el artesano es nulo, el correo electrónico o el número de teléfono no tienen un formato válido, o el correo electrónico o el número de teléfono están vinculados con una cuenta existente.</response>
        /// <response code="404">Si no se encuentra el artesano con el ID especificado.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] ArtisanRequest data)
        {
            var artisan = _mapper.Map<ArtisanRequest, Artisan>(data);
            var result = await _artisanDomain.UpdateAsync(artisan, id);
            return Ok(result);
        }


        // DELETE: api/Artisan/5
        /// <summary>
        /// Elimina un artesano existente.
        /// </summary>
        /// <remarks>
        /// Ejemplo:
        /// DELETE api/artisan/5
        /// </remarks>
        /// <param name="id">El ID del artesano a eliminar.</param>
        /// <returns>
        /// Retorna un mensaje de éxito si el artesano fue eliminado correctamente.
        /// </returns>
        /// <response code="200">Retorna un mensaje de éxito si el artesano fue eliminado correctamente.</response>
        /// <response code="404">Si no se encuentra el artesano con el ID especificado.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _artisanDomain.DeleteAsync(id);
            return Ok(result);
        }
    }
}