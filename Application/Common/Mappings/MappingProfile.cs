using AutoMapper;
using Application.UseCases.Logs.Commands.CreateLog;
using GetLogsByType = Application.UseCases.Logs.Queries.GetLogsByType;
using GetLogById = Application.UseCases.Logs.Queries.GetLogById;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            this.CreateMap<CreateLogCommandModel, CreateLogCommand>();
            this.CreateMap<GetLogById.GetLogsQueryFilterModel, GetLogById.GetLogQueryById>();
            this.CreateMap<GetLogsByType.GetLogsQueryFilterModel, GetLogsByType.GetLogQueryByType>();
        }
    }
}
