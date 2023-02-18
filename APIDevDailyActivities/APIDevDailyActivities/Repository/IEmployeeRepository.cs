using APIDevDailyActivities.Models.Dto;

namespace APIDevDailyActivities.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<EmployeeDto> CreateUpdate(EmployeeDto employeeDto);
        Task<bool> DeleteEmployee(int id);

    }
}
