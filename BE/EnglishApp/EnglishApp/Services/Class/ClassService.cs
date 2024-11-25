using EnglishApp.Entity;
using EnglishApp.Models;
using EnglishApp.Models.Classes;
using EnglishApp.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApp.Services.Class
{
    public class ClassService : GenericRepository<ClassEntity, EnglishContext>, IClassService
    {
        public async Task<bool> CreateClass(ClassInsertDto input)
        {
            var entity = new ClassEntity()
            {
                CreatedAt = DateTime.Now,
                Description = input.Description,
                Name = input.Name,
                TeacherId = input.TeacherId,
                LessonId = input.LessonId,
            };
            englishContext.Classes.Add(entity);
            return await englishContext.SaveChangesAsync() > -1;
        }
        public async Task<bool> UpdateClass(ClassInsertDto input, int id)
        {
            var entity = await englishContext.Classes.FirstOrDefaultAsync(c => c.Id == id);
            if(entity != null)
            {
                entity.Description = input.Description;
                entity.Name = input.Name;
                entity.TeacherId = input.TeacherId;
                entity.LessonId = input.LessonId;
                entity.UpdatedAt = DateTime.Now;
                return await englishContext.SaveChangesAsync() >= 0;
            }
            return false;
        }

        public async Task<ClassDto> GetClassById(int id)
        {
            var iQueryable = englishContext.Classes.Join(englishContext.Teachers, c => c.TeacherId, t => t.Id, (c, t) => new { c, t })
                .Join(englishContext.Lessons, c2 => c2.c.LessonId, l => l.Id, (c2, l) => new { c = c2.c, t = c2.t, l });
            iQueryable = iQueryable.Where(m => m.c.Id == id);
            var data = await iQueryable.Select(m => new ClassDto
            {
                Id = m.c.Id,
                Name = m.c.Name,
                TeacherName = m.t.Name,
                Description = m.c.Description,
                LessonId = m.c.LessonId,
                LessonName = m.l.Name,
                TeacherId = m.c.TeacherId,
            }).FirstOrDefaultAsync();

            return data;
        }

        public async Task<List<ClassDto>> GetListClass(string search = "")
        {
            var iQueryable =  englishContext.Classes.Join(englishContext.Teachers, c => c.TeacherId, t => t.Id, (c, t) => new { c, t })
                .Join(englishContext.Lessons, c2 => c2.c.LessonId, l => l.Id, (c2, l) => new { c = c2.c, t = c2.t, l });
            if(!string.IsNullOrWhiteSpace(search))
            {
                iQueryable = iQueryable.Where(m => EF.Functions.Like(m.c.Name.ToLower(), $"%{search.ToLower()}%"));
            }
            iQueryable = iQueryable.Where(m => !m.c.DeletedAt.HasValue);
            var data = await iQueryable.Select(m => new ClassDto
            {
                Id = m.c.Id,
                Name = m.c.Name,
                TeacherName = m.t.Name,
                LessonName = m.l.Name
            }).ToListAsync();

            return data;
        }

        public async Task<List<LessonDto>> GetLessons()
        {
            var iQueryable = englishContext.Lessons;
            var results = await iQueryable.Select(m => new LessonDto
            {
                Id = m.Id,
                Name = m.Name
            }).ToListAsync();
            return results;
        }

        //public async Task<List<CalendarDto>> GetCalendars(CalendarRequest request)
        //{
        //    var iQueryable = englishContext.Classes.Where(m => !m.DeletedAt.HasValue).AsQueryable();
        //    int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        //    var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        //    if (request.ChooseDate.HasValue)
        //    {
        //        days = DateTime.DaysInMonth(request.ChooseDate.Value.Year, request.ChooseDate.Value.Month);
        //        firstDayOfMonth = new DateTime(request.ChooseDate.Value.Year, request.ChooseDate.Value.Month, 1);
        //        iQueryable = iQueryable.Where(m => m.StartDated.AddMonths((int)m.PackageType).Date >= request.ChooseDate.Value.Date);
        //    }
        //    else
        //    {
        //        iQueryable = iQueryable.Where(m => m.StartDated.AddMonths((int)m.PackageType).Date >= DateTime.Now.Date);
        //    }
        //    var class = await iQueryable.Select(m => new CalendarDto
        //        {
        //            StartDated = m.StartDated.Date,
        //            Content = $"{m.ClassName}({m.Lesson.Time})",
        //            Id = m.Id,
        //            LessonId = m.LessonId,
        //            From = m.Lesson.From,
        //            To = m.Lesson.To,
        //            PackageType = (int) m.PackageType
        //        }).ToListAsync();
        //    var results = new List<CalendarDto>();
        //        if(class != null && class.Any())
        //        {
        //            foreach(var class in class)
        //            {
        //                for (var i = 0; i<days; i++)
        //                {
        //                    if(firstDayOfMonth.AddDays(i).Date >= class.StartDated && firstDayOfMonth.AddDays(i).Date <= class.StartDated.AddMonths(class.PackageType))
        //                    {
        //                        var dayOfWeek = ((int)firstDayOfMonth.AddDays(i).DayOfWeek == 0) ? 7 : (int)firstDayOfMonth.AddDays(i).DayOfWeek;
        //                        /*** Case T2-T7 */
        //                        if (class.To == 7 && dayOfWeek <= 6)
        //                        {
        //                            results.Add(new CalendarDto
        //                            {
        //                                Day = (i + 1),
        //                                Content = class.Content,
        //                                Id = class.Id,
        //                                Month = request.ChooseDate.HasValue? request.ChooseDate.Value.Month : DateTime.Now.Month,
        //                                Year = request.ChooseDate.HasValue ? request.ChooseDate.Value.Year : DateTime.Now.Year
        //                            });
        //                        }
        //                        else if (class.To == 8 && dayOfWeek <= 8)
        //                        {
        //                            results.Add(new CalendarDto
        //                            {
        //                                Day = (i + 1),
        //                                Content = class.Content,
        //                                Id = class.Id,
        //                                Month = request.ChooseDate.HasValue ? request.ChooseDate.Value.Month : DateTime.Now.Month,
        //                                Year = request.ChooseDate.HasValue ? request.ChooseDate.Value.Year : DateTime.Now.Year
        //                            });
        //                        }
        //                    }

        //                }
        //            }
        //        }
        //        return results;
        //    }
        public async Task<bool> DeleteClass(int id)
        {
            var entity = await englishContext.Classes.FirstOrDefaultAsync(c => c.Id == id);
            if(entity != null)
            {
                entity.DeletedAt = DateTime.Now;
                return await englishContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }

    public interface IClassService
    {
        Task<bool> CreateClass(ClassInsertDto input);
        Task<List<ClassDto>> GetListClass(string search = "");
        Task<ClassDto> GetClassById(int id);
        Task<List<LessonDto>> GetLessons();
        //Task<List<CalendarDto>> GetCalendars(CalendarRequest request = null);
        Task<bool> DeleteClass(int id);
        Task<bool> UpdateClass(ClassInsertDto input, int id);
    }
}
