using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDevDailyActivities.Data;
using APIDevDailyActivities.Models;
using APIDevDailyActivities.Models.Dto;
using APIDevDailyActivities.Repository;
using AutoMapper;

namespace APIDevDailyActivities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaborsController : ControllerBase
    {
        private readonly ILogger<LaborsController> _logger;
        private readonly ILaborRepository _laborRepository;
        protected ResponseDto _response;
        private readonly IMapper _mapper;

        public LaborsController(ILaborRepository laborRepository, ILogger<LaborsController> logger)
        {
            _logger = logger;
            _laborRepository = laborRepository;
            _response = new ResponseDto();
        }

        // GET: api/Labors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Labor>>> GetLabors()
        {
            _logger.LogWarning("Method GetLabors invoked");
            try
            {
                var lista = await _laborRepository.GetLabors();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de Labors Generada";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        // GET: api/Labors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Labor>> GetLabor(int id)
        {
            _logger.LogWarning("Method GetLabor invoked");
            var labor = await _laborRepository.GetLaborById(id);

            if (labor == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Labor No Existe";
                return NotFound(_response);
            }
            _response.Result = labor;
            _response.DisplayMessage = "Información de la Labor";
            return Ok(_response);
        }

        // PUT: api/Labors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLabor(int id, LaborDto laborDto)
        {
            _logger.LogWarning("Method PutLabor invoked");
            try
            {
                LaborDto model = await _laborRepository.CreateUpdate(laborDto);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Actualizar la Labor";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/Labors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Labor>> PostLabor(LaborDto laborDto)
        {
            _logger.LogWarning("Method PostLabor invoked");
            try
            {
                LaborDto model = await _laborRepository.CreateUpdate(laborDto);
                _response.Result = model;
                return CreatedAtAction("GetLabor", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Grabar Labor";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/Labors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLabor(int id)
        {
            _logger.LogWarning("Method DeleteLabor invoked");
            try
            {
                bool estaEliminado = await _laborRepository.DeleteLabor(id);
                if (estaEliminado)
                {
                    _response.Result = estaEliminado;
                    _response.DisplayMessage = "Labor Eliminada con Exito";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al Eliminar Labor";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
    }
}
