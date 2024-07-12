using Eazy.Credit.Security.Contracts.Identity;
using Eazy.Credit.Security.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Eazy.Credit.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/workflow")]
    [ApiController]
    public class UserAPIServices : ControllerBase
    {
        private readonly IUserServices userServices;

        public UserAPIServices(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpPost("AddUserAsync")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto request)
        {
            var response = await userServices.CreateUser(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPut("UpdateUserAsync")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto request)
        {
            var response = await userServices.UpdateUser(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpDelete("DeleteUserAsync/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var response = await userServices.DeleteUser(userId);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetUserAsync")]
        public async Task<IActionResult> GetUser(string email)
        {
            var response = await userServices.GetUser(email);

            if (response == null)
                return BadRequest(new { message = "email is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetAllUserAsync")]
        public async Task<IActionResult> GetAllUser()
        {
            var response = await userServices.GetAllUser();

            return Ok(response);
        }

        [HttpGet("GetUserByIdAsync/{loginId}")]
        public async Task<IActionResult> GetUserByIdAsync(string loginId)
        {
            var response = await userServices.GetUserByIdAsync(loginId);

            if (response == null)
                return BadRequest(new { message = "email is incorrect" });

            return Ok(response);
        }

        [HttpGet("GetAllUsersByRoleIdAsync/{roleId}")]
        public async Task<IActionResult> GetAllUsersByRoleId(string roleId)
        {
            var response = await userServices.GetAllUsersByRoleId(roleId);

            return Ok(response);
        }

    }
}
