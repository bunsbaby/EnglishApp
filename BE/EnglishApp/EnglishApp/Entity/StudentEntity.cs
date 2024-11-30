using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace EnglishApp.Entity
{
    [Table("Students")]
    public class StudentEntity
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
        public int? AccountId { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public int GenderId { get; set; }
        public string Address { get; set; }
        public int? ClassId { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
