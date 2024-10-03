using MediatR;
using Application.UseCases.Common.Results;
using Microsoft.Extensions.Configuration;
using Application.Common.Interfaces;
using Domain.Entities;
using Application.UseCases.Common.Handlers;
using Application.Common.Utils;

namespace Application.UseCases.Logs.Queries.GetLogs
{
    public class GetLogsQuery : GetLogsQueryTypeModel, IRequest<Result<GetLogsQueryDto>>
    {
        public class GetLogsQueryHandler(IConfiguration configuration,
            IRepository<Log> logRepository) : UseCaseHandler, IRequestHandler<GetLogsQuery, Result<GetLogsQueryDto>>
        {
            public async Task<Result<GetLogsQueryDto>> Handle(GetLogsQuery request, CancellationToken cancellationToken)
            {
                var cosmosDB = new CosmosDB(configuration);

                var logs = await cosmosDB.GetLogs( request.QueryType , request.Filter );

                var logsDto = logs.Select(x => new GetLogsQueryValueDto()
                    { 
                        Id = x.Id,
                        Date = x.Date,
                        Description = x.Description,
                        Type = x.Type
                    });

                var resultData = new GetLogsQueryDto()
                { Logs = logsDto };

                return Succeded(resultData);

            }
        }
    }
}
