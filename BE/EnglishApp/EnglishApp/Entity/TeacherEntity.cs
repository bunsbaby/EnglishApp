using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace EnglishApp.Entity
{
    [Table("Teachers")]
    public class TeacherEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public int GenderId { get; set; }
        public string Address { get; set; }
        public string Education { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
