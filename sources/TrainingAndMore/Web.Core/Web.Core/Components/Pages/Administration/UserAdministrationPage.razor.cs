
namespace Web.Core.Components.Pages.Administration
{
    public partial class UserAdministrationPage
    {
        private int _section = 0;

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        private void HandleSelectedSectionChanged(int id)
        {
            _section = id;
        }
    }
}
