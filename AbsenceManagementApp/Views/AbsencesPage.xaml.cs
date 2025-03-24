using AbsenceManagementApp.Services;
using AbsenceManagementApp.ViewModels;
using Microsoft.Maui.Controls;

namespace AbsenceManagementApp.Views
{
    public partial class AbsencesPage : ContentPage
    {
        private AbsencesViewModel _viewModel;

        public AbsencesPage(IApiService apiService)
        {
            InitializeComponent();
            _viewModel = new AbsencesViewModel(apiService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadAbsencesAsync();
        }
    }
}

