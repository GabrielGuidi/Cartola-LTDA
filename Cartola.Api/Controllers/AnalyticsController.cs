using Cartola.Domain.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Cartola.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : Controller
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _analyticsService.Analyze();
            return Ok(result);
        }

        [HttpGet("sql")]
        public IActionResult GetSql()
        {
            var result = _analyticsService.sql();
            return Ok(result);
        }
    }
}
