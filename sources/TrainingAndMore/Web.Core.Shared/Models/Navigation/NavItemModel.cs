namespace Web.Core.Shared.Models.Navigation
{
    public class NavItemModel
    {
        public string? Id { get; set; }
        public string? GroupName { get; set; }
        public string? Icon { get; set; }
        public string? ControlId { get; set; }
        public List<NavigationItem> Items { get; set; } = new List<NavigationItem>();
    }

    public class NavigationItem
    {
        public string? Label { get; set; }
        public string? To { get; set; }
    }
}
