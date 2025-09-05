using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserManagement.Constants;
using UserManagement.Constants.Enums;
using UserManagement.Core.Implementation;
using UserManagement.Core.Interfaces;
using UserManagement.Enities;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private string CurrentUser => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
  
        private string CurrentIp => HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll() => Ok(await _userService.GetAllAsync());

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User,ReadOnlyUser")]
        public async Task<IActionResult> Get(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(User user)
        {
            var created = await _userService.CreateAsync(user, CurrentUser);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        //[Authorize]
        public async Task<IActionResult> Update(long id, User user)
        {
            if (id != user.Id) return BadRequest();
            var updated = await _userService.UpdateAsync(user, CurrentUser, CurrentIp);
            return Ok(updated);
        }
        [HttpGet("searchusers")]
        [Authorize]
        public async Task<IActionResult> GetUsersSearch([FromQuery] string? search, [FromQuery] int? role, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (users, totalCount) = await _userService.GetUsersAsync(search, role, page, pageSize);

            return Ok(new { data = users, totalCount });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            await _userService.DeleteAsync(id, CurrentUser, CurrentIp);
            return NoContent();
        }
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
            var user = new User
            {
               
                Username = request.Username,
                Email = request.Email,
                RoleId = request.RoleId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password,passwordSalt),
                PasswordSalt=passwordSalt,
                CreatedAt = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow,
                IsDeleted=false,
                //CreatedBy = CurrentUser,
                //LastModifiedBy = CurrentUser,
                //LastModifiedIp = CurrentIp
            };

            await _userService.CreateAsync(user, CurrentUser);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }



    }
}