using Shared.Enums;

namespace Data.Entities
{
    public class MetricEntity:AEntityBase
    {
        public MetricTypeEnum Type { get; set; }
        public string? Json { get; set; }
    }
}
