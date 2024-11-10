using BusinessLogic.Shared;
using BusinessLogic.Shared.Interfaces;
using Data.Context;
using Data.Entities;
using Shared.Enums;
using Shared.Models.Administration;

namespace BusinessLogic.Administration.Services
{
    public class ApiPerformanceService : ABusinessLogic, IApiPerformanceService
    {
        private readonly AppDataContext _context;
        private readonly IMetricService _metricService;

        public ApiPerformanceService(AppDataContext context, IMetricService metricService) : base(context)
        {
            _context = context;
            _metricService = metricService;
        }

        public async Task<List<ApiPerformanceStatistics>?> GetApiPerformance()
        {
            try
            {
                var metrics = await _metricService.GetMetrics<ApiPerformanceMetric>(MetricTypeEnum.Api, DateTime.Parse($"{DateTime.Now.ToString("dd.MM.yyyy")}").AddDays(-7));

                var preformanceData = (from metric in metrics
                                       group metric by new { metric.Controller, metric.TimeStamp.Date } into metricGroup
                                       select new ApiPerformanceStatistics
                                       {
                                           Date = metricGroup.Key.Date,
                                           Controller = metricGroup.Key.Controller,
                                           AverageRequestDuration = (int)metricGroup.Sum(x => x.RequestDuration),
                                           AverageResponseSize = (int)metricGroup.Sum(x => x.ResponsLength),
                                           RequestCount = metricGroup.Count(),
                                           EndpointPerformance = (from metric in metricGroup
                                                                 group metric by new { metric.Controller, metric.Action } into actionGroup
                                                                 select new EndPointPerformance
                                                                 {
                                                                     AverageRequestDuration = (int)actionGroup.Sum(x => x.RequestDuration),
                                                                     AverageResponseSize = (int)actionGroup.Sum((x) => x.ResponsLength),
                                                                     RequestCount = (int)actionGroup.Count(),
                                                                     Endpoint = $"{actionGroup.Key.Controller}/{actionGroup.Key.Action}",
                                                                 }).ToList()
                                       }).ToList();

                

               return preformanceData;
            }
            catch (Exception exception)
            {

                await LogMessage(new LogMessageEntity
                {
                    Message = "Could not load user administration page data.",
                    ExceptionMessage = exception.Message,
                    MessageType = LogMessageType.Error,
                    Type = nameof(ApiPerformanceService),
                    TimeStamp = DateTime.Now,
                });

                return null;
            }

        }

        private double GetAverage(double value, int count)
        {
            if (value == 0 || count == 0)
            {
                return value;
            }

            return value / count;
        }
        #region dispose

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
