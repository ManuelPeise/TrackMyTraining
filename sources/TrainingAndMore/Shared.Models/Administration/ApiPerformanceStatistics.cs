namespace Shared.Models.Administration
{
    public class ApiPerformanceStatistics
    {
        public DateTime Date { get; set; }
        public string? Controller { get; set; }
        public int RequestCount { get; set; }
        public int AverageRequestDuration { get; set; }
        public int AverageResponseSize { get; set; }
        public List<EndPointPerformance> EndpointPerformance { get; set; } = new List<EndPointPerformance>();
    }

    public class EndPointPerformance
    {
        public string? Endpoint { get; set; }
        public int RequestCount { get; set; }
        public int StatusCode { get; set; }
        public int AverageRequestDuration { get; set; }
        public int AverageResponseSize { get; set; }
    }
}
