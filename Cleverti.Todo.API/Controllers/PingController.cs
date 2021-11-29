using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cleverti.Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly ILogger<PingController> _logger;

        public PingController(ILogger<PingController> logger)
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        // GET: api/ping
        [HttpGet]
        public string Get()
        {
            return "Ping received";
        }

        // GET api/ping/with-auth
        [HttpGet("with-auth")]
        [Authorize]
        public string GetWithAuth()
        {
            return $"Successful Authorization! Welcome : {User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value}";
        }
    }
}
