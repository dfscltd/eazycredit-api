using Eazy.Credit.Security.Entities;
using Eazy.Credit.Security.Contracts.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Eazy.Credit.Security.Dtos;
using Newtonsoft.Json;


namespace Eazy.Credit.Security.Identity.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly RoleManager<AppRoles> _roleManager;
        private readonly UserManager<AppUsers> _userManager;
        private readonly IUserServices _user;

        public RoleServices(RoleManager<AppRoles> roleManager, UserManager<AppUsers> userManager, IUserServices user)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _user = user;
        }

        public async Task<ViewAPIResponse<CreateRoleDto>> CreateRole(CreateRoleDto request)
        {
            ViewAPIResponse<CreateRoleDto> response = null;

            var roleExist = await _roleManager.RoleExistsAsync(request.Name);
            if (roleExist)
            {
                return response = new ViewAPIResponse<CreateRoleDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RoleExists",
                    ResponseResult = request
                };

            }

            var role = new AppRoles
            {
                Id = request.UserRoleID,
                Name = request.Name,
                Description = request.Description
            };
            var result = await _roleManager.CreateAsync(role);

            return response = new ViewAPIResponse<CreateRoleDto>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = request
            };
        }

        public async Task<ViewAPIResponse<CreateRoleDto>> EditRole(CreateRoleDto request)
        {
            ViewAPIResponse<CreateRoleDto> response = null;


            var role = await _roleManager.FindByIdAsync(request.UserRoleID);

            if (role == null)
            {
                return response = new ViewAPIResponse<CreateRoleDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RoleNotFound",
                    ResponseResult = request
                };

            }

            role.Name = request.Name;
            role.Description = request.Description;
            var result = await _roleManager.UpdateAsync(role);

            return response = new ViewAPIResponse<CreateRoleDto>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = request
            };
        }

        public async Task<ViewAPIResponse<string>> DeleteRole(string name)
        {
            ViewAPIResponse<string> response = null;

            var role = await _roleManager.FindByNameAsync(name);
            if (role == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RoleNotFound"
                };
            }

            var result = await _roleManager.DeleteAsync(role);

            if(result.Succeeded)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success"
                };
            }
            else
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "Errordeleting"
                };
            }
            
        }

        public async Task<ViewAPIResponse<string>> AddRoleToUser(AssignRoleToUserDto request)
        {
            ViewAPIResponse<string> response = null;

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "UserNotFound"
                };

            }

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);

            return response = new ViewAPIResponse<string>()
            {
                ResponseCode = "00",
                ResponseMessage = "success"
            };
        }

        public async Task<ViewAPIResponse<List<CreateRoleDto>>> GetAllRoles()
        {
            ViewAPIResponse<List<CreateRoleDto>> response = null;
            List<CreateRoleDto> roleList = new List<CreateRoleDto>();

            var roles = await _roleManager.Roles.ToListAsync();

            if(roles.Count == 0)
            {
                return response = new ViewAPIResponse<List<CreateRoleDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RoleNotFound"
                };
            }
            foreach (var role in roles) {
                roleList.Add(new CreateRoleDto
                {
                    UserRoleID = role.Id,
                    Name = role.Name,
                    Description = role.Description
                });
            }

            return response = new ViewAPIResponse<List<CreateRoleDto>>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = roleList
            };

        }

        public async Task<ViewAPIResponse<List<RolesMembershipDto>>> GetUserRoles(string userId)
        {
            ViewAPIResponse<List<RolesMembershipDto>> response = null;
            List<RolesMembershipDto> roleList = new List<RolesMembershipDto>();

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return response = new ViewAPIResponse<List<RolesMembershipDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "UserNotFound"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var record in roles)
            {
                var data = await _roleManager.FindByNameAsync(record);
                roleList.Add(new RolesMembershipDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    RoleId = data.Id,
                    RoleName = data.Name,
                    RoleDescription = data.Description
                });
            }


            return response = new ViewAPIResponse<List<RolesMembershipDto>>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = roleList
            };
        }

        public async Task<ViewAPIResponse<CreateRoleDto>> GetRoleById(string roleId)
        {
            ViewAPIResponse<CreateRoleDto> response = null;

            var roleExist = await _roleManager.FindByIdAsync(roleId);
            if (roleExist == null)
            {
                return response = new ViewAPIResponse<CreateRoleDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "RoleNotExist"
                };

            }

            var result = new CreateRoleDto
            {
                UserRoleID = roleExist.Id,
                Name = roleExist.Name,
                Description = roleExist.Description
            };


            return response = new ViewAPIResponse<CreateRoleDto>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = result
            };

        }

        public async Task<ViewAPIResponse<string>> RemoveRoleFromUser(AssignRoleToUserDto request)
        {
            ViewAPIResponse<string> response = null;

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "UserNotFound"
                };

            }

            var result = await _userManager.RemoveFromRoleAsync(user, request.RoleName);

            if(result.Succeeded)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success"
                };
            }
            else
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = JsonConvert.SerializeObject(result.Errors)
                };
            }
        }

    }
}
