using Application.Common.Interfaces;
using Application.Common.Utils;
using Application.UseCases.Logs.Commands.CreateLog;
using Azure.Core;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LogService: ILogService
    {
        public IConfiguration conf;
        public LogService(IConfiguration configuration)
        {
            conf = configuration;
        }


        public async Task<CreateLogCommandDto> LogInformationAsync(string id, string description)
        {
            var cosmosDB = new CosmosDB(conf);

            Guid guid = Guid.Parse(id);

            var log = new Log
            {
                Id = guid,
                Description = description,
                Date = DateTime.Now,
                Type = LogType.Information
            };

            await cosmosDB.InsertLogOperation(log);


            var resultData = new CreateLogCommandDto { Created = true };

            return resultData;

        }
    }
}
