using APIDevDailyActivities.Models;
using APIDevDailyActivities.Models.Dto;
using AutoMapper;

namespace APIDevDailyActivities
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration( config =>
            {
                config.CreateMap<Activity, ActivityDto>();
                config.CreateMap<ActivityDto, Activity>();

                config.CreateMap<DailyActivity, DailyActivityDto>();
                config.CreateMap<DailyActivityDto, DailyActivity>();

                config.CreateMap<Employee, EmployeeDto>();
                config.CreateMap<EmployeeDto, Employee>();

                config.CreateMap<Labor, LaborDto>();
                config.CreateMap<LaborDto, Labor>();

            });
            return mappingConfig;
        }

    }
}
