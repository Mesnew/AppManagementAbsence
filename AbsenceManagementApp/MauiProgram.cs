using AbsenceManagementApp.Converters;
using AbsenceManagementApp.Services;
using AbsenceManagementApp.Views;
using AbsenceManagementApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using CommunityToolkit.Maui;

namespace AbsenceManagementApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Configuration de l'URL de base de l'API
            string baseApiUrl = "http://192.168.50.188:5000"; // À remplacer par l'URL réelle de votre API
            
            // Enregistrement des services
            builder.Services.AddSingleton<IApiService>(provider => new ApiService(baseApiUrl));
            builder.Services.AddSingleton<AuthService>();
            
            // Enregistrement des ViewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<ParentHomeViewModel>();
            builder.Services.AddTransient<AbsencesViewModel>();
            builder.Services.AddTransient<NotificationsViewModel>();
            builder.Services.AddTransient<StatisticsViewModel>();
            builder.Services.AddTransient<JustifyAbsenceViewModel>();
            
            // Enregistrement des pages
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<ParentHomePage>();
            builder.Services.AddTransient<AbsencesPage>();
            builder.Services.AddTransient<NotificationsPage>();
            builder.Services.AddTransient<StatisticsPage>();
            builder.Services.AddTransient<JustifyAbsencePage>();
            
            // Enregistrement des convertisseurs
            builder.Services.AddSingleton<StatusToColorConverter>();
            builder.Services.AddSingleton<BoolToColorConverter>();
            builder.Services.AddSingleton<BoolToReadStatusConverter>();
            builder.Services.AddSingleton<MonthNumberToNameConverter>();
            builder.Services.AddSingleton<NotNullConverter>();

            return builder.Build();
        }
    }
}

