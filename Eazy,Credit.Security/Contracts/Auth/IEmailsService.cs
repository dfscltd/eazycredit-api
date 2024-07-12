using Eazy.Credit.Security.Dtos;


namespace Eazy.Credit.Security.Contracts.Auth
{
    public interface IEmailsService
    {
        public Task<ViewAPIResponse<string>> SendEmail(string email, string Message, string? reason);
        Task<ViewAPIResponse<string>> SendEmailSendGrid(string email, string message, string reason);
    }
}
