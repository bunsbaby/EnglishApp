using System.ComponentModel;

namespace EnglishApp.Enums
{
    public enum RevenueStatus
    {
        [Description("Đã trả")]
        Paid,
        [Description("Chưa trả")]
        Unpaid
    }
}
