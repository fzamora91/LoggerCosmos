
namespace Application.UseCases.Logs.Queries.GetLogById
{
    public class GetLogQueryByIdDto
    {
        public IEnumerable<GetLogQueryByIdValueDto> Logs { get; set; } = new List<GetLogQueryByIdValueDto>();
    }
}
