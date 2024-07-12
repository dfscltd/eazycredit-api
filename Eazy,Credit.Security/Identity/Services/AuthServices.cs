using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Entities;
using Eazy.Credit.Security.Identity.Data;
using Eazy.Credit.Security.Contracts.Auth;


using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Identity.UI.Services;

/**
 * token managent
 * https://dotnettutorials.net/lesson/how-to-store-tokens-in-asp-net-core-identity/
 * */

namespace Eazy.Credit.Security.Identity.Services
{
    public class AuthServices: IAuthServicecs
    {
        private readonly AuthSettings _authSettings;
        private readonly SecurityContext db;
        private readonly IConfiguration _config;

        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly RoleManager<AppRoles> _roleManager;
        private readonly IEmailsService _emailsService;
        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        public AuthServices(IOptions<AuthSettings> authSettings, 
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

        public async Task<ViewAPIResponse<LoginResponseDto>> Login(loginModelDTO request)
        {

            ViewAPIResponse<LoginResponseDto> response = null;
            LoginResponseDto loginResult = null;

            var user = await _userManager.FindByIdAsync(request.UserName);

            if (user == null)
            {
                loginResult = new LoginResponseDto();
                return response = new ViewAPIResponse<LoginResponseDto>() { 
                    ResponseCode = "01",
                    ResponseMessage = "UserNotFound",
                    ResponseResult = loginResult
                };
            }

            var isPasswordExpired = (user?.LastPasswordChangedDate.AddDays(Convert.ToDouble(_config.GetSection("IdentityDefaultOptions:PasswordAge").Value)) < DateTime.Now) ? true : false;

            //await LoadSharedKeyAndQrCodeUriAsync(user);

            //if (user?.LastPasswordChangedDate.AddDays(Convert.ToDouble(_config.GetSection("IdentityDefaultOptions:PasswordAge").Value)) < DateTime.Now)
            //{
                loginResult = new LoginResponseDto
                {
                    Authenticated = false,
                    //Id = user.Id,
                    Token = null,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Enabled = true,
                    Locked = user.LockoutEnabled,
                    PasswordExired = isPasswordExpired,//(user?.LastPasswordChangedDate.AddDays(Convert.ToDouble(_config.GetSection("IdentityDefaultOptions:PasswordAge").Value)) < DateTime.Now)? true:false,
                    Branches = null,
                    LastPasswordChangedDate = user.LastPasswordChangedDate,
                    PasswordExpiration = user.PasswordExpiration
                };

            //    return response = new ViewAPIResponse<LoginResponseDto>()
            //    {
            //        ResponseCode = "01",
            //        ResponseMessage = "Password has expired",
            //        ResponseResult = loginResult
            //    };
            //}

            //var isPwdValid = await _userManager.CheckPasswordAsync(user, request.Password);


            if (isPasswordExpired || !(await _userManager.CheckPasswordAsync(user, request.Password)))
            {
                //loginResult = new LoginResponseDto();
                return response = new ViewAPIResponse<LoginResponseDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "passwordinvalid",
                    ResponseResult = loginResult
                };
            }

            //var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);



            //if (!result.Succeeded)
            //{
            //    loginResult = new LoginResponseDto();

            //    return response = new ViewAPIResponse<LoginResponseDto>()
            //    {
            //        ResponseCode = "01",
            //        ResponseMessage = "InvalidCredentials",
            //        ResponseResult = loginResult
            //    };
            //}
            //if (result.IsLockedOut)
            //{
            //    //It's important to inform users when their account is locked.
            //    //This can be done through the UI or by sending an email notification.
            //    await _emailsService.SendEmailSendGrid(user.Email, "AccountLocked", $"Your account has been locked out");
            //}

            //if (result.RequiresTwoFactor)
            //{
            //    //Then Check if User Exists, EmailConfirmed and Password Is Valid
            //    //CheckPasswordAsync: Returns a flag indicating whether the given password is valid for the specified user.
            //    if (!user.EmailConfirmed)
            //    {
            //        return response = new ViewAPIResponse<LoginResponseDto>()
            //        {
            //            ResponseCode = "01",
            //            ResponseMessage = "2FA require email confirmation before login",
            //            ResponseResult = loginResult
            //        };
            //    }


            //    var TwoFactorAuthenticationToken = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            //    //Sending SMS
            //    //await smsSender.SendSmsAsync(user.PhoneNumber, $"Your 2FA Token is {TwoFactorAuthenticationToken}");

            //    //Sending Email
            //    //await emailSender.SendEmailAsync(user.Email, "2FA Token", $"Your 2FA Token is {TwoFactorAuthenticationToken}", false);
            //    await _emailsService.SendEmailSendGrid(user.Email, "2FA Token", $"Your 2FA Token is {TwoFactorAuthenticationToken}");

            //}

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            loginResult = new LoginResponseDto
            {
                Authenticated = true,
                //Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Enabled = true,
                Locked = user.LockoutEnabled,
                PasswordExired = false,
                Branches = null,
                LastPasswordChangedDate = user.LastPasswordChangedDate,
                PasswordExpiration = user.PasswordExpiration,
                EmailConfirmed = user.EmailConfirmed,
                PasswordReset = user.PasswordReset,
            };

            

            return response = new ViewAPIResponse<LoginResponseDto>()
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = loginResult
            };
        }

