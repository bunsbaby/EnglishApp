using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace EnglishApp.Entity
{
    [Table("Documents")]
    public class DocumentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public string Name { get;set; }
        public int DocumentSize { get; set; }
        public string Description { get; set; }
        [Required]
        public string FileName { get; set; }
        public string DisplayName { get; set; }
        public int ClassId { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
