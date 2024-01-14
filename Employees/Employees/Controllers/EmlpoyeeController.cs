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
        private readonly IDataIngestionService _dataIngestionService;

        public EmlpoyeeController(ILogger<EmlpoyeeController> logger, IEmlpoymentService emlpoymentService, IDataIngestionService dataIngestionService)
        {
            _logger = logger;
            _emlpoymentService = emlpoymentService;
            _dataIngestionService = dataIngestionService;
        }

        [HttpPost]
        public CommonEmployment Post([FromBody] string content)
        {
            IEnumerable<EmploymentRecord> records = _dataIngestionService.ReadRecords(content);
            return _emlpoymentService.CalculateLongestCommonEmlpoyment(records);
        }
    }
}
