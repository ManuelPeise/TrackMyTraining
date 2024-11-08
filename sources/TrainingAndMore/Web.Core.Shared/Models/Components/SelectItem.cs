using Web.Core.Shared.ViewModels;

namespace Web.Core.Shared.Models.Components
{
    public class SelectItem
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public bool Selected { get; set; }
        public OnApiPerformanceDataChangedDelegate? OnApiPerformanceDataChangedDelegate { get; set; }
        public void OnSelect()
        {
            OnApiPerformanceDataChangedDelegate?.Invoke(Id);
        }
    }
}
