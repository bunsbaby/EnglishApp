using EnglishApp.Enums;
using System;

namespace EnglishApp.Models.Classes
{
    public class ClassDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string LessonName { get; set; }
        public int LessonId { get; set; }
    }
}
