

using Eazy.Credit.Security.Dtos;

namespace Eazy.Credit.Security.Contracts.Identity
{
    public interface IRoleServices
    {
        Task<ViewAPIResponse<CreateRoleDto>> CreateRole(CreateRoleDto request);
        Task<ViewAPIResponse<CreateRoleDto>> EditRole(CreateRoleDto request);
        Task<ViewAPIResponse<string>> DeleteRole(string name);
        Task<ViewAPIResponse<string>> AddRoleToUser(AssignRoleToUserDto request);
        Task<ViewAPIResponse<List<CreateRoleDto>>> GetAllRoles();
        Task<ViewAPIResponse<List<RolesMembershipDto>>> GetUserRoles(string userId);
        Task<ViewAPIResponse<string>> RemoveRoleFromUser(AssignRoleToUserDto request);
        Task<ViewAPIResponse<CreateRoleDto>> GetRoleById(string roleId);
    }
}
