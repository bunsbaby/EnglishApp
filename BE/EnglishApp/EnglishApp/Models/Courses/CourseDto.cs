using EnglishApp.Enums;
using System;

namespace EnglishApp.Models.Courses
{
    public class CourseDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public PackageCourseType PackageType { get; set; }
        public DateTime StartDated { get; set; }
    }
}
