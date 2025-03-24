using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace AbsenceManagementApp.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        public IAsyncRelayCommand ManageParentsCommand { get; }
        public IAsyncRelayCommand ManageTeachersCommand { get; }
        public IAsyncRelayCommand ManageStudentsCommand { get; }
        public IAsyncRelayCommand ManageClassesCommand { get; }
        public IAsyncRelayCommand GlobalStatsCommand { get; }
        public IAsyncRelayCommand ExportDataCommand { get; }

        public AdminViewModel()
        {
            Title = "Administration";
            
            ManageParentsCommand = new AsyncRelayCommand(ManageParentsAsync);
            ManageTeachersCommand = new AsyncRelayCommand(ManageTeachersAsync);
            ManageStudentsCommand = new AsyncRelayCommand(ManageStudentsAsync);
            ManageClassesCommand = new AsyncRelayCommand(ManageClassesAsync);
            GlobalStatsCommand = new AsyncRelayCommand(GlobalStatsAsync);
            ExportDataCommand = new AsyncRelayCommand(ExportDataAsync);
        }

        private async Task ManageParentsAsync()
        {
            await DisplayAlert("Fonctionnalité à venir", "La gestion des parents sera disponible dans une prochaine mise à jour.", "OK");
        }

        private async Task ManageTeachersAsync()
        {
            await DisplayAlert("Fonctionnalité à venir", "La gestion des enseignants sera disponible dans une prochaine mise à jour.", "OK");
        }

        private async Task ManageStudentsAsync()
        {
            await DisplayAlert("Fonctionnalité à venir", "La gestion des étudiants sera disponible dans une prochaine mise à jour.", "OK");
        }

        private async Task ManageClassesAsync()
        {
            await DisplayAlert("Fonctionnalité à venir", "La gestion des classes sera disponible dans une prochaine mise à jour.", "OK");
        }

        private async Task GlobalStatsAsync()
        {
            await DisplayAlert("Fonctionnalité à venir", "Les statistiques globales seront disponibles dans une prochaine mise à jour.", "OK");
        }

        private async Task ExportDataAsync()
        {
            await DisplayAlert("Fonctionnalité à venir", "L'exportation des données sera disponible dans une prochaine mise à jour.", "OK");
        }
    }
}

