using System.Collections.Generic;
using System.Threading.Tasks;
using AbsenceManagementApp.DTOs;
using AbsenceManagementApp.Models;

namespace AbsenceManagementApp.Services
{
    public interface IApiService
    {
        Task<AuthResponseDTO> LoginAsync(string username, string password);
        Task<List<Absence>> GetAbsencesAsync();
        Task<bool> JustifyAbsenceAsync(int absenceId, string reason, string document);
        Task<List<Notification>> GetNotificationsAsync();
        Task<Statistics> GetStudentStatisticsAsync(int studentId);
        Task<bool> MarkNotificationAsReadAsync(int notificationId);
    }
}

