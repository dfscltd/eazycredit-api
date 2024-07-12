using Eazy.Credit.Security.Dtos;


namespace Eazy.Credit.Security.Contracts.Persistence
{
    public interface IPrmRoleLimitService
    {
        Task<ViewAPIResponse<CreatePrmRoleLimitDto>> CreatePmrRuleNumber(CreatePrmRoleLimitDto request);
        Task<ViewAPIResponse<CreatePrmRoleLimitDto>> FindPmrRuleNumberByParamId(string request);
        Task<ViewAPIResponse<CreatePrmRoleLimitDto>> EditPmrRuleNumber(CreatePrmRoleLimitDto request);
        Task<ViewAPIResponse<string>> DeletePmrRuleNumber(string limitId);
        Task<ViewAPIResponse<string>> AssignRolesToRuleNumber(AssignRoleToRuleNumberDto request);
        Task<ViewAPIResponse<string>> RemoveRolesFromRuleNumber(AssignRoleToRuleNumberDto request);
        Task<ViewAPIResponse<List<CreatePrmRoleLimitDto>>> FindAllPmrRuleNumbers();
        Task<ViewAPIResponse<List<RolesPrmLimitDto>>> FindNumberRulesByRoleId(string UserRoleID);
    }
}
