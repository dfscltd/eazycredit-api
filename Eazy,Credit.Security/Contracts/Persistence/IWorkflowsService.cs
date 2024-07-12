using Eazy.Credit.Security.Dtos;


namespace Eazy.Credit.Security.Contracts.Persistence
{
    public interface IWorkflowsService
    {
        Task<ViewAPIResponse<CreateWorkflowsDto>> CreateWorkflow(CreateWorkflowsDto request);
        Task<ViewAPIResponse<ResultWorkflowsDto>> FindWorkflowsById(string workflowId);
        Task<ViewAPIResponse<CreateWorkflowsDto>> EditWorkflow(CreateWorkflowsDto request);
        Task<ViewAPIResponse<string>> DeleteWorkflow(string workflowId);
        Task<ViewAPIResponse<List<ResultWorkflowsDto>>> FindAllWorkflows();
    }
}
