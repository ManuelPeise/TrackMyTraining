namespace Web.Core.Shared.ViewModels.Interfaces
{
    public interface IViewModelBase
    {
        public bool IsLoading { get; set; }
        public bool IsInitialized { get; set; }
    }
}
