using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Entities;
using Eazy.Credit.Security.Identity.Data;
using Eazy.Credit.Security.Contracts.Auth;
using Eazy.Credit.Security.Contracts.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;


namespace Eazy.Credit.Security.Identity.Services
{
    /*
     * 
     * https://jasonwatmore.com/post/2022/02/26/net-6-boilerplate-api-tutorial-with-email-sign-up-verification-authentication-forgot-password
     * */
    public class UserServices : IUserServices
    {
        private readonly AuthSettings _authSettings;
        private readonly SecurityContext db;
        

        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly RoleManager<AppRoles> _roleManager;
        private readonly IEmailsService _emailsService;
        private readonly IConfiguration _config;
        public UserServices(IOptions<AuthSettings> authSettings,
            SecurityContext _db,
            UserManager<AppUsers> userManager,
            SignInManager<AppUsers> signInManager,
            RoleManager<AppRoles> roleManager,
            IEmailsService emailsService,
            IConfiguration config)
        {
            _authSettings = authSettings.Value;
            db = _db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailsService = emailsService; 
            _config = config;
        }

        public async Task<ViewAPIResponse<CreateUserDto>> CreateUser(CreateUserDto request)
        {
            ViewAPIResponse<CreateUserDto> response = null;

            var existingUser = await _userManager.FindByNameAsync(request.UserId);

            if (existingUser != null)
            {
                return response = new ViewAPIResponse<CreateUserDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "UsernameExists",
                    ResponseResult = request
                };

            }

            //byte[] passwordHash, passwordSalt;
            //CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            

