using BusinessLogic.Shared.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Web.Core.Shared.Services.Interfaces;

namespace Web.Core.Shared.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private bool _isLoading = false;
        private bool _isInitialized = false;
        private readonly IHttpService _httpService;
        private readonly IJwtTokenService _tokenService;

        public ViewModelBase(IHttpService httpService, IJwtTokenService tokenService)
        {
            _httpService = httpService;
            _tokenService = tokenService;
        }


        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                SetPropertyValue(ref _isLoading, value);
            }
        }
        public bool IsInitialized
        {
            get
            {
                return _isInitialized;
            }
            set
            {
                SetPropertyValue(ref _isInitialized, value);
            }
        }

        public IHttpService HttpService { get => _httpService; }
        public IJwtTokenService TokenService { get => _tokenService; }
        protected void SetPropertyValue<T>(ref T field, T value, [CallerMemberName] string? property = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;

            field = value;

            OnPropertyChanged(property);
        }

        protected void OnPropertyChanged([CallerMemberName] string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
