namespace Web.Core.Components.Pages.Auth
{
    public partial class LoginPage
    {
        public bool IsLoading { get; set; }

        private void SetIsLoading(bool isLoading)
        {
            IsLoading = isLoading;
        }
    }
}
