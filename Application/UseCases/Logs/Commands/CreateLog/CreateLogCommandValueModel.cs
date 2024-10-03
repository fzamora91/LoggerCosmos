using Domain.Enums;

namespace Application.UseCases.Logs.Commands.CreateLog
{
    public class CreateLogCommandValueModel
    {
        public Guid Id { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }
        public LogType? Type { get; set; }
    }
}
