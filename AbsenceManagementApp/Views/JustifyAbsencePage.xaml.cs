using AbsenceManagementApp.Services;
using AbsenceManagementApp.ViewModels;
using Microsoft.Maui.Controls;

namespace AbsenceManagementApp.Views
{
    public partial class JustifyAbsencePage : ContentPage
    {
        public JustifyAbsencePage(IApiService apiService)
        {
            InitializeComponent();
            BindingContext = new JustifyAbsenceViewModel(apiService);
        }
    }
}

