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
    public class StatusController : Controller
    {
        private readonly ILogger _logger;
        private readonly IStatusService _statusService;
        public StatusController(
          ILogger<StatusController> logger,
          IStatusService statusService)
        {
            _logger = logger;
            _statusService = statusService;
        }
        [HttpGet]
        public IActionResult GetStatuses()
        {
            var response = _statusService.GetStatuses();
            return Ok(response);
        }
    }
}
