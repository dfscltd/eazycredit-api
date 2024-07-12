using Eazy.Credit.Security.Contracts.Persistence;
using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eazy.Credit.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/workflow")]
    [ApiController]
    public class TransHeaderStageAPIService : ControllerBase
    {
        private readonly ICreateTransHeaderStageService headerStageService;

        public TransHeaderStageAPIService(ICreateTransHeaderStageService headerStageService)
        {
            this.headerStageService = headerStageService;
        }

        [HttpPost("AddTransHeaderStageAsync")]
        public async Task<IActionResult> CreateTransHeaderStage([FromBody] CreateTransHeaderStageDto request)
        {
            var response = await headerStageService.CreateTransHeaderStage(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPut("EditTransHeaderStageAsync")]
        public async Task<IActionResult> EditTransHeaderStage([FromBody] CreateTransHeaderStageDto request)
        {
            var response = await headerStageService.EditTransHeaderStage(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("DeleteTransHeaderStageAsync/{transId}/{workflow}/{workflowLevel}")]
        public async Task<IActionResult> DeleteTransHeaderStage(string transId, string workflow, string workflowLevel)
        {
            var response = await headerStageService.DeleteTransHeaderStage(transId, workflow, workflowLevel);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetTransHeaderStageByIdAsync/{transId}/{workflow}/{workflowLevel}")]
        public async Task<IActionResult> FindTransHeaderStageById(string transId, string workflow, string workflowLevel)
        {
            var response = await headerStageService.FindTransHeaderStageById(transId, workflow, workflowLevel);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetTransHeaderStageByUserIdAsync/{userId}")]
        public async Task<IActionResult> FindTransHeaderStageByUserId(string userId)
        {
            var response = await headerStageService.FindTransHeaderStageByUserId(userId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

    }
}
