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
                CourseId = input.CourseId,
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
                entity.CourseId = input.CourseId;
                entity.TeacherId = input.TeacherId;
                entity.LessonId = input.LessonId;
                entity.UpdatedAt = DateTime.Now;
                return await englishContext.SaveChangesAsync() >= 0;
            }
            return false;
        }

        public async Task<ClassDto> GetClassById(int id)
        {
            var iQueryable = englishContext.Classes.Join
                (
                    englishContext.Teachers, cl
                        => cl.TeacherId, t => t.Id, (cl, t)
                            => new { cl, t }
                )
                .Join
                (
                    englishContext.Lessons, cl_t
                        => cl_t.cl.LessonId, l => l.Id, (cl_t, l)
                            => new { cl = cl_t.cl, t = cl_t.t, l }
                ).Join
                (
                    englishContext.Courses, cl_t_l
                        => cl_t_l.cl.CourseId, c => c.Id, (cl_t_l, c)
                            => new { cl = cl_t_l.cl, t = cl_t_l.t, cl_t_l.l, c }
                 );
            iQueryable = iQueryable.Where(m => m.cl.Id == id);
            var data = await iQueryable.Select(m => new ClassDto
            {
                Id = m.cl.Id,
                Name = m.cl.Name,
                Description = m.cl.Description,
                CourseId = m.c.Id,
                CourseName = m.c.Name,
                TeacherId = m.t.Id,
                TeacherName = m.t.Name,
                LessonName = m.l.Name,
                LessonId = m.l.Id
            }).FirstOrDefaultAsync();

            return data;
        }

        public async Task<List<ClassDto>> GetListClass(string search = "")
        {
            var iQueryable =  englishContext.Classes.Join
                (
                    englishContext.Teachers, cl 
                        => cl.TeacherId, t => t.Id, (cl, t) 
                            => new { cl, t}
                )
                .Join
                (
                    englishContext.Lessons, cl_t 
                        => cl_t.cl.LessonId, l => l.Id, (cl_t, l) 
                            => new { cl = cl_t.cl, t = cl_t.t, l }
                ).Join
                (
                    englishContext.Courses, cl_t_l
                        => cl_t_l.cl.CourseId, c => c.Id, (cl_t_l, c)
                            => new { cl = cl_t_l.cl, t = cl_t_l.t, cl_t_l.l, c }
                 );
            if(!string.IsNullOrWhiteSpace(search))
            {
                iQueryable = iQueryable.Where(m => EF.Functions.Like(m.c.Name.ToLower(), $"%{search.ToLower()}%"));
            }
            iQueryable = iQueryable.Where(m => !m.cl.DeletedAt.HasValue);
            var data = await iQueryable.Select(m => new ClassDto
            {
                Id = m.cl.Id,
                Name = m.cl.Name,
                Description = m.cl.Description,
                CourseId = m.c.Id,
                CourseName = m.c.Name,
                TeacherId = m.t.Id,
                TeacherName = m.t.Name,
                LessonName = m.l.Name,
                LessonId = m.l.Id
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
