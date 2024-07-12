using Eazy.Credit.Security.Contracts.Persistence;
using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Eazy.Credit.API.Controllers
{
    [Route("api/workflow")]
    //[Route("api/[controller]")]
    [ApiController]
    public class WorkflowLevelAPIService : ControllerBase
    {
        private readonly IWorkflowLevelService workflowLevelService;

        public WorkflowLevelAPIService(IWorkflowLevelService workflowLevelService)
        {
            this.workflowLevelService = workflowLevelService;
        }

        [HttpPost("AddWorkflowLevelAsync")]
        public async Task<IActionResult> CreateWorkflowLevel([FromBody] CreateWorkflowLevelDto request)
        {
            var response = await workflowLevelService.CreateWorkflowLevel(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPut("UpdateWorkflowLevelAsync")]
        public async Task<IActionResult> EditWorkflowLevel([FromBody] CreateWorkflowLevelDto request)
        {
            var response = await workflowLevelService.EditWorkflowLevel(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("RemoveWorkflowLevelAsync/{workflowId}/{levelId}")]
        public async Task<IActionResult> RemoveWorkflowLevel(string workflowId, string levelId)
        {
            var response = await workflowLevelService.RemoveWorkflowLevel(workflowId, levelId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetWorkflowsLevelBy/{workflowId}")]
        public async Task<IActionResult> FindWorkflowsLevelByWorkflow(string workflowId)
        {
            var response = await workflowLevelService.FindWorkflowsLevelByWorkflow(workflowId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetWorkflowsLevel/{workflowId}/{levelId}")]
        public async Task<IActionResult> FindWorkflowsLevelById(string workflowId, string levelId)
        {
            var response = await workflowLevelService.FindWorkflowsLevelById(workflowId, levelId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetAssignedWorkflowsLevelsToRole/{roleId}")]
        public async Task<IActionResult> FindAssignedRoleToWorkflowsLevel(string roleId)
        {
            var response = await workflowLevelService.FindAssignedRoleToWorkflowsLevel(roleId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("AssignRolesToWorkflowLevelAsync")]
        public async Task<IActionResult> AssignRolesToWorkflowLevel([FromBody] AssignRoleToWorkflowLevelDto request)
        {
            var response = await workflowLevelService.AssignRolesToWorkflowLevel(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("RemoveRolesFromWorkflowLevelAsync")]
        public async Task<IActionResult> RemoveRolesFromWorkflowLevel([FromBody] AssignRoleToWorkflowLevelDto request)
        {
            var response = await workflowLevelService.RemoveRolesFromWorkflowLevel(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("GetAssignedRoleToWorkflowsLevel")]
        public async Task<IActionResult> FindApprovingUsersInWorkflowsLevelById(ApprovingUsersRequestDto request)
        {
            var response = await workflowLevelService.FindApprovingUsersInWorkflowsLevelById(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetAssignedRolesToWorkflowsLevel/{workflowId}/{levelId}")]
        public async Task<IActionResult> FindRolesInWorkflowsLevelById(string workflowId, string levelId)
        {
            var response = await workflowLevelService.FindRolesInWorkflowsLevelById(workflowId, levelId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

    }
}
