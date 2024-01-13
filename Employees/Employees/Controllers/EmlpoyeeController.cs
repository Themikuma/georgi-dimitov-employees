using Employees.Interfaces;
using Employees.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmlpoyeeController : ControllerBase
    {
        private readonly ILogger<EmlpoyeeController> _logger;
        private readonly IEmlpoymentService _emlpoymentService;

        public EmlpoyeeController(ILogger<EmlpoyeeController> logger, IEmlpoymentService emlpoymentService)
        {
            _emlpoymentService = emlpoymentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<CommonEmployment> Get()
        {
            return await _emlpoymentService.CalculateLongestCommonEmlpoyment();
        }
    }
}