using EnglishApp.Entity;
using EnglishApp.Models.Teachers;
using EnglishApp.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApp.Services.Teacher
{
    public class TeacherService : GenericRepository<TeacherEntity, EnglishContext>, ITeacherService
    {
        public async Task<bool> CreateTeacher(TeacherInsertDto input)
        {
            var anyEmail = await englishContext.Teachers.AnyAsync(m => m.Email == input.Email);
            if (anyEmail) return false;
            var entity = new TeacherEntity()
            {
                Address = input.Address,
                Education = input.Education,
                Email = input.Email,
                GenderId = input.GenderId,
                Name = input.Name,
                Phone = input.Phone,
                CreatedAt = DateTime.Now
            };
            englishContext.Teachers.Add(entity);
            var flag = await englishContext.SaveChangesAsync();
            return flag > -1;
        }

        public async Task<bool> UpdateTeacher(TeacherInsertDto input, int id)
        {
            var anyEmail = await englishContext.Teachers.AnyAsync(m => m.Email == input.Email && m.Id != id);
            if (anyEmail) return false;
            var entity = await englishContext.Teachers.FirstOrDefaultAsync(x => x.Id == id);
            if(entity != null)
            {
                entity.Address = input.Address;
                entity.UpdatedAt = DateTime.Now;
                entity.Name = input.Name;
                entity.Phone = input.Phone;
                entity.GenderId = input.GenderId;
                entity.Email= input.Email;
                entity.Education = input.Education;
                return await englishContext.SaveChangesAsync() >= 0;
            }
            return false;
        }

        public async Task<TeacherDto> GetTeacherById(int Id)
        {
            var iQueryable = englishContext.Teachers.AsQueryable();
            iQueryable = iQueryable.Where(m => m.Id == Id);
            var results = await iQueryable.Select(m => new TeacherDto
            {
                Address = m.Address,
                Education = m.Education,
                Email = m.Email,
                GenderId = m.GenderId,
                Name = m.Name,
                Phone = m.Phone,
                Id = m.Id
            }).FirstOrDefaultAsync();
            return results;
        }

        public async Task<List<TeacherDto>> GetTeacherList(string search = "")
        {
            var iQueryable = englishContext.Teachers.Where(m => !m.DeletedAt.HasValue).AsQueryable();
            if(!string.IsNullOrWhiteSpace(search))
            {
                iQueryable = iQueryable.Where(m => EF.Functions.Like(m.Name.ToLower(), $"%{search.ToLower()}%") || EF.Functions.Like(m.Email.ToLower(), $"%{search.ToLower()}%"));
            }

            var results = await iQueryable.Select(m => new TeacherDto
            {
                Address = m.Address,
                Education = m.Education,
                Email = m.Email,
                GenderId = m.GenderId,
                Name = m.Name,
                Phone = m.Phone,
                Id = m.Id
            }).ToListAsync();
            return results;
        }

        public async Task<int> GetTotalTeacher()
        {
            var iQueryable = englishContext.Teachers.Where(m => !m.DeletedAt.HasValue).AsQueryable();
            return await iQueryable.CountAsync();
        }
        public async Task<bool> DeleteTeacher(int id)
        {
            var iQueryable = englishContext.Teachers.AsQueryable();
            iQueryable = iQueryable.Where(m => m.Id == id);
            var entity = await iQueryable.FirstOrDefaultAsync();
            if(entity != null)
            {
                entity.DeletedAt = DateTime.Now;
                return await englishContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }

    public interface ITeacherService 
    {
        Task<bool> CreateTeacher(TeacherInsertDto input);
        Task<List<TeacherDto>> GetTeacherList(string search = "");
        Task<TeacherDto> GetTeacherById(int Id);
        Task<int> GetTotalTeacher();
        Task<bool> DeleteTeacher(int id);
        Task<bool> UpdateTeacher(TeacherInsertDto input, int id);
    }
}
