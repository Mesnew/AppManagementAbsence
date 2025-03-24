namespace AbsenceManagementApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; } // "admin", "parent", "professeur", "eleve"
        public string Email { get; set; }
        public string Password { get; set; } // Stocké sous forme de hash
        public DateTime? CreatedAt { get; set; }
    }
}

