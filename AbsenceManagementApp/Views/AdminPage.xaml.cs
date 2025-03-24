using AbsenceManagementApp.ViewModels;
using Microsoft.Maui.Controls;

namespace AbsenceManagementApp.Views
{
    public partial class AdminPage : ContentPage
    {
        public AdminPage()
        {
            InitializeComponent();
            BindingContext = new AdminViewModel();
        }
    }
}