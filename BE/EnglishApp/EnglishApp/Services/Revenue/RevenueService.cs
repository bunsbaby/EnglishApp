using EnglishApp.Entity;
using EnglishApp.Models.Dashboards;
using EnglishApp.Models.Revenues;
using EnglishApp.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApp.Services.Revenue
{
    public class RevenueService : GenericRepository<RevenueEntity, EnglishContext>, IRevenueService
    {
        public async Task<bool> CreateRevenue(RevenueInsertDto input)
        {
            var entity = new RevenueEntity
            {
                BankAccount = input.BankAccount,
                FeeType = input.FeeType,
                Name = input.Name,
                PaymentDeadline = input.PaymentDeadline,
                Phone = input.Phone,
                Status = input.Status,
                Unit = input.Unit,
                Fee = input.Fee,
            };
            englishContext.Revenues.Add(entity);
            return await englishContext.SaveChangesAsync() >= 0;
        }

        public async Task<bool> UpdateRevenue(RevenueInsertDto input, int id)
        {
            var entity = await englishContext.Revenues.FirstOrDefaultAsync(m => m.Id == id);
            if(entity != null)
            {
                entity.BankAccount = input.BankAccount;
                entity.FeeType = input.FeeType;
                entity.Name = input.Name;
                entity.PaymentDeadline = input.PaymentDeadline;
                entity.Phone = input.Phone;
                entity.Status = input.Status;
                entity.Unit = input.Unit;
                entity.Fee = input.Fee;
                return await englishContext.SaveChangesAsync() >= 0;
            }
            return false;
        }

        public async Task<List<RevenueDto>> GetListRenvenues(string search = "")
        {
            var iQueryable = englishContext.Revenues.AsQueryable();
            iQueryable = iQueryable.Where(m => !m.DeletedAt.HasValue);
            if(!string.IsNullOrWhiteSpace(search))
            {
                iQueryable = iQueryable.Where(m => EF.Functions.Like(m.Name.ToLower(), $"%{search.ToLower()}%") || EF.Functions.Like(m.Phone, $"%{search}%"));
            }
            var results = await iQueryable.Select(m => new RevenueDto
            {
                BankAccount = m.BankAccount,
                FeeType = m.FeeType,
                Id = m.Id,
                Name = m.Name,
                PaymentDeadline = m.PaymentDeadline,
                Phone = m.Phone,
                Status = m.Status,
                Unit = m.Unit,
                Fee = m.Fee
            }).ToListAsync();
            return results;
        }

        public async Task<RevenueDto> GetRevenueById(int id)
        {
            var result = await englishContext.Revenues.Where(m => m.Id == id)
                .Select(m => new RevenueDto
                {
                    BankAccount = m.BankAccount,
                    FeeType = m.FeeType,
                    Id = m.Id,
                    Name = m.Name,
                    PaymentDeadline = m.PaymentDeadline,
                    Phone = m.Phone,
                    Status = m.Status,
                    Unit = m.Unit,
                    Fee = m.Fee
                }).FirstOrDefaultAsync();
            return result;
        }

        public List<DashboardChartRevenueDto> GetBarCharts()
        {
            var iQueryable = englishContext.Revenues.AsQueryable();
            iQueryable = iQueryable.Where(m => m.PaymentDeadline.Year == DateTime.Now.Year && !m.DeletedAt.HasValue);
            var data = iQueryable.AsEnumerable().GroupBy(m => m.PaymentDeadline.Month).Select(m => new DashboardChartRevenueDto
            {
                Month = m.Key.ToString(),
                Fee = m.Where(z => z.FeeType == Enums.FeeTypeEnum.Fee).Sum(z => z.Unit),
                Expense = m.Where(z => z.FeeType == Enums.FeeTypeEnum.Expense).Sum(z => z.Unit),
            }).ToList();
            if (data != null && data.Any())
            {
                data.ForEach(m =>
                {
                    m.Month = ConvertMonths(m.Month);
                });
            }
            return data;
        }
        private string ConvertMonths(string month)
        {
            string strMonth = "";
            switch (month)
            {
                case "1":
                    strMonth = "Jan";
                    break;
                case "2":
                    strMonth = "Feb";
                    break;
                case "3":
                    strMonth = "Mar";
                    break;
                case "4":
                    strMonth = "April";
                    break;
                case "5":
                    strMonth = "May";
                    break;
                case "6":
                    strMonth = "Jun";
                    break;
                case "7":
                    strMonth = "Jul";
                    break;
                case "8":
                    strMonth = "Aug";
                    break;
                case "9":
                    strMonth = "Sept";
                    break;
                case "10":
                    strMonth = "Oct";
                    break;
                case "11":
                    strMonth = "Nov";
                    break;
                case "12":
                    strMonth = "Dec";
                    break;
            }
            return strMonth;
        }
        public async Task<bool> DeleteRevenue(int id)
        {
            var entity = await englishContext.Revenues.FirstOrDefaultAsync(x => x.Id == id);
            if(entity != null)
            {
                entity.DeletedAt = DateTime.Now;
                return await englishContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }

    public interface IRevenueService
    {
        Task<bool> CreateRevenue(RevenueInsertDto input);
        Task<RevenueDto> GetRevenueById(int id);
        Task<List<RevenueDto>> GetListRenvenues(string search = "");
        List<DashboardChartRevenueDto> GetBarCharts();
        Task<bool> DeleteRevenue(int id);
        Task<bool> UpdateRevenue(RevenueInsertDto input, int id);
    }
}
