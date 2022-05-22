using ApplicationCore.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UserManagement_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : Controller
    {
        private readonly ILogger _logger;
        private readonly IPermissionService _permissionService;
        public PermissionController(
          ILogger<PermissionController> logger,
          IPermissionService permissionService)
        {
            _logger = logger;
            _permissionService = permissionService;
        }
        [HttpGet]
        public IActionResult GetPermissions()
        {
            var response = _permissionService.GetPermissions();
            return Ok(response);
        }
    }
}
