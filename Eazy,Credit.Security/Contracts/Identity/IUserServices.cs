using Eazy.Credit.Security.Dtos;


namespace Eazy.Credit.Security.Contracts.Identity
{
    public interface IUserServices
    {
        Task<ViewAPIResponse<CreateUserDto>> CreateUser(CreateUserDto request);
        Task<ViewAPIResponse<UpdateUserDto>> UpdateUser(UpdateUserDto request);
        Task<ViewAPIResponse<string>> DeleteUser(string userId);
        Task<ViewAPIResponse<SelectUserDto>> GetUser(string email);
        Task<ViewAPIResponse<List<SelectUserDto>>> GetAllUser();
        Task<ViewAPIResponse<SelectUserDto>> GetUserByIdAsync(string loginId);
        Task<ViewAPIResponse<List<SelectUserDto>>> GetAllUsersByRoleId(string roleId);
    }
}
