using ApplicationCore.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using UserManagement.Domain.Models.ExceptionHandling;
using UserManagement.Domain.Models.User;

namespace UserManagement_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        public UserController(
            ILogger<UserController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpGet("all")]
        public IActionResult GetUsers()
        {
            var response = _userService.GetUsers();
            return Ok(response);
        }

        [HttpPost("list")]
        public IActionResult GetUserPaginated([FromBody] UserSearchFilterDto model)
        {
            var response = _userService.GetUsersPaginated(model.page, model.pageSize, model.search, model.sort, model.order, model.statusId, model.permissions);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var response = _userService.GetUserById(id);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO model)
        {
            var response = _userService.CreateUser(model);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var response = _userService.DeleteUser(id);
            return Ok(response);
        }

        [HttpPost("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO model)
        {
            var response = _userService.EditUser(model);
            return Ok(response);
        }
        [HttpPost("AssignUserPermissions")]
        public IActionResult PostUserPermissions([FromBody] UserPermissionsDTO model)
        {
            var response = _userService.AssignUserPermissions(model);
            return Ok(response);
        }
    }
}
