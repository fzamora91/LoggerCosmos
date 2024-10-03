using Domain.Enums;

namespace Application.UseCases.Logs.Queries.GetLogsByType
{
    public class GetLogsQueryFilterModel
    {
        public required LogType Filter { get; set; }         
    }
}
