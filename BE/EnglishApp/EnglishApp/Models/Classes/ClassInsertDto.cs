using EnglishApp.Enums;
using System;
using System.Collections.Generic;

namespace EnglishApp.Models.Classes
{
    public class ClassInsertDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TeacherId { get; set; }
        public int LessonId { get; set; }
    }
}
