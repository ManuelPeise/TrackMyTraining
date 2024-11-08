using BusinessLogic.Shared.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Enums;
using Shared.Models.Administration;
using System.Diagnostics;


namespace Service.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MeasuredApiControllerBase: ControllerBase
    {
        private IMetricService _metricService;
        private Stopwatch _stopwatch;

        public MeasuredApiControllerBase(IMetricService metricService)
        {
            _metricService = metricService;
            _stopwatch = new Stopwatch();

            _stopwatch.Start();
        }

        [NonAction]
        public async Task MonitorEndpointPerformance<T>(string controller, string action, T data)
        {
            _stopwatch.Stop();

            var length = JsonConvert.SerializeObject(data, Formatting.Indented).Length;
            await _metricService.SaveMetric(new MetricEntity
            {
                Type = MetricTypeEnum.Api,
                Json = JsonConvert.SerializeObject(new ApiPerformanceMetric
                {
                    Controller = controller,
                    Action = action,
                    Endpoint = $"{controller}/{action}",
                    StatusCode = HttpContext.Response.StatusCode,
                    ResponsLength = length,
                    MetricType = MetricTypeEnum.Api,
                    RequestDuration = _stopwatch.Elapsed.Milliseconds,
                    TimeStamp = DateTime.Now
                })
            });

            _stopwatch.Reset();
        }
    }
}
