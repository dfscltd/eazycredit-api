
using MimeKit;
using Eazy.Credit.Security.Contracts.Auth;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using System.Net;
using Eazy.Credit.Security.Dtos;

namespace Eazy.Credit.Security.Identity.Services
{
    public class EmailsService : IEmailsService
    {
        #region Fields
        private readonly EmailSettingsDto _emailSettings;
        private readonly SendGridOptionsDto _sendgrid;
        #endregion

        #region Constructors
        public EmailsService(IOptions<EmailSettingsDto> emailSettings, IOptions<SendGridOptionsDto> sendgrid) 
        {
            _emailSettings = emailSettings.Value;
            _sendgrid = sendgrid.Value;
        }
        #endregion

        #region Handle Functions
        public async Task<ViewAPIResponse<string>> SendEmail(string email, string message, string reason)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                // Sending a professional and customized email
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);

                    // Create a more customized email body
                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = $"<p>{"DearUser"},</p>" +
                                   $"<p>{message}</p>" +
                                   $"<p>{"BestRegards"} </p>",
                        TextBody = $"Dear User,{Environment.NewLine}{message}{Environment.NewLine}Best Regards,{Environment.NewLine}CompanyName"
                    };

                    var mimeMessage = new MimeMessage
                    {
                        Body = bodyBuilder.ToMessageBody()
                    };

                    mimeMessage.From.Add(new MailboxAddress("CompanyName", _emailSettings.FromEmail));
                    mimeMessage.To.Add(new MailboxAddress("RecipientName", email));
                    mimeMessage.Subject = reason == null ? "NoSubmitted" : reason;

                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }

                // End of sending email
                //return Success<string>(_localizer[]);
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = "Success"
                };//Success<string>(_localizer["Success"]);
            }
            catch (Exception ex)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = ex.Message
                };
            }
        }

        public async Task<ViewAPIResponse<string>> SendEmailSendGrid(string email, string message, string reason)
        {
            ViewAPIResponse<string> response = null;

            try
            {
               var result = await Execute(_sendgrid.SendGridKey, reason, message, email);

                if (result.StatusCode == HttpStatusCode.Accepted)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success",
                        ResponseResult = "Success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "faile",
                        ResponseResult = result.ToString()
                    };
                }

                
            }
            catch (Exception ex)
            {
                return response = new ViewAPIResponse<string>()
                {
                    ResponseCode = "01",
                    ResponseMessage = ex.Message
                };
            }
        }

        public async Task<SendGrid.Response> Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_sendgrid.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            var response = await client.SendEmailAsync(msg);
            return response;
        }

        #endregion

    }
}
