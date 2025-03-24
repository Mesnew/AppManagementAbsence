using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AbsenceManagementApp.DTOs;
using AbsenceManagementApp.Models;

namespace AbsenceManagementApp.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private string _token;

        public ApiService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = TimeSpan.FromSeconds(30) // Augmenter le timeout
            };
        }

        public async Task<AuthResponseDTO> LoginAsync(string username, string password)
        {
            try
            {
                var request = new AuthRequestDTO
                {
                    Username = username,
                    Password = password
                };

                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Pour le débogage, utilisez une réponse simulée
                #if DEBUG
                // Simulation de réponse pour le débogage
                if (username == "parent" && password == "password")
                {
                    var mockResponse = new AuthResponseDTO
                    {
                        Token = "mock_token_for_testing",
                        Username = username,
                        Role = "parent"
                    };
                    
                    // Stocke le token pour les futures requêtes
                    _token = mockResponse.Token;
                    _httpClient.DefaultRequestHeaders.Authorization = 
                        new AuthenticationHeaderValue("Bearer", _token);
                        
                    return mockResponse;
                }
                #endif

                var response = await _httpClient.PostAsync("/api/auth/login", content);
                
                // Vérifier si la requête a réussi
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Échec de l'authentification: {response.StatusCode}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var authResponse = JsonSerializer.Deserialize<AuthResponseDTO>(responseContent, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Stocke le token pour les futures requêtes
                _token = authResponse.Token;
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", _token);

                return authResponse;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la connexion: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Absence>> GetAbsencesAsync()
        {
            try
            {
                EnsureAuthenticated();

                #if DEBUG
                // Données de test pour le débogage
                if (_token == "mock_token_for_testing")
                {
                    return new List<Absence>
                    {
                        new Absence 
                        { 
                            Id = 1, 
                            StudentId = 1, 
                            StudentName = "Jean Dupont", 
                            AbsenceDate = new DateOnly(2023, 9, 15), 
                            Status = "en attente", 
                            Reason = "Non spécifié" 
                        },
                        new Absence 
                        { 
                            Id = 2, 
                            StudentId = 1, 
                            StudentName = "Jean Dupont", 
                            AbsenceDate = new DateOnly(2023, 9, 16), 
                            Status = "justifiée", 
                            Reason = "Maladie" 
                        }
                    };
                }
                #endif

                var response = await _httpClient.GetAsync("/api/parents/absences");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Absence>>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la récupération des absences: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> JustifyAbsenceAsync(int absenceId, string reason, string document)
        {
            try
            {
                EnsureAuthenticated();

                #if DEBUG
                // Simulation pour le débogage
                if (_token == "mock_token_for_testing")
                {
                    return true;
                }
                #endif

                var request = new JustifyAbsenceDTO
                {
                    Reason = reason,
                    Document = document
                };

                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"/api/parents/absences/{absenceId}/justify", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la justification de l'absence: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Notification>> GetNotificationsAsync()
        {
            try
            {
                EnsureAuthenticated();

                #if DEBUG
                // Données de test pour le débogage
                if (_token == "mock_token_for_testing")
                {
                    return new List<Notification>
                    {
                        new Notification 
                        { 
                            Id = 1, 
                            UserId = 1, 
                            Message = "Votre enfant Jean a été absent le 15/09/2023", 
                            IsRead = false, 
                            CreatedAt = DateTime.Now.AddDays(-2) 
                        },
                        new Notification 
                        { 
                            Id = 2, 
                            UserId = 1, 
                            Message = "Réunion parents-professeurs le 20/09/2023", 
                            IsRead = true, 
                            CreatedAt = DateTime.Now.AddDays(-5) 
                        }
                    };
                }
                #endif

                var response = await _httpClient.GetAsync("/api/parents/notifications");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Notification>>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la récupération des notifications: {ex.Message}");
                throw;
            }
        }

        public async Task<Statistics> GetStudentStatisticsAsync(int studentId)
        {
            try
            {
                EnsureAuthenticated();

                #if DEBUG
                // Données de test pour le débogage
                if (_token == "mock_token_for_testing")
                {
                    return new Statistics
                    {
                        StudentId = studentId,
                        StudentName = "Jean Dupont",
                        TotalAbsences = 5,
                        JustifiedAbsences = 3,
                        UnjustifiedAbsences = 2,
                        AbsencesByMonth = new Dictionary<int, int>
                        {
                            { 9, 3 },
                            { 10, 2 }
                        }
                    };
                }
                #endif

                var response = await _httpClient.GetAsync($"/api/parents/stats/{studentId}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Statistics>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la récupération des statistiques: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> MarkNotificationAsReadAsync(int notificationId)
        {
            try
            {
                EnsureAuthenticated();

                #if DEBUG
                // Simulation pour le débogage
                if (_token == "mock_token_for_testing")
                {
                    return true;
                }
                #endif

                var response = await _httpClient.PutAsync($"/api/parents/notifications/{notificationId}/read", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors du marquage de la notification comme lue: {ex.Message}");
                throw;
            }
        }

        private void EnsureAuthenticated()
        {
            if (string.IsNullOrEmpty(_token))
            {
                throw new InvalidOperationException("Vous devez vous connecter avant d'utiliser cette fonctionnalité.");
            }
        }
    }
}

