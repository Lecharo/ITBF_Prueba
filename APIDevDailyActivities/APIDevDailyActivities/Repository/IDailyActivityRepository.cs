using APIDevDailyActivities.Models.Dto;

namespace APIDevDailyActivities.Repository
{
    public interface IDailyActivityRepository
    {
        Task<List<DailyActivityDto>> GetDailyActivities();
        Task<DailyActivityDto> GetDailyActivityById(int id);
        Task<DailyActivityDto> CreateUpdate(DailyActivityDto dailyActivityDto);
        Task<bool> DeleteDailyActivity(int id);

    }
}
