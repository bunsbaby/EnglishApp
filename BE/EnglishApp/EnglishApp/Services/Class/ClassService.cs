using EnglishApp.Entity;
using EnglishApp.Models;
using EnglishApp.Models.Classes;
using EnglishApp.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            var studentCount = englishContext.Students.Where(
                    m => m.ClassId == id
                ).Count();
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
                LessonId = m.l.Id,
                StudentCount = studentCount
            }).FirstOrDefaultAsync();

            return data;
        }

        public async Task<List<ClassDto>> GetListClass(string search = "")
        {
            var class_s = englishContext.Classes.Join(
                    englishContext.Students, cl
                        => cl.Id, s => s.ClassId, (cl, s)
                            => new { cl, s }).GroupBy(n => n.cl.Id).Select(m => new
                            {
                                StudentCount = m.Count(),
                                Id = m.Key
                            }
            ).OrderBy(x => x.Id);
            var iQueryable = englishContext.Classes.Join
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
                //).Join(
                //    englishContext.Students, cl_s
                //        => cl_s.cl.Id, s => s.ClassId, (cl_s, s)
                //            => new { cl = cl_s.cl, t = cl_s.t, l = cl_s.l, c = cl_s.c, s }
                //).Join(
                //    class_s, i
                //    => i.cl.Id, s => s.Id, (i, s)
                //    => new { cl = i.cl, t = i.t, l = i.l, c = i.c, s }
                );
            if (!string.IsNullOrWhiteSpace(search))
            {
                iQueryable = iQueryable.Where(m => EF.Functions.Like(m.cl.Name.ToLower(), $"%{search.ToLower()}%"));
            }
            //iQueryable = (
            //                from query in iQueryable
            //                join student in englishContext.Students
            //                    on query.cl.Id equals student.Id into queryJoined
            //             )
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
                LessonId = m.l.Id,
                StudentCount = 0
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

        public async Task<List<CalendarDto>> GetCalendars(CalendarRequest request)
        {
            var iQueryable = englishContext.Classes.Join(
                                englishContext.Courses, cl => cl.CourseId, c => c.Id, (cl, c) => new {cl, c}
                            ).Where(m => !m.cl.DeletedAt.HasValue).AsQueryable();
            int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            if (request.ChooseDate.HasValue)
            {
                days = DateTime.DaysInMonth(request.ChooseDate.Value.Year, request.ChooseDate.Value.Month);
                firstDayOfMonth = new DateTime(request.ChooseDate.Value.Year, request.ChooseDate.Value.Month, 1);
                iQueryable = iQueryable.Where(m => m.c.StartDated.AddMonths((int)m.c.PackageType).Date >= request.ChooseDate.Value.Date);
            }
            else
            {
                iQueryable = iQueryable.Where(m => m.c.StartDated.AddMonths((int)m.c.PackageType).Date >= DateTime.Now.Date);
            }
            var qclass = await iQueryable.Select(m => new CalendarDto
                {
                    StartDated = m.c.StartDated.Date,
                    Content = $"{m.cl.Name}({m.cl.Lesson.Time})",
                    Id = m.cl.Id,
                    LessonId = m.cl.LessonId,
                    From = m.cl.Lesson.From,
                    To = m.cl.Lesson.To,
                    PackageType = (int) m.c.PackageType
                }).ToListAsync();
            var results = new List<CalendarDto>();
            if(qclass != null && qclass.Any())
            {
                foreach(var iclass in qclass)
                {
                    for (var i = 0; i<days; i++)
                    {
                        if(firstDayOfMonth.AddDays(i).Date >= iclass.StartDated && firstDayOfMonth.AddDays(i).Date <= iclass.StartDated.AddMonths(iclass.PackageType))
                        {
                            var dayOfWeek = ((int)firstDayOfMonth.AddDays(i).DayOfWeek == 0) ? 7 : (int)firstDayOfMonth.AddDays(i).DayOfWeek;
                            /*** Case T2-T7 */
                            if (iclass.To == 7 && dayOfWeek <= 6)
                            {
                                results.Add(new CalendarDto
                                {
                                    Day = (i + 1),
                                    Content = iclass.Content,
                                    Id = iclass.Id,
                                    Month = request.ChooseDate.HasValue? request.ChooseDate.Value.Month : DateTime.Now.Month,
                                    Year = request.ChooseDate.HasValue ? request.ChooseDate.Value.Year : DateTime.Now.Year
                                });
                            }
                            else if (iclass.To == 8 && dayOfWeek <= 8)
                            {
                                results.Add(new CalendarDto
                                {
                                    Day = (i + 1),
                                    Content = iclass.Content,
                                    Id = iclass.Id,
                                    Month = request.ChooseDate.HasValue ? request.ChooseDate.Value.Month : DateTime.Now.Month,
                                    Year = request.ChooseDate.HasValue ? request.ChooseDate.Value.Year : DateTime.Now.Year
                                });
                            }
                        }

                    }
                }
            }
            return results;
        }
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
        Task<List<CalendarDto>> GetCalendars(CalendarRequest request = null);
        Task<bool> DeleteClass(int id);
        Task<bool> UpdateClass(ClassInsertDto input, int id);
    }
}
