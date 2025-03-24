using System;
using System.Threading.Tasks;
using AbsenceManagementApp.Services;
using AbsenceManagementApp.Views;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;

namespace AbsenceManagementApp.ViewModels
{
    public class ParentHomeViewModel : BaseViewModel
    {
        private readonly AuthService _authService;

        public IAsyncRelayCommand NavigateToAbsencesCommand { get; }
        public IAsyncRelayCommand NavigateToNotificationsCommand { get; }
        public IAsyncRelayCommand NavigateToStatisticsCommand { get; }
        public IAsyncRelayCommand LogoutCommand { get; }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public ParentHomeViewModel(AuthService authService)
        {
            _authService = authService;
            Title = "Accueil Parent";

            NavigateToAbsencesCommand = new AsyncRelayCommand(NavigateToAbsencesAsync);
            NavigateToNotificationsCommand = new AsyncRelayCommand(NavigateToNotificationsAsync);
            NavigateToStatisticsCommand = new AsyncRelayCommand(NavigateToStatisticsAsync);
            LogoutCommand = new AsyncRelayCommand(LogoutAsync);

            // Charger le nom d'utilisateur
            LoadUserInfoAsync();
        }

        private async void LoadUserInfoAsync()
        {
            try
            {
                Username = await _authService.GetUsernameAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement des informations utilisateur: {ex.Message}");
            }
        }

        private async Task NavigateToAbsencesAsync()
        {
            try
            {
                await Shell.Current.GoToAsync("//absences");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Impossible de naviguer vers la page des absences: {ex.Message}", "OK");
            }
        }

        private async Task NavigateToNotificationsAsync()
        {
            try
            {
                await Shell.Current.GoToAsync("//notifications");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Impossible de naviguer vers la page des notifications: {ex.Message}", "OK");
            }
        }

        private async Task NavigateToStatisticsAsync()
        {
            try
            {
                await Shell.Current.GoToAsync("//statistics");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Impossible de naviguer vers la page des statistiques: {ex.Message}", "OK");
            }
        }

        private async Task LogoutAsync()
        {
            try
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert(
                    "Déconnexion", 
                    "Êtes-vous sûr de vouloir vous déconnecter ?", 
                    "Oui", "Non");

                if (confirm)
                {
                    await _authService.LogoutAsync();
                    
                    // Rediriger vers la page de connexion
                    Application.Current.MainPage = new NavigationPage(new LoginPage(_authService));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Impossible de se déconnecter: {ex.Message}", "OK");
            }
        }
    }
}

