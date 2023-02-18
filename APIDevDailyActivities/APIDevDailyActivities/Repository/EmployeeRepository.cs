using APIDevDailyActivities.Data;
using APIDevDailyActivities.Models;
using APIDevDailyActivities.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APIDevDailyActivities.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _db;
        protected IMapper _mapper;

        public EmployeeRepository(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> CreateUpdate(EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<EmployeeDto, Employee>(employeeDto);
            if (employee.Id > 0)
            {
                _db.Employees.Update(employee);
            }
            else
            {
                await _db.Employees.AddAsync(employee);
            }

            await _db.SaveChangesAsync();

            return _mapper.Map<Employee, EmployeeDto>(employee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            try
            {
                Employee employee = await _db.Employees.FindAsync(id);
                if (employee == null)
                {
                    return false;
                }

                _db.Employees.Remove(employee);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            Employee employee = await _db.Employees.FindAsync(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            List<Employee> lista = await _db.Employees.ToListAsync();
            return _mapper.Map<List<EmployeeDto>>(lista);
        }
    }
}
