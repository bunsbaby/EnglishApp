using System;

namespace EnglishApp.Models
{
    public class CalendarDto
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Content { get; set; }
        public int Id { get; set; }
        public int PackageType { get; set; }
        public DateTime StartDated { get; set; }
    }

    public class CalendarRequest
    {
        public DateTime? ChooseDate { get; set; }
    }
}
