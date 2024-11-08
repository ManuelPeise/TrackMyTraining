namespace Shared.Models.Administration
{
    public class ApiPerformancData
    {
        public List<string> Controllers { get; set; } = new List<string>();
        public List<ApiPerformanceStatistics> ApiPeformance { get; set; } = new List<ApiPerformanceStatistics>();
    }
}
