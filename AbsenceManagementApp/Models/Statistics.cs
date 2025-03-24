using System.Collections.Generic;

namespace AbsenceManagementApp.Models
{
    public class Statistics
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int TotalAbsences { get; set; }
        public int JustifiedAbsences { get; set; }
        public int UnjustifiedAbsences { get; set; }
        public Dictionary<int, int> AbsencesByMonth { get; set; }
    }
}

