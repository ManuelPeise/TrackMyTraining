namespace Web.Core.Components.Pages.Auth
{
    public partial class RegisterPage
    {
        public bool IsLoading { get; set; } = false;

        private void SetIsLoading(bool isLoading)
        {
            IsLoading = isLoading;
        }
    }
}
