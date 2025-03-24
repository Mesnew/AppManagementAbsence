using System;

namespace AbsenceManagementApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly? Birthdate { get; set; }
        
        // Propriété calculée pour l'affichage
        public string FullName => $"{FirstName} {LastName}";
    }
}

