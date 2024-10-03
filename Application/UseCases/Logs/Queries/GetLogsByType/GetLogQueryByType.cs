using Application.UseCases.Common.Results;
using Microsoft.Extensions.Configuration;
using Application.Common.Interfaces;
using Domain.Entities;
using Application.UseCases.Common.Handlers;
using Application.Common.Utils;
using MediatR;
using Application.UseCases.Logs.Queries.GetLogs;


namespace Application.UseCases.Logs.Queries.GetLogsByType
{
    public class GetLogQueryByType : GetLogsQueryFilterModel , IRequest<Result<GetLogQueryByTypeDto>>
    {
        public class GetLogQueryByTypeHandler(IConfiguration configuration,
            IRepository<Log> logRepository) : UseCaseHandler, IRequestHandler<GetLogQueryByType, Result<GetLogQueryByTypeDto>>
        {
            public async Task<Result<GetLogQueryByTypeDto>> Handle(GetLogQueryByType request, CancellationToken cancellationToken)
            {
                var cosmosDB = new CosmosDB(configuration);

                var logs = await cosmosDB.GetLogs(GetLogsQueryType.ByType, ((int)request.Filter).ToString());

                var logsDto = logs.Select(x => new GetLogQueryByTypeValueDto()
                {
                    Id = x.Id,
                    Date = x.Date,
                    Description = x.Description,
                    Type = x.Type
                });

                var resultData = new GetLogQueryByTypeDto()
                { Logs = logsDto };

                return Succeded(resultData);
            }
        }
    }
}
