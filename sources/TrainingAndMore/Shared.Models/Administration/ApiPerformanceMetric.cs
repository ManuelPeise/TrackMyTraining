using Shared.Enums;

namespace Shared.Models.Administration
{
    public class ApiPerformanceMetric
    {
        public MetricTypeEnum MetricType { get; set; }
        public string Controller { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public double RequestDuration { get; set; }
        public long ResponsLength { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
