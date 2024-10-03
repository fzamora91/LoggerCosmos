namespace Application.UseCases.Logs.Queries.GetLogs
{
    public class GetLogsQueryTypeModel
    {
        public GetLogsQueryType QueryType { get; set; }

        public string Filter { get; set; } = string.Empty;
    }
}
