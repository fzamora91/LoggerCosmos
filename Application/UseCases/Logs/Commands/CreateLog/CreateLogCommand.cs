using Application.Common.Utils;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;


namespace Application.UseCases.Logs.Commands.CreateLog
{
    public class CreateLogCommand: CreateLogCommandModel, IRequest<Result<CreateLogCommandDto>>
    {
        public class CreateLogCommandHandler(IConfiguration configuration)
            : UseCaseHandler, IRequestHandler<CreateLogCommand, Result<CreateLogCommandDto>>
        {
            public async Task<Result<CreateLogCommandDto>> Handle(CreateLogCommand request, CancellationToken cancellationToken)
            {

                var cosmosDB = new CosmosDB(configuration);

                var log = new Log
                {
                    Id = request.Log.Id,
                    Description = request?.Log?.Description,
                    Date = DateTime.Now,
                    Type = request.Log.Type
                };

                await cosmosDB.InsertLogOperation(log);

                var resultData = new CreateLogCommandDto { Created = true };

                return Succeded(resultData);
            }
        }
                
    }
}
