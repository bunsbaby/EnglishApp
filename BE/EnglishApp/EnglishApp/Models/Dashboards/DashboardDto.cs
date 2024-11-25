using System.Collections.Generic;

namespace EnglishApp.Models.Dashboards
{
    public class DashboardDto
    {
        public int TotalTeacher { get; set; }
        public int TotalStudent { get; set; }

        public List<DashboardChartDto> DashboardCharts { get; set; }
        public List<DashboardChartRevenueDto> BarCharts { get; set; }
    }
    public class DashboardChartDto
    {
        public string Month { get; set; }
        public int Value { get; set; }
    }

    public class DashboardChartRevenueDto
    {
        public string Month { get; set; }
        public decimal Fee { get; set; }
        public decimal Expense { get; set; }
    }
}
