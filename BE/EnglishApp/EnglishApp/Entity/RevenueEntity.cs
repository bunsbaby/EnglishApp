using EnglishApp.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishApp.Entity
{
    [Table("Revenues")]
    public class RevenueEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Fee { get; set; }
        [Required]
        public decimal Unit { get; set; }
        [Required]
        public RevenueStatus Status { get; set; }
        [Required]
        public string BankAccount { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime PaymentDeadline { get; set; }
        [Required]
        public FeeTypeEnum FeeType { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
