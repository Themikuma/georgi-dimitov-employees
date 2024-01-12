using Employees.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmlpoyeeController : ControllerBase
    {
        private readonly ILogger<EmlpoyeeController> _logger;

        public EmlpoyeeController(ILogger<EmlpoyeeController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<CommonEmployment> Get()
        {
            return new List<CommonEmployment>();
        }
    }
}
