using Domain.Enums;

namespace Application.UseCases.Logs.Queries.GetLogsByType
{
    public class GetLogQueryByTypeValueDto
    {
        public Guid Id { get; set; }
        public required string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public LogType? Type { get; set; }
    }
}
