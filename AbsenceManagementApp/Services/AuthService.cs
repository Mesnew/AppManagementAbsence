using System;
using System.Threading.Tasks;
using AbsenceManagementApp.DTOs;
using Microsoft.Maui.Storage;

namespace AbsenceManagementApp.Services
{
    public class AuthService
    {
        private readonly IApiService _apiService;
        private const string TokenKey = "auth_token";
        private const string UsernameKey = "username";
        private const string RoleKey = "role";

        public AuthService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<(bool Success, string Role)> LoginAsync(string username, string password)
        {
            try
            {
                var response = await _apiService.LoginAsync(username, password);
                
                // Stocke les informations d'authentification
                await SecureStorage.SetAsync(TokenKey, response.Token);
                await SecureStorage.SetAsync(UsernameKey, response.Username);
                await SecureStorage.SetAsync(RoleKey, response.Role);
                
                return (true, response.Role);
            }
            catch (Exception)
            {
                return (false, string.Empty);
            }
        }

        public async Task LogoutAsync()
        {
            await ClearAuthDataAsync();
        }

        public async Task ClearAuthDataAsync()
        {
            try
            {
                SecureStorage.Remove(TokenKey);
                SecureStorage.Remove(UsernameKey);
                SecureStorage.Remove(RoleKey);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de la suppression des données d'authentification: {ex.Message}");
            }
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await SecureStorage.GetAsync(TokenKey);
            return !string.IsNullOrEmpty(token);
        }

        public async Task<string> GetUsernameAsync()
        {
            return await SecureStorage.GetAsync(UsernameKey) ?? string.Empty;
        }

        public async Task<string> GetRoleAsync()
        {
            return await SecureStorage.GetAsync(RoleKey) ?? string.Empty;
        }
    }
}

