using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using EnglishApp.Enums;

namespace EnglishApp.Entity
{
    [Table("Classes")]
    public class ClassEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [ForeignKey("LessonId")]
        public int LessonId { get; set; }

        public virtual LessonEntity Lesson { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
