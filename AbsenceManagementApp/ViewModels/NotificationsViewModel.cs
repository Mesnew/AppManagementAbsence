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
    public class NotificationsViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        public ObservableCollection<Notification> Notifications { get; } = new ObservableCollection<Notification>();

        private Notification _selectedNotification;
        public Notification SelectedNotification
        {
            get => _selectedNotification;
            set
            {
                if (SetProperty(ref _selectedNotification, value) && value != null)
                {
                    MarkAsReadCommand.Execute(value);
                }
            }
        }

        public IAsyncRelayCommand RefreshCommand { get; }
        public IAsyncRelayCommand<Notification> MarkAsReadCommand { get; }

        public NotificationsViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "Notifications";
            
            RefreshCommand = new AsyncRelayCommand(LoadNotificationsAsync);
            MarkAsReadCommand = new AsyncRelayCommand<Notification>(MarkAsReadAsync);
        }

        public async Task LoadNotificationsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Notifications.Clear();
                var notifications = await _apiService.GetNotificationsAsync();
                foreach (var notification in notifications)
                {
                    Notifications.Add(notification);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Impossible de charger les notifications : {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task MarkAsReadAsync(Notification notification)
        {
            if (notification.IsRead == true)
                return;

            try
            {
                var result = await _apiService.MarkNotificationAsReadAsync(notification.Id);
                if (result)
                {
                    notification.IsRead = true;
                    OnPropertyChanged(nameof(Notifications));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Impossible de marquer la notification comme lue : {ex.Message}", "OK");
            }
        }
    }
}

