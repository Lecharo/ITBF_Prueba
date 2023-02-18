using APIDevDailyActivities.Models.Dto;

namespace APIDevDailyActivities.Repository
{
    public interface ILaborRepository
    {
        Task<List<LaborDto>> GetLabors();
        Task<LaborDto> GetLaborById(int id);
        Task<LaborDto> CreateUpdate(LaborDto laborDto);
        Task<bool> DeleteLabor(int id);

    }
}
