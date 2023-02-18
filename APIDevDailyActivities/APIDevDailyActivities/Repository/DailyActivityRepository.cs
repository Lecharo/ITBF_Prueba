
using APIDevDailyActivities.Data;
using APIDevDailyActivities.Models;
using APIDevDailyActivities.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APIDevDailyActivities.Repository
{
    public class DailyActivityRepository : IDailyActivityRepository
    {
        private readonly DataContext _db;
        protected IMapper _mapper;

        public DailyActivityRepository(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<DailyActivityDto> CreateUpdate(DailyActivityDto dailyActivityDto)
        {
            DailyActivity dailyActivity = _mapper.Map<DailyActivityDto, DailyActivity>(dailyActivityDto);
            if (dailyActivity.Id > 0)
            {
                _db.DailyActivities.Update(dailyActivity);
            }
            else
            {
                await _db.DailyActivities.AddAsync(dailyActivity);
            }

            await _db.SaveChangesAsync();

            return _mapper.Map<DailyActivity, DailyActivityDto>(dailyActivity);

        }

        public async Task<bool> DeleteDailyActivity(int id)
        {
            try
            {
                DailyActivity dailyActivity = await _db.DailyActivities.FindAsync(id);
                if (dailyActivity == null)
                {
                    return false;
                }

                _db.DailyActivities.Remove(dailyActivity);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<DailyActivityDto> GetDailyActivityById(int id)
        {
            DailyActivity dailyActivity = await _db.DailyActivities.FindAsync(id);
            return _mapper.Map<DailyActivityDto>(dailyActivity);
        }

        public async Task<List<DailyActivityDto>> GetDailyActivities()
        {
            List<DailyActivity> lista = await _db.DailyActivities.ToListAsync();
            return _mapper.Map<List<DailyActivityDto>>(lista);
        }
    }
}
