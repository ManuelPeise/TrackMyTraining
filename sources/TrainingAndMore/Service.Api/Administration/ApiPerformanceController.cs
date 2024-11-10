using BusinessLogic.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Administration;


namespace Service.Api.Administration
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiPerformanceController : MeasuredApiControllerBase
    {
        private readonly IApiPerformanceService _service;

        public ApiPerformanceController(IApiPerformanceService service, IMetricService metricService) : base(metricService)
        {
            _service = service;
        }

        [HttpGet(Name = "GetApiPerformance")]
        public async Task<List<ApiPerformanceStatistics>?> GetApiPerformance()
        {
            var response = await _service.GetApiPerformance();
            
            await MonitorEndpointPerformance("ApiPerformance","GetApiPerformance", response);

            return response;
        }
    }
}
