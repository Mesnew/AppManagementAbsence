using System;

namespace AbsenceManagementApp.Models
{
    public class Absence
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateOnly AbsenceDate { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; } // "en attente", "justifiée", "non justifiée"
        public string? Document { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Propriétés de navigation
        public string StudentName { get; set; }
    }
}

