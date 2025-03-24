using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AbsenceManagementApp.Models;
using AbsenceManagementApp.Services;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;

namespace AbsenceManagementApp.ViewModels
{
    public class AbsencesViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        public ObservableCollection<Absence> Absences { get; } = new ObservableCollection<Absence>();

        private Absence _selectedAbsence;
        public Absence SelectedAbsence
        {
            get => _selectedAbsence;
            set
            {
                if (SetProperty(ref _selectedAbsence, value) && value != null)
                {
                    JustifyAbsenceCommand.Execute(value);
                }
            }
        }

        public IAsyncRelayCommand RefreshCommand { get; }
        public IAsyncRelayCommand<Absence> JustifyAbsenceCommand { get; }

        public AbsencesViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "Absences";
            
            RefreshCommand = new AsyncRelayCommand(LoadAbsencesAsync);
            JustifyAbsenceCommand = new AsyncRelayCommand<Absence>(NavigateToJustifyAbsence);
        }

        public async Task LoadAbsencesAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Absences.Clear();
                var absences = await _apiService.GetAbsencesAsync();
                foreach (var absence in absences)
                {
                    Absences.Add(absence);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Impossible de charger les absences : {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task NavigateToJustifyAbsence(Absence absence)
        {
            if (absence.Status == "justifiée")
            {
                await DisplayAlert("Information", "Cette absence est déjà justifiée.", "OK");
                return;
            }

            // Navigation vers la page de justification
            await Shell.Current.GoToAsync($"justifyAbsence?id={absence.Id}");
        }
    }
}

