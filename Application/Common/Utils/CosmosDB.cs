using Application.UseCases.Logs.Queries.GetLogs;
using Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace Application.Common.Utils
{
    public class CosmosDB (IConfiguration configuration)
    {
        public string ConnectionString { get; set; } = configuration.GetSection("CosmosDB:ConnectionString").Value ?? string.Empty;
        public string Key { get; set; } = configuration.GetSection("CosmosDB:Key").Value ?? string.Empty;
        public string DatabaseName { get; set; } = configuration.GetSection("CosmosDB:DatabaseName").Value ?? string.Empty;
        public string ContainerName { get; set; } = configuration.GetSection("CosmosDB:ContainerName").Value ?? string.Empty;


        public async Task InsertLogOperation(Log log)
        {
            var cosmosClient = new CosmosClient(ConnectionString, Key);
            Database db = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);
            Container container =  await db.CreateContainerIfNotExistsAsync (ContainerName, $"/LogId");

            var _log = new Log();

            _log.Id = log.Id;
            _log.Date = DateTime.UtcNow;
            _log.Type = log.Type;
            _log.LogId = log.Id.ToString();
            _log.Description = log.Description;

            await container.CreateItemAsync<Log>(_log);
        }

        public async Task<IEnumerable<Log>> GetLogs(  GetLogsQueryType queryType , string filter )
        {
            var cosmosClient = new CosmosClient(ConnectionString, Key);
            Database db = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);
            Container container = await db.CreateContainerIfNotExistsAsync(ContainerName, $"/LogId");

            string sqlQueryText = string.Empty;

            switch (queryType)
            {
                case GetLogsQueryType.All:
                    sqlQueryText = "SELECT * FROM c ";
                    break;
                case GetLogsQueryType.ById:
                    sqlQueryText = $"SELECT * FROM c WHERE c.id = '{ filter }'";
                    break;
                case GetLogsQueryType.ByType:
                    sqlQueryText = $"SELECT * FROM c WHERE c.Type = { filter }";                    
                    break;
            }

            QueryDefinition queryDefinition = new QueryDefinition( sqlQueryText );
            FeedIterator<Log> queryFeedIterator = container.GetItemQueryIterator<Log>(queryDefinition);

            List<Log> logs = new List<Log>();

            while (queryFeedIterator.HasMoreResults)
            {
                FeedResponse<Log> currentResultSet = await queryFeedIterator.ReadNextAsync();
                foreach (Log log in currentResultSet)
                {
                    logs.Add(log);
                }
            }

            return logs;

        }
    }
}
