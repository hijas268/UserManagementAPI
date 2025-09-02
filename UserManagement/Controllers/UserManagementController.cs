using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Constants;
using UserManagement.Constants.Enums;
using UserManagement.Core.Interfaces;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpPost("create")]
        [Route(ApiMethods.CreateUser)]
        public async Task<IActionResult> CreateUser([FromBody] UserDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Invalid request payload.");
            }

            var result = await _userManagementService.CreateUser(requestDto);

            if (result.ResponseCode==ResponseCode.Success)
            {
                return Ok(result);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
