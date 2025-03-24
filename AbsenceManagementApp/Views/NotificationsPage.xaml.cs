using AbsenceManagementApp.Services;
using AbsenceManagementApp.ViewModels;
using Microsoft.Maui.Controls;

namespace AbsenceManagementApp.Views
{
    public partial class NotificationsPage : ContentPage
    {
        private NotificationsViewModel _viewModel;

        public NotificationsPage(IApiService apiService)
        {
            InitializeComponent();
            _viewModel = new NotificationsViewModel(apiService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadNotificationsAsync();
        }
    }
}

