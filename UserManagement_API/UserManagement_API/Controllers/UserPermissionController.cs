using ApplicationCore.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPermissionController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserPermissionService _userPermissionService;
        public UserPermissionController(
            ILogger<UserController> logger,
            IUserPermissionService userPermissionService)
        {
            _logger = logger;
            _userPermissionService = userPermissionService;
        }
        [HttpGet("{id}")]
        public IActionResult GetUserPermissionsByUserId(int id)
        {
            var response = _userPermissionService.GetUserPermissions(id);
            return Ok(response);
        }
       
    }
}
