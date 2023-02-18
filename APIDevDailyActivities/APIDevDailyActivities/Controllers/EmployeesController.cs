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
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        protected ResponseDto _response;
        private readonly IMapper _mapper;


        public EmployeesController(IEmployeeRepository employeeRepository, ILogger<EmployeesController> logger)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _response = new ResponseDto();

        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            _logger.LogWarning("Method GetEmployees invoked");
            try
            {
                var lista = await _employeeRepository.GetEmployees();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de Employees Generada";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            _logger.LogWarning("Method GetEmployee invoked");
            var activity = await _employeeRepository.GetEmployeeById(id);

            if (activity == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Employee No Existe";
                return NotFound(_response);
            }
            _response.Result = activity;
            _response.DisplayMessage = "Información del Employee";
            return Ok(_response);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDto employeeDto)
        {
            _logger.LogWarning("Method PutEmployee invoked");
            try
            {
                EmployeeDto model = await _employeeRepository.CreateUpdate(employeeDto);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Actualizar Employee";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeDto employeeDto)
        {
            _logger.LogWarning("Method PostEmployee invoked");
            try
            {
                EmployeeDto model = await _employeeRepository.CreateUpdate(employeeDto);
                _response.Result = model;
                return CreatedAtAction("GetEmployee", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Grabar Employee";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            _logger.LogWarning("Method DeleteEmployee invoked");

            try
            {
                bool estaEliminado = await _employeeRepository.DeleteEmployee(id);
                if (estaEliminado)
                {
                    _response.Result = estaEliminado;
                    _response.DisplayMessage = "Employee Eliminado con Exito";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al Eliminar Employee";
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