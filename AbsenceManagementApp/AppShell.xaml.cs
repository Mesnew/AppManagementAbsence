using AbsenceManagementApp.Views;
using Microsoft.Maui.Controls;

namespace AbsenceManagementApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            // Enregistrer les routes pour la navigation
            Routing.RegisterRoute("justifyAbsence", typeof(JustifyAbsencePage));
        }
        
        // Méthode pour gérer le bouton retour
        protected override bool OnBackButtonPressed()
        {
            // Si nous sommes sur la page d'accueil, empêcher le retour
            if (CurrentPage is ParentHomePage)
            {
                return true; // Empêche le retour
            }
            
            return base.OnBackButtonPressed();
        }
    }
}

