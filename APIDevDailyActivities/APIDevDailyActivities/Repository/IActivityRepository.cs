using APIDevDailyActivities.Models;
using APIDevDailyActivities.Models.Dto;

namespace APIDevDailyActivities.Repository
{
    public interface IActivityRepository
    {
        Task<List<ActivityDto>> GetActivities();
        Task<ActivityDto> GetActivityById(int id);
        Task<ActivityDto> CreateUpdate(ActivityDto activityDto);
        Task<bool> DeleteActivity(int id);
    }
}
