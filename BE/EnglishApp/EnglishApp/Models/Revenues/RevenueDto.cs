using EnglishApp.Enums;
using System;

namespace EnglishApp.Models.Revenues
{
    public class RevenueDto
    {
        public string Name { get; set; }
        public string Fee { get; set; }
        public decimal Unit { get; set; }
        public RevenueStatus Status { get; set; }
        public string BankAccount { get; set; }
        public string Phone { get; set; }
        public DateTime PaymentDeadline { get; set; }
        public int Id { get; set; }
        public FeeTypeEnum FeeType { get; set; }
    }
}
