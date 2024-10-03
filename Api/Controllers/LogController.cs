using Api.Controllers;
using Application.UseCases.Logs.Commands.CreateLog;
using GetLogById = Application.UseCases.Logs.Queries.GetLogById;
using Application.UseCases.Logs.Queries.GetLogs;
using GetLogsByType = Application.UseCases.Logs.Queries.GetLogsByType;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Logs.Queries.GetLogsByType;

namespace Challenge02a.Controllers
{
    public class LogController : BaseController
    {
        [HttpPost]
        [Route("Create")]
        [Produces(typeof(CreateLogCommandDto))]
        [ActionName(nameof(Create))]
        public async Task<IActionResult> Create(CreateLogCommandModel model)
        {
            var command = this.Mapper.Map<CreateLogCommand>(model);
            var result = await this.Mediator.Send(command);
            return this.FromResult(result);
        }

        [HttpGet]
        [Route("GetAllLogs")]
        [Produces(typeof(GetLogsQueryDto))]
        [ActionName(nameof(GetAllLogs))]
        public async Task<IActionResult> GetAllLogs()
        {
            var query = new GetLogsQuery { QueryType = GetLogsQueryType.All, Filter = string.Empty };
            var result = await this.Mediator.Send(query);
            return this.FromResult(result);
        }

        [HttpGet]
        [Route("GetLogById")]
        [Produces(typeof(GetLogById.GetLogQueryByIdDto))]
        [ActionName(nameof(GetLogById))]
        public async Task<IActionResult> GetLogById([FromQuery] GetLogById.GetLogsQueryFilterModel model)
        {
            var query = this.Mapper.Map<GetLogById.GetLogQueryById>(model);
            var result = await this.Mediator.Send(query);
            return this.FromResult(result);
        }


        [HttpGet]
        [Route("GetLogsByType")]
        [Produces(typeof(GetLogsByType.GetLogQueryByTypeDto))]
        [ActionName(nameof(GetLogsByType))]
        public async Task<IActionResult> GetLogsByType( [FromQuery] GetLogsByType.GetLogsQueryFilterModel model)
        {
            var query = this.Mapper.Map<GetLogQueryByType>(model);
            var result = await this.Mediator.Send(query);
            return this.FromResult(result);
        }

    }
}
