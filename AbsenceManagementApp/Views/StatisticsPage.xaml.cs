using AbsenceManagementApp.Services;
using AbsenceManagementApp.ViewModels;
using Microsoft.Maui.Controls;

namespace AbsenceManagementApp.Views
{
    public partial class StatisticsPage : ContentPage
    {
        private StatisticsViewModel _viewModel;

        public StatisticsPage(IApiService apiService)
        {
            InitializeComponent();
            _viewModel = new StatisticsViewModel(apiService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                await _viewModel.LoadStudentsCommand.ExecuteAsync(null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement des étudiants: {ex.Message}");
            }
        }
    }
}

