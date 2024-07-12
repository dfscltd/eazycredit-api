using Eazy.Credit.Security.Contracts.Persistence;
using Eazy.Credit.Security.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eazy.Credit.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/workflow")]
    [ApiController]
    public class RoleLimitAPIService : ControllerBase
    {
        private readonly IPrmRoleLimitService ruleNumberService;

        public RoleLimitAPIService(IPrmRoleLimitService ruleNumberService)
        {
            this.ruleNumberService = ruleNumberService;
        }

        [HttpPost("AddPrmRoleLimitAsync")]
        public async Task<IActionResult> CreatePmrRuleNumber([FromBody] CreatePrmRoleLimitDto request)
        {
            var response = await ruleNumberService.CreatePmrRuleNumber(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPut("EditPrmRoleLimitAsync")]
        public async Task<IActionResult> EditPmrRuleNumber([FromBody] CreatePrmRoleLimitDto request)
        {
            var response = await ruleNumberService.EditPmrRuleNumber(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("DeletePrmRoleLimitAsync/{limitId}")]
        public async Task<IActionResult> DeletePmrRuleNumber(string limitId)
        {
            var response = await ruleNumberService.DeletePmrRuleNumber(limitId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("AssignRoleToPrmRoleLimitAsync")]
        public async Task<IActionResult> AssignRolesToRuleNumber([FromBody] AssignRoleToRuleNumberDto request)
        {
            var response = await ruleNumberService.AssignRolesToRuleNumber(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("RemoveRoleFromPrmRoleLimitRuleAsync")]
        public async Task<IActionResult> RemoveRolesFromRuleNumber([FromBody] AssignRoleToRuleNumberDto request)
        {
            var response = await ruleNumberService.RemoveRolesFromRuleNumber(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetPrmRoleLimitByLimitIdAsync/{limitId}")]
        public async Task<IActionResult> FindPmrRuleNumberByParamId(string limitId)
        {
            var response = await ruleNumberService.FindPmrRuleNumberByParamId(limitId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetAllPrmRoleLimitsAsync")]
        public async Task<IActionResult> FindAllPmrRuleNumbers()
        {
            var response = await ruleNumberService.FindAllPmrRuleNumbers();

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetRoleLimitByRoleId/{roleId}")]
        public async Task<IActionResult> FindNumberRulesByRoleId(string roleId)
        {
            var response = await ruleNumberService.FindNumberRulesByRoleId(roleId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

    }
}
