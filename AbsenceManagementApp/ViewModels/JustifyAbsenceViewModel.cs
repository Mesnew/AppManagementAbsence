using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AbsenceManagementApp.Services;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;

namespace AbsenceManagementApp.ViewModels
{
    [QueryProperty(nameof(AbsenceId), "id")]
    public class JustifyAbsenceViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        private int _absenceId;
        public int AbsenceId
        {
            get => _absenceId;
            set => SetProperty(ref _absenceId, value);
        }

        private string _reason;
        public string Reason
        {
            get => _reason;
            set => SetProperty(ref _reason, value);
        }

        private string _document;
        public string Document
        {
            get => _document;
            set => SetProperty(ref _document, value);
        }

        public IAsyncRelayCommand JustifyCommand { get; }
        public IAsyncRelayCommand SelectDocumentCommand { get; }

        public JustifyAbsenceViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "Justifier une absence";
            
            JustifyCommand = new AsyncRelayCommand(JustifyAbsenceAsync);
            SelectDocumentCommand = new AsyncRelayCommand(SelectDocumentAsync);
        }

        private async Task JustifyAbsenceAsync()
        {
            if (string.IsNullOrWhiteSpace(Reason))
            {
                await DisplayAlert("Erreur", "Veuillez saisir un motif d'absence.", "OK");
                return;
            }

            IsBusy = true;

            try
            {
                var result = await _apiService.JustifyAbsenceAsync(AbsenceId, Reason, Document);

                if (result)
                {
                    await DisplayAlert("Succès", "L'absence a été justifiée avec succès.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await DisplayAlert("Erreur", "Impossible de justifier l'absence.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Une erreur est survenue : {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SelectDocumentAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Sélectionner un justificatif"
                });

                if (result != null)
                {
                    Document = result.FileName;
                    // Ici, vous pourriez implémenter le chargement du fichier
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Impossible de sélectionner un document : {ex.Message}", "OK");
            }
        }
    }
}

