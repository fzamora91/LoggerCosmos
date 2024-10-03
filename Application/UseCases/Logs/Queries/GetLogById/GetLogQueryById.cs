using Application.UseCases.Common.Results;
using Microsoft.Extensions.Configuration;
using Application.Common.Interfaces;
using Domain.Entities;
using Application.UseCases.Common.Handlers;
using Application.Common.Utils;
using MediatR;
using Application.UseCases.Logs.Queries.GetLogs;

namespace Application.UseCases.Logs.Queries.GetLogById
{
    public class GetLogQueryById : GetLogsQueryFilterModel , IRequest<Result<GetLogQueryByIdDto>>
    {
        public class GetLogQueryByIdHandler(IConfiguration configuration,
            IRepository<Log> logRepository) : UseCaseHandler, IRequestHandler<GetLogQueryById, Result<GetLogQueryByIdDto>>
        {
            public async Task<Result<GetLogQueryByIdDto>> Handle(GetLogQueryById request, CancellationToken cancellationToken)
            {
                var cosmosDB = new CosmosDB(configuration);

                var logs = await cosmosDB.GetLogs(GetLogsQueryType.ById, request.Filter);

                var logsDto = logs.Select(x => new GetLogQueryByIdValueDto()
                {
                    Id = x.Id,
                    Date = x.Date,
                    Description = x.Description,
                    Type = x.Type
                });

                var resultData = new GetLogQueryByIdDto()
                { Logs = logsDto };

                return Succeded(resultData);
            }
        }
    }
}