            var user = new AppUsers
            {
                Id = request.UserId,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                OtherName = request.OtherName,
                UserName = request.UserId,
                ShortName = request.ShortName,
                EmployeeID = request.EmployeeId,
                EmailConfirmed = false,
                LastPasswordChangedDate = DateTime.Now,
                PasswordExpiration = new DateTime().AddDays(Convert.ToDouble(_config.GetSection("IdentityDefaultOptions:PasswordAge").Value)),
                PasswordReset = true,
                UserPhoto = request.UserPhoto,
                IsAdmin = request.IsAdmin,
                AddedBy = request.AddedBy
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                //if (result.Succeeded)
                //{
                //    await _userManager.AddToRoleAsync(user, "Employee");
                //    return Created(user.Id);
                //}
                //else
                //{
                //    return BadRequest<string>(_localizer["BadRequestDetails"], result.Errors.Select(a => a.Description).ToList());
                //}

                if(result.Succeeded)
                {
                    var pwdUser = await _userManager.FindByEmailAsync(request.Email);
                    await _userManager.AddPasswordAsync(pwdUser, request.Password);


                    if (Convert.ToBoolean(_config.GetSection("IdentityDefaultOptions:Enable2FALogin").Value))
                    {
                        await _userManager.SetTwoFactorEnabledAsync(pwdUser, true);
                    }


                    return response = new ViewAPIResponse<CreateUserDto>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success",
                        ResponseResult = request
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<CreateUserDto>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = result.ToString(),
                        ResponseResult = request
                    };
                }
                

            }
            else
            {
                return response = new ViewAPIResponse<CreateUserDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "EmailExists",
                    ResponseResult = request
                };
            }
        }

        public async Task<ViewAPIResponse<UpdateUserDto>> UpdateUser(UpdateUserDto request)
        {
            ViewAPIResponse<UpdateUserDto> response = null;

            var existingUser = await _userManager.FindByNameAsync(request.UserId);

            if (existingUser == null)
            {
                return response = new ViewAPIResponse<UpdateUserDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "UsernameExists",
                    ResponseResult = request
                };

            }


            existingUser.FirstName = request.FirstName;
            existingUser.LastName = request.LastName;
            existingUser.OtherName = request.OtherName;
            existingUser.Email = request.Email;
            existingUser.UserPhoto = request.UserPhoto;
            existingUser.Disabled = request.Disabled;
            existingUser.DisableReason = request.DisableReason;
            existingUser.EnableDate = request.EnableDate;
            //existingUser.Locked = request.Locked;


            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                return response = new ViewAPIResponse<UpdateUserDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "invalidemail",
                    ResponseResult = request
                };

            }
            else
            {
                var result = await _userManager.UpdateAsync(existingUser);

                return response = new ViewAPIResponse<UpdateUserDto>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = request
                };

            }
        }

        public async Task<ViewAPIResponse<string>> DeleteUser(string userid)
        {
            ViewAPIResponse<string> response = null;

            var existingUser = await _userManager.FindByIdAsync(userid);

            if (existingUser == null)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "UsernotExists"
                };

            }

            else
            {
                var result = await _userManager.DeleteAsync(existingUser);

                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success"
                };

            }
        }

        public async Task<ViewAPIResponse<SelectUserDto>> GetUser(string email)
        {
            ViewAPIResponse<SelectUserDto> response = null;

            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser == null)
            {
                return response = new ViewAPIResponse<SelectUserDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "UserNotFound"
                };

            }

            var result = new SelectUserDto
            {
                UserId = existingUser.Id,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                OtherName = existingUser.OtherName,
                ShortName = existingUser.ShortName,
                EnableDate = existingUser.EnableDate,
                AddedBy = existingUser.AddedBy,
                Disabled = existingUser.Disabled,
                Email = string.IsNullOrEmpty(existingUser.Email) ? "" : existingUser.Email,
                DisableReason = existingUser.DisableReason,
                IsAdmin = existingUser.IsAdmin,
                SuccessfulLoginsToday = existingUser.SuccessfulLoginsToday,
                PreviousPasswordChange = existingUser.LastPasswordChangedDate,
                PasswordExpiration = existingUser.PasswordExpiration,
                LogInRetries = existingUser.LogInRetries,
                UserPhoto = existingUser.UserPhoto,
                TwoFactorEnabled = existingUser.TwoFactorEnabled,
                //DateAdded = existingUser.LastLoginDate
                PreviousLogin = existingUser.LastLoginDate,
                EmployeeId = existingUser.EmployeeID

            };


            return response = new ViewAPIResponse<SelectUserDto>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = result
            };

        }

        public async Task<ViewAPIResponse<List<SelectUserDto>>> GetAllUser()
        {
            ViewAPIResponse<List<SelectUserDto>> response = null;

            List<SelectUserDto> userList = new List<SelectUserDto>();

            var existingUser = _userManager.Users;

            if (existingUser == null)
            {
                return response = new ViewAPIResponse<List<SelectUserDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "UserNotFound"
                };

            }

            foreach(var record in existingUser)
            {
                userList.Add(new SelectUserDto
                {
                    UserId = record.Id,
                    FirstName = record.FirstName,
                    LastName = record.LastName,
                    OtherName = record.OtherName,
                    ShortName = record.ShortName,
                    EnableDate = record.EnableDate,
                    AddedBy = record.AddedBy,
                    Disabled = record.Disabled,
                    Email = string.IsNullOrEmpty(record.Email) ? "" : record.Email,
                    DisableReason = record.DisableReason,
                    IsAdmin = record.IsAdmin,
                    SuccessfulLoginsToday = record.SuccessfulLoginsToday,
                    PreviousPasswordChange = record.LastPasswordChangedDate,
                    PasswordExpiration = record.PasswordExpiration,
                    LogInRetries = record.LogInRetries,
                    UserPhoto = record.UserPhoto,
                    TwoFactorEnabled = record.TwoFactorEnabled,
                    PreviousLogin = record.LastLoginDate,
                    EmployeeId = record.EmployeeID
                    //DateAdded = record.


                });
            }


            return response = new ViewAPIResponse<List<SelectUserDto>>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = userList
            };

        }

        public async Task<ViewAPIResponse<List<SelectUserDto>>> GetAllUsersByRoleId(string roleId)
        {
            ViewAPIResponse<List<SelectUserDto>> response = null;



            List<SelectUserDto> userList = new List<SelectUserDto>();

            var role = await _roleManager.FindByIdAsync(roleId); 

            if (role == null)
            {
                return response = new ViewAPIResponse<List<SelectUserDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "NoRecord"
                };
            }

            var existingUser = await _userManager.GetUsersInRoleAsync(role.Name);

            if (existingUser.Count == 0)
            {
                return response = new ViewAPIResponse<List<SelectUserDto>>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "NoRecord"
                };

            }

            foreach (var record in existingUser)
            {
                userList.Add(new SelectUserDto
                {
                    UserId = record.Id,
                    FirstName = record.FirstName,
                    LastName = record.LastName,
                    OtherName = record.OtherName,
                    ShortName = record.ShortName,
                    EnableDate = record.EnableDate,
                    AddedBy = record.AddedBy,
                    Disabled = record.Disabled,
                    Email = string.IsNullOrEmpty(record.Email) ? "" : record.Email,
                    DisableReason = record.DisableReason,
                    IsAdmin = record.IsAdmin,
                    SuccessfulLoginsToday = record.SuccessfulLoginsToday,
                    PreviousPasswordChange = record.LastPasswordChangedDate,
                    PasswordExpiration = record.PasswordExpiration,
                    LogInRetries = record.LogInRetries,
                    UserPhoto = record.UserPhoto,
                    TwoFactorEnabled = record.TwoFactorEnabled,
                    PreviousLogin = record.LastLoginDate,
                    EmployeeId = record.EmployeeID
                    //DateAdded = record.


                });
            }


            return response = new ViewAPIResponse<List<SelectUserDto>>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = userList
            };

        }

        public async Task<ViewAPIResponse<SelectUserDto>> GetUserByIdAsync(string loginId)
        {
            ViewAPIResponse<SelectUserDto> response = null;

            var existingUser = await _userManager.FindByIdAsync(loginId);

            if (existingUser == null)
            {
                return response = new ViewAPIResponse<SelectUserDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "UserNotFound"
                };

            }

            var result = new SelectUserDto
            {
                UserId = existingUser.Id,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                OtherName = existingUser.OtherName,
                ShortName = existingUser.ShortName,
                EnableDate = existingUser.EnableDate,
                AddedBy = existingUser.AddedBy,
                Disabled = existingUser.Disabled,
                Email = string.IsNullOrEmpty(existingUser.Email) ? "" : existingUser.Email,
                DisableReason = existingUser.DisableReason,
                IsAdmin = existingUser.IsAdmin,
                SuccessfulLoginsToday = existingUser.SuccessfulLoginsToday,
                PreviousPasswordChange = existingUser.LastPasswordChangedDate,
                PasswordExpiration = existingUser.PasswordExpiration,
                LogInRetries = existingUser.LogInRetries,
                UserPhoto = existingUser.UserPhoto,
                TwoFactorEnabled = existingUser.TwoFactorEnabled,
                //DateAdded = existingUser.LastLoginDate
                PreviousLogin = existingUser.LastLoginDate,
                EmployeeId = existingUser.EmployeeID

            };


            return response = new ViewAPIResponse<SelectUserDto>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = result
            };

        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }



    }
}
