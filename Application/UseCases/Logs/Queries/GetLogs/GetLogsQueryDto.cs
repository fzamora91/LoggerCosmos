
namespace Application.UseCases.Logs.Queries.GetLogs
{
    public class GetLogsQueryDto
    {
        public IEnumerable<GetLogsQueryValueDto> Logs { get; set; } = new List<GetLogsQueryValueDto>();
    }
}
