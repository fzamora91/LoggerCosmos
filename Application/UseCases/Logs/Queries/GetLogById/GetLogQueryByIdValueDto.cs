using Domain.Enums;

namespace Application.UseCases.Logs.Queries.GetLogById
{
    public class GetLogQueryByIdValueDto
    {
        public Guid Id { get; set; }
        public required string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public LogType? Type { get; set; }
    }
}
