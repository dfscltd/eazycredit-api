using Eazy.Credit.Security.Contracts.Identity;
using Eazy.Credit.Security.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Eazy.Credit.API.Controllers
{
    [Route("api/workflow")]
    //[Route("api/[controller]")]
    [ApiController]
    public class RoleAPIServices : ControllerBase
    {
        private readonly IRoleServices roleServices;

        public RoleAPIServices(IRoleServices roleServices)
        {
            this.roleServices = roleServices;
        }

        [HttpPost("AddRoleAsync")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto request)
        {
            var response = await roleServices.CreateRole(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPut("UpdateRoleAsync")]
        public async Task<IActionResult> EditRole([FromBody] CreateRoleDto request)
        {
            var response = await roleServices.EditRole(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("DeleteRoleAsync/{name}")]
        public async Task<IActionResult> DeleteRole(string name)
        {
            var response = await roleServices.DeleteRole(name);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("AddRoleToUserAsync")]
        public async Task<IActionResult> AddRoleToUser([FromBody] AssignRoleToUserDto request)
        {
            var response = await roleServices.AddRoleToUser(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetAllRolesAsync")]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await roleServices.GetAllRoles();

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetRolesAsync/{loginId}")]
        public async Task<IActionResult> GetUserRoles(string loginId)
        {
            var response = await roleServices.GetUserRoles(loginId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("RemoveRoleFromUserAsync")]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] AssignRoleToUserDto request)
        {
            var response = await roleServices.RemoveRoleFromUser(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetRoleByIdAsync/{roleId}")]
        public async Task<IActionResult> GetRoleById(string roleId)
        {
            var response = await roleServices.GetRoleById(roleId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

    }
}
