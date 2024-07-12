using Eazy.Credit.Security.Contracts.Auth;
using Eazy.Credit.Security.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eazy.Credit.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/workflow")]
    [ApiController]
    public class AuthAPIService : ControllerBase
    {
        private readonly IAuthServicecs authServicecs;

        public AuthAPIService(IAuthServicecs authServicecs)
        {
            this.authServicecs = authServicecs;
        }


        [HttpPost("LoginAsync")]
        public async Task<IActionResult> Login([FromBody] loginModelDTO request)
        {
            var response = await authServicecs.Login(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("GenerateTwoFactorTokenAsync")]
        public async Task<IActionResult> Generate2FAToken([FromBody] TwoFATokenDto request)
        {
            var response = await authServicecs.Generate2FAToken(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }


        [HttpPost("TwoFactorAuthenticatorSignInAsync")]
        public async Task<IActionResult> TwoFAAuthenticator([FromBody] TwoFAAuthenticatorRequestDto request)
        {
            var response = await authServicecs.TwoFAAuthenticator(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("GenerateResetPasswordTokenAsync")]
        public async Task<IActionResult> GenerateResetPasswordTokenAsync([FromBody] GenerateResetPasswordTokenDto request)
        {
            var response = await authServicecs.GenerateResetPasswordTokenAsync(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("ResetPasswordTokenAsync")]
        public async Task<IActionResult> ResetPasswordTokenAsync([FromBody] ResetPasswordTokenDto request)
        {
            var response = await authServicecs.ResetPasswordTokenAsync(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("ChangePasswordAsync")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
        {
            var response = await authServicecs.ChangePassword(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("SendResetPasswordCode")]
        public async Task<IActionResult> SendResetPasswordCode([FromBody] ResetPasswordCodeDto request)
        {
            var response = await authServicecs.SendResetPasswordCode(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("ConfirmAndResetPassword")]
        public async Task<IActionResult> ConfirmAndResetPassword([FromBody] ConfirmAndResetPasswordDto request)
        {
            var response = await authServicecs.ConfirmAndResetPassword(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

    }
}
