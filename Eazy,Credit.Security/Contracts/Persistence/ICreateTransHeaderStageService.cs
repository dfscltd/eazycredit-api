using Eazy.Credit.Security.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Contracts.Persistence
{
    public interface ICreateTransHeaderStageService
    {
        Task<ViewAPIResponse<CreateTransHeaderStageDto>> CreateTransHeaderStage(CreateTransHeaderStageDto request);
        Task<ViewAPIResponse<TransHeaderStageResultDto>> FindTransHeaderStageById(string transId, string workflow, string workflowLevel);
        Task<ViewAPIResponse<CreateTransHeaderStageDto>> EditTransHeaderStage(CreateTransHeaderStageDto request);
        Task<ViewAPIResponse<string>> DeleteTransHeaderStage(string transId, string workflow, string workflowLevel);
        Task<ViewAPIResponse<List<TransHeaderStageResultDto>>> FindTransHeaderStageByUserId(string userId);
    }
}
