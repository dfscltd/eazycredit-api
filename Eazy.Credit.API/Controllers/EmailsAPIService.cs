using Eazy.Credit.Security.Contracts.Auth;
using Eazy.Credit.Security.Contracts.Persistence;
using Eazy.Credit.Security.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eazy.Credit.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/workflow")]
    [ApiController]
    public class EmailsAPIService : ControllerBase
    {
        private readonly IEmailsService emailsService;
        public EmailsAPIService(IEmailsService emailsService)
        {
            this.emailsService = emailsService;
        }

        [HttpPost("SendEmailAsync")]
        public async Task<IActionResult> SendEmailSendGrid([FromBody] SendEmailRequestDto request)
        {
            var response = await emailsService.SendEmailSendGrid(request.DestEmail, request.Subject, request.Message);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
