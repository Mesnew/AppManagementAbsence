using AbsenceManagementApp.Services;
using AbsenceManagementApp.ViewModels;
using Microsoft.Maui.Controls;

namespace AbsenceManagementApp.Views
{
    public partial class ParentHomePage : ContentPage
    {
        public ParentHomePage(AuthService authService)
        {
            InitializeComponent();
            BindingContext = new ParentHomeViewModel(authService);
        }
    }
}

