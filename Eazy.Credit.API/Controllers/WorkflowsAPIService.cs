using Eazy.Credit.Security.Contracts.Persistence;
using Eazy.Credit.Security.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Eazy.Credit.API.Controllers
{
    [Route("api/workflow")]
    //[Route("api/[controller]")]
    [ApiController]
    public class WorkflowsAPIService : ControllerBase
    {
        private readonly IWorkflowsService workflowsService;

        public WorkflowsAPIService(IWorkflowsService workflowsService)
        {
            this.workflowsService = workflowsService;
        }


        [HttpPost("AddWorkflowAsync")]
        public async Task<IActionResult> CreateWorkflow([FromBody] CreateWorkflowsDto request)
        {
            var response = await workflowsService.CreateWorkflow(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPut("EditWorkflowAsync")]
        public async Task<IActionResult> EditWorkflow([FromBody] CreateWorkflowsDto request)
        {
            var response = await workflowsService.EditWorkflow(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("RemoveWorkflowAsync/{workflowId}")]
        public async Task<IActionResult> DeleteWorkflow(string workflowId)
        {
            var response = await workflowsService.DeleteWorkflow(workflowId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("WorkflowAsync/{workFlowId}")]
        public async Task<IActionResult> FindWorkflowsById(string workFlowId)
        {
            var response = await workflowsService.FindWorkflowsById(workFlowId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("WorkflowAsync")]
        public async Task<IActionResult> FindAllWorkflowsD()
        {
            var response = await workflowsService.FindAllWorkflows();

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
