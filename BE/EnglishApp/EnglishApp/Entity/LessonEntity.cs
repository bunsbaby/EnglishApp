using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishApp.Entity
{
    [Table("Lessons")]
    public class LessonEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public int From { get; set; }
        public int To { get; set; }

        public virtual ICollection<ClassEntity> Classes { get; set; }
    }
}
