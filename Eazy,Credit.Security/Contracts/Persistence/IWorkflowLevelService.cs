using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Entities;


namespace Eazy.Credit.Security.Contracts.Persistence
{
    public interface IWorkflowLevelService
    {
        Task<ViewAPIResponse<CreateWorkflowLevelDto>> CreateWorkflowLevel(CreateWorkflowLevelDto request);
        Task<ViewAPIResponse<ResulyWorkflowLevelDto>> FindWorkflowsLevelById(string workflowId, string levelId);
        Task<ViewAPIResponse<CreateWorkflowLevelDto>> EditWorkflowLevel(CreateWorkflowLevelDto request);
        Task<ViewAPIResponse<string>> RemoveWorkflowLevel(string workflowId, string levelId);
        Task<ViewAPIResponse<List<ResulyWorkflowLevelDto>>> FindWorkflowsLevelByWorkflow(string workflowId);
        Task<ViewAPIResponse<string>> AssignRolesToWorkflowLevel(AssignRoleToWorkflowLevelDto request);
        Task<ViewAPIResponse<string>> RemoveRolesFromWorkflowLevel(AssignRoleToWorkflowLevelDto request);
        Task<ViewAPIResponse<List<ResultRoleToWorkflowLevelDto>>> FindAssignedRoleToWorkflowsLevel(string roleId);
        Task<ViewAPIResponse<List<ApprovingUsersDto>>> FindApprovingUsersInWorkflowsLevelById(ApprovingUsersRequestDto request);
        Task<ViewAPIResponse<List<RolesInWorkflowsLevelResultDto>>> FindRolesInWorkflowsLevelById(string workflowId, string levelId);
    }
}
