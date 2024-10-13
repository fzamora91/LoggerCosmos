using Application.UseCases.Logs.Commands.CreateLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ILogService
    {
        Task<CreateLogCommandDto> LogInformationAsync(string id, string description);
    }
}
