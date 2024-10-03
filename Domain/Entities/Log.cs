using Domain.Enums;

namespace Domain.Entities
{
    public class Log : BaseEntity
    {
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public LogType? Type { get; set; }        

        public string? LogId { get; set; }
    }
}
