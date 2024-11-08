using BusinessLogic.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using Shared.Models.Administration;
using Web.Core.Shared.Models.Components;
using Web.Core.Shared.Services.Interfaces;
using Web.Core.Shared.ViewModels.Interfaces;

namespace Web.Core.Shared.ViewModels
{
    public delegate void OnApiPerformanceDataChangedDelegate(int id);

    public class ApiPerformanceViewModel : ViewModelBase, IApiPerformanceViewModel
    {

        public ApiPerformanceViewModel(IHttpService httpService, IJwtTokenService tokenService) : base(httpService, tokenService) { }

        private List<ApiPerformanceStatistics> _apiPerformanceStatistics = new List<ApiPerformanceStatistics>();
        private List<SelectItem> _controllerDropdownItems = new List<SelectItem>();
        private SelectItem _selectedController = new SelectItem();
        private OnApiPerformanceDataChangedDelegate? OnChange;
        private int? _apiStaticticId;

        public List<SelectItem> ControllerDropdownItems
        {
            get => _controllerDropdownItems;

            set
            {
                if (_controllerDropdownItems != value)
                {
                    _controllerDropdownItems = value;
                    OnPropertyChanged(nameof(ControllerDropdownItems));
                }
            }
        }
        public List<ApiPerformanceStatistics> ApiPerformanceStatistics
        {
            get
            {
                return _apiPerformanceStatistics;
            }

            private set
            {
                if (_apiPerformanceStatistics != value)
                {
                    _apiPerformanceStatistics = value;
                    OnPropertyChanged(nameof(ApiPerformanceStatistics));
                }
            }
        }
        public int ApiStatisticId
        {
            get => _apiStaticticId ?? 0;
            private set
            {
                if (_apiStaticticId != value)
                {
                    _apiStaticticId = value;

                    OnPropertyChanged(nameof(ApiStatisticId));
                }
            }
        }
        public SelectItem SelectedController
        {
            get
            {
                return _selectedController;
            }
            private set
            {
                if (_selectedController != value)
                {
                    _selectedController = value;
                    OnPropertyChanged(nameof(SelectedController));
                }
            }
        }

        public async Task InitializeAsync()
        {
            IsInitialized = false;
            IsLoading = true;

            await LoadDataAsync();

            LoadDropdownItems(0);

            SetSelectedController(0);

            IsLoading = false;
            IsInitialized = true;
        }

        public void HandleSelectedItemChanged(ChangeEventArgs e)
        {
            var id = _controllerDropdownItems.FirstOrDefault(x => x.Value == (string)e.Value)?.Id;

            if (id != null)
            {
                ApiStatisticId = (int)id;

                SetSelectedController((int)id);

                LoadDropdownItems((int)id);
            }
        }

        private async Task LoadDataAsync()
        {
            var jwtToken = await TokenService.GetToken();

            var response = await HttpService.SendGetRequest<List<ApiPerformanceStatistics>>("ApiPerformance/GetApiPerformance", jwtToken);

            if (response != null)
            {
                ApiPerformanceStatistics = response;
            }
        }

        private void SetSelectedController(int id)
        {
            SelectedController = new SelectItem
            {
                Id = id,
                Value = _apiPerformanceStatistics[id]?.Controller ?? string.Empty,
                Selected = true
            };
        }

        private void LoadDropdownItems(int selectecControllerId)
        {
            if (_apiPerformanceStatistics.Select(x => x.Controller).Any())
            {
                var id = 0;

                var controllerItems = (from contoller in _apiPerformanceStatistics.Select(x => x.Controller).Distinct()
                                       let controllerId = id++
                                       select new SelectItem
                                       {
                                           Id = controllerId,
                                           Selected = false,
                                           Value = contoller,
                                           OnApiPerformanceDataChangedDelegate = OnChange
                                       }).ToList();

                ControllerDropdownItems = new List<SelectItem>(controllerItems);
            }
        }

    }
}
