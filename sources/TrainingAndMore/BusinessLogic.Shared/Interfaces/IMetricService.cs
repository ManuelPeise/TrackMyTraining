using Data.Entities;
using Shared.Enums;

namespace BusinessLogic.Shared.Interfaces
{
    public interface IMetricService
    {
        Task<List<T>> GetMetrics<T>(MetricTypeEnum type, DateTime? from = null);
        Task SaveMetric(MetricEntity entity);
    }
}
