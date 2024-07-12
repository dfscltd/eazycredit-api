using Eazy.Credit.Security.Dtos;


namespace Eazy.Credit.Security.Contracts.Auth
{
    public interface IAuthServicecs
    {
        Task<ViewAPIResponse<LoginResponseDto>> Login(loginModelDTO request);
        Task<ViewAPIResponse<TwoFAAuthenticatorResponseDto>> Generate2FAToken(TwoFATokenDto request);
        Task<ViewAPIResponse<TwoFAAuthenticatorSignInResultDto>> TwoFAAuthenticator(TwoFAAuthenticatorRequestDto request);
        Task<ViewAPIResponse<GenerateResetPasswordTokenResultDto>> GenerateResetPasswordTokenAsync(GenerateResetPasswordTokenDto request);
        Task<ViewAPIResponse<ResetPasswordTokenResultDto>> ResetPasswordTokenAsync(ResetPasswordTokenDto request);
        Task<ViewAPIResponse<ChangePasswordResultDto>> ChangePassword(ChangePasswordDto request);
        Task<ViewAPIResponse<string>> SendResetPasswordCode(ResetPasswordCodeDto request);
        Task<ViewAPIResponse<string>> ConfirmAndResetPassword(ConfirmAndResetPasswordDto request);
    }
}