        //confirm/validate the token generated during login process
        public async Task<ViewAPIResponse<TwoFAAuthenticatorSignInResultDto>> TwoFAAuthenticator(TwoFAAuthenticatorRequestDto request)
        {
            ViewAPIResponse<TwoFAAuthenticatorSignInResultDto> response = null;



                using (var trans = await db.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var user = await _userManager.FindByIdAsync(request.UserId);

                        if (user == null)
                            return response = new ViewAPIResponse<TwoFAAuthenticatorSignInResultDto>()
                            {
                                ResponseCode = "01",
                                ResponseMessage = "UserNotFound"
                            };

                        var userCode = user.AuthCode;

                        if (userCode == request.TokenCode) //OtpCode is the value generated from ChangePassword request method and must match what is on the DB.
                        {
                            var result = new TwoFAAuthenticatorSignInResultDto
                            {
                                Validated = true,
                                Token = userCode
                            };

                            return response = new ViewAPIResponse<TwoFAAuthenticatorSignInResultDto>()
                            {
                                ResponseCode = "00",
                                ResponseMessage = "success",
                                ResponseResult = result
                            };
                        }
                        else
                        {
                            return response = new ViewAPIResponse<TwoFAAuthenticatorSignInResultDto>()
                            {
                                ResponseCode = "01",
                                ResponseMessage = "InvalidCode"
                            };

                        }

                    }
                    catch (Exception ex)
                    {
                        await trans.RollbackAsync();
                        return response = new ViewAPIResponse<TwoFAAuthenticatorSignInResultDto>()
                        {
                            ResponseCode = "01",
                            ResponseMessage = ex.Message
                        };
                    }
                }
        }
        
        //generate token for login verification
        public async Task<ViewAPIResponse<TwoFAAuthenticatorResponseDto>> Generate2FAToken(TwoFATokenDto request)
        {
            ViewAPIResponse<TwoFAAuthenticatorResponseDto> response = null;

            var trans = await db.Database.BeginTransactionAsync();

            try
            {
                var user = await _userManager.FindByIdAsync(request.UserName);

                if (user == null)
                    return response = new ViewAPIResponse<TwoFAAuthenticatorResponseDto>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "UserNotFound"
                    };

                var chars = "0123456789";
                var random = new Random();
                var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

                user.AuthCode = randomNumber;

                var updateResult = await _userManager.UpdateAsync(user);

                if (!updateResult.Succeeded)
                    return response = new ViewAPIResponse<TwoFAAuthenticatorResponseDto>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "ErrorInUpdateUser"
                    };

                var message = $"Code To authenticate: {user.AuthCode}";

                await _emailsService.SendEmailSendGrid(user.Email, message, "Authentication");

                await trans.CommitAsync();

                TwoFAAuthenticatorResponseDto twoFAAuthenticatorResponseDto = new TwoFAAuthenticatorResponseDto();
                twoFAAuthenticatorResponseDto.IsTokenSent = true;
                twoFAAuthenticatorResponseDto.Token = randomNumber;

                return response = new ViewAPIResponse<TwoFAAuthenticatorResponseDto>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = twoFAAuthenticatorResponseDto
                };//Success<string>(_localizer["Success"]);
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return response = new ViewAPIResponse<TwoFAAuthenticatorResponseDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = ex.Message
                };
            }
        }
       
        //new account creation verification and password reset
        public async Task<ViewAPIResponse<GenerateResetPasswordTokenResultDto>> GenerateResetPasswordTokenAsync(GenerateResetPasswordTokenDto request)
        {
            ViewAPIResponse<GenerateResetPasswordTokenResultDto> response = null;


            var user = await _userManager.FindByEmailAsync(request.email);

            if (user == null)
            {
                return response = new ViewAPIResponse<GenerateResetPasswordTokenResultDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "UserNotFound"
                };
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // save the token into the AspNetUserTokens database table
            await _userManager.SetAuthenticationTokenAsync(user, "ResetPassword", "ResetPasswordToken", token);

            ////var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //string code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));


            user.PasswordResetCode = token;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                return response = new ViewAPIResponse<GenerateResetPasswordTokenResultDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "ErrorInUpdateUser"
                };


            if (token != null)
            {

                var message = $"Account verification: {token}";

                await _emailsService.SendEmailSendGrid(user.Email, message, "Verify account");

                var result = new GenerateResetPasswordTokenResultDto()
                {
                    IsTokenSent = true,
                    Token = token,
                };

                return response = new ViewAPIResponse<GenerateResetPasswordTokenResultDto>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = result
                };
            }
            else
            {
                return response = new ViewAPIResponse<GenerateResetPasswordTokenResultDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "token could not be generated"
                };
            }
        }

        //generate and send a token to the provided email
        public async Task<ViewAPIResponse<string>> SendResetPasswordCode(ResetPasswordCodeDto request)
        {
            ViewAPIResponse<string> response = null;

            var trans = await db.Database.BeginTransactionAsync();

            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "UserNotFound"
                    };

                var chars = "0123456789";
                var random = new Random();
                var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

                user.PasswordChangeCode = randomNumber;

                var updateResult = await _userManager.UpdateAsync(user);

                if (!updateResult.Succeeded)
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "ErrorInUpdateUser"
                    };

                var message = $"Code To Change Password: {user.PasswordChangeCode}";

                await _emailsService.SendEmailSendGrid(user.Email, message, "Change Password Request Code");

                await trans.CommitAsync();

                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "Success"
                };
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "failed",
                    ResponseResult = ex.Message
                };
            }
        }

        public async Task<ViewAPIResponse<string>> ConfirmAndResetPassword(ConfirmAndResetPasswordDto request)
        {
            ViewAPIResponse<string> response = null;

            using (var trans = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(request.Email);

                    if (user == null)
                        return response = new ViewAPIResponse<string>()
                        {
                            ResponseCode = "01",
                            ResponseMessage = "UserNotFound"
                        };

                    var userCode = user.PasswordResetCode;

                    if (userCode == request.OtpCode)
                    {
                        // Code is valid, proceed to reset the password
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, request.NewPassword);

                        await trans.CommitAsync();

                        return response = new ViewAPIResponse<string>()
                        {
                            ResponseCode = "00",
                            ResponseMessage = "Success",
                            ResponseResult = "PasswordResetSuccess"
                        };
                        
                    }

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "failed",
                        ResponseResult = "InvalidCode"
                    };
                }
                catch (Exception ex)
                {
                    await trans.RollbackAsync();
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "failed",
                        ResponseResult = ex.Message
                    };
                }
            }
        }

        public async Task<ViewAPIResponse<ChangePasswordResultDto>> ChangePassword(ChangePasswordDto request)
        {
            ViewAPIResponse<ChangePasswordResultDto> response = null;


            var trans = await db.Database.BeginTransactionAsync();

            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                    return response = new ViewAPIResponse<ChangePasswordResultDto>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "UserNotFound"
                    };

                if (user.PasswordChangeCode == request.OtpCode) //OtpCode is the value generated from ChangePassword request method and must match what is on the DB.
                {
                    // Code is valid, proceed to reset the password
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, request.NewPassword);

                    user.LastPasswordChangedDate = DateTime.Now;
                    user.PasswordExpiration = DateTime.Now.AddDays(Convert.ToInt16(_config.GetSection("IdentityDefaultOptions:PasswordAge").Value));


                    await _userManager.UpdateAsync(user);

                    await trans.CommitAsync();

                    var result = new ChangePasswordResultDto() { IsPasswordChanged = true };

                    return response = new ViewAPIResponse<ChangePasswordResultDto>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "Success",
                        ResponseResult = result
                    };

                    //return Success<string>(_localizer["PasswordResetSuccess"]);
                }

                else
                {
                    return response = new ViewAPIResponse<ChangePasswordResultDto>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "InvalidCode"
                    };
                }

            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return response = new ViewAPIResponse<ChangePasswordResultDto>()
                {
                    ResponseCode = "01",
                    ResponseMessage = "failed"
                    
                };
            }
        }

        //reset user password by deactivating the old one and making the new provided pwd active
        public async Task<ViewAPIResponse<ResetPasswordTokenResultDto>> ResetPasswordTokenAsync(ResetPasswordTokenDto request)
        {
            ViewAPIResponse<ResetPasswordTokenResultDto> response = null;
            using (var trans = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(request.Email);

                    if (user == null)
                        return response = new ViewAPIResponse<ResetPasswordTokenResultDto>()
                        {
                            ResponseCode = "01",
                            ResponseMessage = "UserNotFound"
                        };

                    //var userCode = user.PasswordChangeCode; 

                    if (user.PasswordResetCode == request.TokenCode) //OtpCode is the value generated from ChangePassword request method and must match what is on the DB.
                    {


                        //var verifyToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", request.TokenCode);

                        //await _userManager.RemovePasswordAsync(user);
                        //var pwdReset = await _userManager.AddPasswordAsync(user, request.Password);

                        

                        var pwdReset = await _userManager.ResetPasswordAsync(user, request.TokenCode, request.Password);

                        if(pwdReset.Succeeded)
                        {
                            //Once the Password is Reset, remove the token from the database
                            await _userManager.RemoveAuthenticationTokenAsync(user, "ResetPassword", "ResetPasswordToken");

                            user.EmailConfirmed = true;
                            user.PasswordReset = false;
                            //user.UserName = request.Password;
                            user.LastPasswordChangedDate = DateTime.Now;
                            user.PasswordExpiration = DateTime.Now.AddDays(Convert.ToInt16(_config.GetSection("IdentityDefaultOptions:PasswordAge").Value));
                            await _userManager.UpdateAsync(user);

                        }

                        await trans.CommitAsync();

                        var result = new ResetPasswordTokenResultDto() { IsPasswordReset = true };

                        return response = new ViewAPIResponse<ResetPasswordTokenResultDto>()
                        {
                            ResponseCode = "00",
                            ResponseMessage = "Success",
                            ResponseResult = result
                        };

                        //return Success<string>(_localizer["PasswordResetSuccess"]);
                    }
                    else
                    {
                        return response = new ViewAPIResponse<ResetPasswordTokenResultDto>()
                        {
                            ResponseCode = "01",
                            ResponseMessage = "InvalidCode"
                        };
                    }
                    
                }
                catch (Exception ex)
                {
                    await trans.RollbackAsync();
                    return response = new ViewAPIResponse<ResetPasswordTokenResultDto>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = ex.Message,
                    };
                    
                }
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(AppUsers user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Secret));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _authSettings.Issuer,
                audience: _authSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_authSettings.TokenExpirationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition, 4)).Append(' ');
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private async ValueTask LoadSharedKeyAndQrCodeUriAsync(AppUsers user)
        {
            // Load the authenticator key & QR code URI to display on the form
            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            string sharedKey = FormatKey(unformattedKey!);

            //var email = await _userManager.GetEmailAsync(user);
            //get secret key from here
            string authenticatorUri = GenerateQrCodeUri(user.Email!, unformattedKey!);
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                AuthenticatorUriFormat,
                null,
                null,
                unformattedKey);
        }


    }
}
