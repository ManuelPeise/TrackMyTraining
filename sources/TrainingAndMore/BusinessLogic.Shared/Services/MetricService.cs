using BusinessLogic.Shared.Interfaces;
using BusinessLogic.Shared.Models;
using BusinessLogic.Shared.Repositories;
using Data.Context;
using Data.Entities;
using Newtonsoft.Json;
using Shared.Enums;

namespace BusinessLogic.Shared.Services
{
    public class MetricService : ABusinessLogic, IMetricService
    {
        private AppDataContext _context { get; set; }
        private IDbRepositoryBase<MetricEntity> _metricRepository;

        public MetricService(AppDataContext context) : base(context)
        {
            _context = context;
            _metricRepository = new DatabaseRepository<MetricEntity>(context);
        }

        public async Task<List<T>> GetMetrics<T>(MetricTypeEnum type, DateTime? from = null)
        {
            try
            {
                Func<MetricEntity, bool> typeQuery = x => x.Type == type;
                Func<MetricEntity, bool> query = x => x.Type == type && DateTime.Parse(x.CreatedAt) >= from;

                var metrics = await _metricRepository.GetBy(new DbQueryOptions<MetricEntity>
                {
                    AsNoTracking = true,
                    WhereExpression = from == null ? typeQuery : query
                });

                if (metrics == null)
                {
                    return new List<T>();
                }

                return (from metric in metrics
                        where metric.Json != null && !string.IsNullOrWhiteSpace(metric.Json)
                        select JsonConvert.DeserializeObject<T>(metric.Json)).ToList();

            }
            catch (Exception exception)
            {
                await LogMessage(new LogMessageEntity
                {
                    Message = $"Could not load metric type of {nameof(type)}",
                    ExceptionMessage = exception.Message,
                    MessageType = LogMessageType.Error,
                    TimeStamp = DateTime.Now,
                    Type = nameof(MetricService),
                });

                return new List<T>();
            }
        }

        public async Task SaveMetric(MetricEntity entity)
        {
            try
            {
                await _metricRepository.AddAsync(entity);

                await SaveChanges();
            }
            catch (Exception exception)
            {
                await LogMessage(new LogMessageEntity
                {
                    Message = $"Could not store metric type of {nameof(entity.Type)}",
                    ExceptionMessage = exception.Message,
                    MessageType = LogMessageType.Error,
                    TimeStamp = DateTime.Now,
                    Type = nameof(MetricService),
                });
            }
        }

    }
}
