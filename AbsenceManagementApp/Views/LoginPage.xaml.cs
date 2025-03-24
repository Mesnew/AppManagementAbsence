using AbsenceManagementApp.Services;
using AbsenceManagementApp.ViewModels;
using Microsoft.Maui.Controls;

namespace AbsenceManagementApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(AuthService authService)
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(authService, Navigation);
        }
    }
}

