using AbsenceManagementApp.Services;
using AbsenceManagementApp.Views;
using Microsoft.Maui.Controls;

namespace AbsenceManagementApp
{
    public partial class App : Application
    {
        private readonly AuthService _authService;
        private readonly LoginPage _loginPage;
        
        public App(AuthService authService, LoginPage loginPage)
        {
            InitializeComponent();
            _authService = authService;
            _loginPage = loginPage;
            
            // Définir la page de connexion par défaut
            MainPage = new NavigationPage(_loginPage);
            
            // Vérifier si l'utilisateur est déjà connecté
            CheckLoginStatus();
        }
        
        private async void CheckLoginStatus()
        {
            try
            {
                if (await _authService.IsAuthenticatedAsync())
                {
                    // L'utilisateur est déjà connecté, vérifier son rôle
                    var role = await _authService.GetRoleAsync();
                    
                    if (role?.ToLower() == "parent")
                    {
                        MainPage = new AppShell();
                    }
                }
            }
            catch (Exception ex)
            {
                // En cas d'erreur, rester sur la page de connexion
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la vérification du statut de connexion: {ex.Message}");
            }
        }
    }
}

