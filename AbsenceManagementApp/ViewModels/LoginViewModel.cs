using System;
using System.Threading.Tasks;
using AbsenceManagementApp.Services;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;

namespace AbsenceManagementApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly INavigation _navigation;

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        // Propriété pour activer/désactiver le bouton de connexion
        public bool IsNotBusy => !IsBusy;

        public IAsyncRelayCommand LoginCommand { get; }

        public LoginViewModel(AuthService authService, INavigation navigation)
        {
            _authService = authService;
            _navigation = navigation;
            Title = "Connexion";
            
            // Pour le débogage, préremplir les champs
            #if DEBUG
            Username = "parent";
            Password = "password";
            #endif
            
            LoginCommand = new AsyncRelayCommand(LoginAsync);
            
            // S'assurer qu'aucune donnée d'authentification n'est présente
            _authService.ClearAuthDataAsync();
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await DisplayAlert("Erreur", "Veuillez saisir un nom d'utilisateur et un mot de passe.", "OK");
                return;
            }

            IsBusy = true;
            OnPropertyChanged(nameof(IsNotBusy));

            try
            {
                var (success, role) = await _authService.LoginAsync(Username, Password);

                if (success)
                {
                    await DisplayAlert("Succès", "Connexion réussie!", "OK");
                    
                    // Redirection basée sur le rôle
                    switch (role.ToLower())
                    {
                        case "parent":
                            Application.Current.MainPage = new AppShell();
                            break;
                        case "admin":
                            // Redirection vers la page admin (à implémenter si nécessaire)
                            await DisplayAlert("Information", "Connexion en tant qu'administrateur. Cette fonctionnalité n'est pas encore implémentée.", "OK");
                            break;
                        case "professeur":
                            // Redirection vers la page professeur (à implémenter si nécessaire)
                            await DisplayAlert("Information", "Connexion en tant que professeur. Cette fonctionnalité n'est pas encore implémentée.", "OK");
                            break;
                        case "eleve":
                            // Redirection vers la page élève (à implémenter si nécessaire)
                            await DisplayAlert("Information", "Connexion en tant qu'élève. Cette fonctionnalité n'est pas encore implémentée.", "OK");
                            break;
                        default:
                            await DisplayAlert("Erreur", "Rôle non reconnu.", "OK");
                            break;
                    }
                }
                else
                {
                    await DisplayAlert("Erreur", "Nom d'utilisateur ou mot de passe incorrect.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Une erreur est survenue : {ex.Message}", "OK");
                System.Diagnostics.Debug.WriteLine($"Erreur de connexion: {ex}");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }
    }
}

