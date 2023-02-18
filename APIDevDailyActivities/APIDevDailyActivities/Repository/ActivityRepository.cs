using APIDevDailyActivities.Data;
using APIDevDailyActivities.Models;
using APIDevDailyActivities.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APIDevDailyActivities.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly DataContext _db;
        protected IMapper _mapper;

        public ActivityRepository(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ActivityDto> CreateUpdate(ActivityDto activityDto)
        {
            Activity activity = _mapper.Map<ActivityDto, Activity>(activityDto);
            if (activity.Id > 0)
            {
                _db.Activities.Update(activity);
            }
            else
            {
                await _db.Activities.AddAsync(activity);
            }

            await _db.SaveChangesAsync();

            return _mapper.Map<Activity, ActivityDto>(activity);

        }

        public async Task<bool> DeleteActivity(int id)
        {
            try
            {
                Activity activity = await _db.Activities.FindAsync(id);
                if (activity == null)
                {
                    return false;
                }

                _db.Activities.Remove(activity);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<ActivityDto> GetActivityById(int id)
        {
            Activity activity = await _db.Activities.FindAsync(id);
            return _mapper.Map<ActivityDto>(activity);
        }

        public async Task<List<ActivityDto>> GetActivities()
        {
            List<Activity> lista = await _db.Activities.ToListAsync();
            return _mapper.Map<List<ActivityDto>>(lista);
        }
    }
}

