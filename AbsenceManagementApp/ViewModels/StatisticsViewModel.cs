using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AbsenceManagementApp.Models;
using AbsenceManagementApp.Services;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;

namespace AbsenceManagementApp.ViewModels
{
    public class StatisticsViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        private Statistics _statistics;
        public Statistics Statistics
        {
            get => _statistics;
            set => SetProperty(ref _statistics, value);
        }

        private List<Student> _students;
        public List<Student> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                if (SetProperty(ref _selectedStudent, value) && value != null)
                {
                    LoadStatisticsForStudentCommand.Execute(value.Id);
                }
            }
        }

        public IAsyncRelayCommand LoadStudentsCommand { get; }
        public IAsyncRelayCommand<int> LoadStatisticsForStudentCommand { get; }

        public StatisticsViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Title = "Statistiques";
            
            LoadStudentsCommand = new AsyncRelayCommand(LoadStudentsAsync);
            LoadStatisticsForStudentCommand = new AsyncRelayCommand<int>(LoadStatisticsForStudentAsync);
        }

        private async Task LoadStudentsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // Dans une application réelle, vous devriez avoir un endpoint pour récupérer les enfants du parent
                // Pour cet exemple, nous utilisons les données des absences pour extraire les étudiants
                var absences = await _apiService.GetAbsencesAsync();
                var studentIds = absences.Select(a => a.StudentId).Distinct().ToList();
                
                Students = studentIds.Select(id => new Student
                {
                    Id = id,
                    FirstName = absences.First(a => a.StudentId == id).StudentName.Split(' ')[0],
                    LastName = absences.First(a => a.StudentId == id).StudentName.Split(' ')[1]
                }).ToList();

                if (Students.Any())
                {
                    SelectedStudent = Students.First();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Impossible de charger les étudiants : {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadStatisticsForStudentAsync(int studentId)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Statistics = await _apiService.GetStudentStatisticsAsync(studentId);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Impossible de charger les statistiques : {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

