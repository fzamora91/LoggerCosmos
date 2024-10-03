using Domain.Enums;

namespace Application.UseCases.Logs.Queries.GetLogs
{
    public class GetLogsQueryValueDto
    {
        public Guid Id { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }
        public LogType? Type { get; set; }
    }
}
