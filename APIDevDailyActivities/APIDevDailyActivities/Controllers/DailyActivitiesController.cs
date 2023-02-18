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
using System.Diagnostics;

namespace APIDevDailyActivities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyActivitiesController : ControllerBase
    {
        private readonly ILogger<DailyActivitiesController> _logger;
        private readonly IDailyActivityRepository _dailyActivityRepository;
        protected ResponseDto _response;
        private readonly IMapper _mapper;

        public DailyActivitiesController(IDailyActivityRepository dailyActivityRepository, ILogger<DailyActivitiesController> logger)
        {
            _logger = logger;
            _dailyActivityRepository = dailyActivityRepository;
            _response = new ResponseDto();
        }

        // GET: api/DailyActivities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyActivity>>> GetDailyActivities()
        {
            _logger.LogWarning("Method GetDailyActivities invoked");
            try
            {
                var lista = await _dailyActivityRepository.GetDailyActivities();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de DailyActivities Generada";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        // GET: api/DailyActivities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DailyActivity>> GetDailyActivity(int id)
        {
            _logger.LogWarning("Method GetDailyActivity invoked");
            var dailyActivity = await _dailyActivityRepository.GetDailyActivityById(id);

            if (dailyActivity == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "DailyActivity No Existe";
                return NotFound(_response);
            }
            _response.Result = dailyActivity;
            _response.DisplayMessage = "Información de DailyActivity";
            return Ok(_response);
        }

        // PUT: api/DailyActivities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailyActivity(int id, DailyActivityDto dailyActivityDto)
        {
            _logger.LogWarning("Method PutDailyActivities invoked");
            try
            {
                DailyActivityDto model = await _dailyActivityRepository.CreateUpdate(dailyActivityDto);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Actualizar DailyActivity";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/DailyActivities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DailyActivity>> PostDailyActivity(DailyActivityDto dailyActivityDto)
        {
            _logger.LogWarning("Method PostDailyActivities invoked");
            try
            {
                DailyActivityDto model = await _dailyActivityRepository.CreateUpdate(dailyActivityDto);
                _response.Result = model;
                return CreatedAtAction("GetDailyActivity", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Grabar DailyActivity";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/DailyActivities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyActivity(int id)
        {
            _logger.LogWarning("Method DeleteDailyActivities invoked");
            try
            {
                bool estaEliminado = await _dailyActivityRepository.DeleteDailyActivity(id);
                if (estaEliminado)
                {
                    _response.Result = estaEliminado;
                    _response.DisplayMessage = "DailyActivity Eliminada con Exito";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al Eliminar DailyActivity";
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