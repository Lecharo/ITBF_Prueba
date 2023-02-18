using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDevDailyActivities.Data;
using APIDevDailyActivities.Models;
using APIDevDailyActivities.Repository;
using AutoMapper;
using APIDevDailyActivities.Models.Dto;

namespace APIDevDailyActivities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly ILogger<ActivitiesController> _logger;
        private readonly IActivityRepository _activityRepository;
        protected ResponseDto _response;
        private readonly IMapper _mapper;

        public ActivitiesController(IActivityRepository activityRepository, ILogger<ActivitiesController> logger)
        {
            _logger = logger;
            _activityRepository = activityRepository;
            _response = new ResponseDto();
        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            _logger.LogWarning("Method GetActivities invoked");
            try
            {
                var lista = await _activityRepository.GetActivities();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de Activities Generada";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(int id)
        {
            _logger.LogWarning("Method GetActivity invoked");
            var activity = await _activityRepository.GetActivityById(id);

            if (activity == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Activity No Existe";
                return NotFound(_response);
            }
            _response.Result = activity;
            _response.DisplayMessage = "Información de la Activity";
            return Ok(_response);
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, ActivityDto activityDto)
        {
            _logger.LogWarning("Method PutActivity invoked");

            try
            {
                ActivityDto model = await _activityRepository.CreateUpdate(activityDto);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Actualizar el Activity";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity(ActivityDto activityDto)
        {
            _logger.LogWarning("Method PostActivity invoked");

            try
            {
                ActivityDto model = await _activityRepository.CreateUpdate(activityDto);
                _response.Result = model;
                return CreatedAtAction("GetActivity", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Grabar Activity";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            _logger.LogWarning("Method DeleteActivity invoked");

            try
            {
                bool estaEliminado = await _activityRepository.DeleteActivity(id);
                if (estaEliminado)
                {
                    _response.Result = estaEliminado;
                    _response.DisplayMessage = "Activity Eliminada con Exito";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al Eliminar Activity";
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
