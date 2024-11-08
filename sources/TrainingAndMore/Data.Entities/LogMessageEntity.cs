using Shared.Enums;

namespace Data.Entities
{
    public class LogMessageEntity:AEntityBase
    {
        public string Message { get; set; } = string.Empty;
        public string? ExceptionMessage { get; set; }
        public LogMessageType MessageType { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; }
    }
}
