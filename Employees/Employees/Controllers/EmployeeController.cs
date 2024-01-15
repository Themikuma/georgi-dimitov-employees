using Employees.Interfaces;
using Employees.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmlpoymentService _emlpoymentService;
        private readonly IDataIngestionService _dataIngestionService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmlpoymentService emlpoymentService, IDataIngestionService dataIngestionService)
        {
            _logger = logger;
            _emlpoymentService = emlpoymentService;
            _dataIngestionService = dataIngestionService;
        }

        [HttpPost]
        public CommonEmployment Post([FromBody] Request request)
        {
            IEnumerable<EmploymentRecord> records = _dataIngestionService.ReadRecords(request.Content);
            return _emlpoymentService.CalculateLongestCommonEmlpoyment(records);
        }
    }
}
