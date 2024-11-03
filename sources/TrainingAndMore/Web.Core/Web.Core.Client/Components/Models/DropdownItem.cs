using Shared.Enums;

namespace Web.Core.Client.Components.Models
{
    public class DropdownItem
    {
        public DropdownItemTypeEnum ItemType { get; set; }
        public string? To { get; set; }
        public string? Label { get; set; }
        public Delegates.AsyncCallback? Callback { get; set; }

    }
}
