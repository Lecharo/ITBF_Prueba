using APIDevDailyActivities.Data;
using APIDevDailyActivities.Models;
using APIDevDailyActivities.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APIDevDailyActivities.Repository
{
    public class LaborRepository : ILaborRepository
    {
        private readonly DataContext _db;
        protected IMapper _mapper;

        public LaborRepository(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<LaborDto> CreateUpdate(LaborDto laborDto)
        {
            Labor labor = _mapper.Map<LaborDto, Labor>(laborDto);
            if (labor.Id > 0)
            {
                _db.Labors.Update(labor);
            }
            else
            {
                await _db.Labors.AddAsync(labor);
            }

            await _db.SaveChangesAsync();

            return _mapper.Map<Labor, LaborDto>(labor);

        }

        public async Task<bool> DeleteLabor(int id)
        {
            try
            {
                Labor labor = await _db.Labors.FindAsync(id);
                if (labor == null)
                {
                    return false;
                }

                _db.Labors.Remove(labor);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<LaborDto> GetLaborById(int id)
        {
            Labor labor = await _db.Labors.FindAsync(id);
            return _mapper.Map<LaborDto>(labor);
        }

        public async Task<List<LaborDto>> GetLabors()
        {
            List<Labor> lista = await _db.Labors.ToListAsync();
            return _mapper.Map<List<LaborDto>>(lista);
        }
    }
}
