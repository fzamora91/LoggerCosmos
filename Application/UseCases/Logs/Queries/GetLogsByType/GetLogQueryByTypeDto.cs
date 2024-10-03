
namespace Application.UseCases.Logs.Queries.GetLogsByType
{
    public class GetLogQueryByTypeDto
    {
        public IEnumerable<GetLogQueryByTypeValueDto> Logs { get; set; } = new List<GetLogQueryByTypeValueDto>();
    }
}
