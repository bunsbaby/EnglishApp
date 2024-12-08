using EnglishApp.Entity;
using EnglishApp.Models.Documents;
using EnglishApp.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishApp.Services.Document
{
    public class DocumentService : GenericRepository<DocumentEntity, EnglishContext>, IDocumentService
    {
        public async Task<bool> CreateDocument(DocumentInsertDto input)
        {
            var entity = new DocumentEntity()
            {
                CreatedAt = DateTime.Now,
                Description = input.Description,
                DocumentSize = input.DocumentSize,
                FileName = input.FileName,
                Name = input.Name,
                DisplayName = input.DisplayName,
                ClassId = input.ClassId
            };
            englishContext.Documents.Add(entity);
            var flag = await englishContext.SaveChangesAsync();
            return flag > -1;
        }

        public async Task<DocumentDto> GetDocumentById(int Id)
        {
            var iQueryable = englishContext.Documents.Join(
                    englishContext.Classes, d => d.ClassId, cl => cl.Id, (d, cl) => new { d, cl }
                ).AsQueryable();
            iQueryable = iQueryable.Where(m => m.d.Id == Id);
            var data = await iQueryable.Select(m => new DocumentDto
            {
                Description = m.d.Description,
                DocumentSize = m.d.DocumentSize,
                FileName = m.d.FileName,
                Id = m.d.Id,
                Name = m.d.Name,
                DisplayName = m.d.DisplayName,
                ClassId = m.d.ClassId,
                ClassName = m.cl.Name,
                CreatedAt = m.d.CreatedAt
            }).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<DocumentDto>> GetListDocument(string search = "")
        {
            var iQueryable = englishContext.Documents
                .Join(
                    englishContext.Classes, d => d.ClassId, cl => cl.Id, (d, cl) => new { d, cl }
                ).Where(m => !m.d.DeletedAt.HasValue).AsQueryable();
            if(!string.IsNullOrWhiteSpace(search))
            {
                iQueryable = iQueryable.Where(m => EF.Functions.Like(m.d.Name.ToLower(), $"%{search.ToLower()}%"));
            }
            var data = await iQueryable.Select(m => new DocumentDto
            {
                Description = m.d.Description,
                DocumentSize = m.d.DocumentSize,
                FileName = m.d.FileName,
                Id = m.d.Id,
                ClassId = m.d.ClassId,
                ClassName = m.cl.Name,
                Name = m.d.Name,
                CreatedAt = m.d.CreatedAt
            }).ToListAsync();
            return data;
        }
        public async Task<bool> DeleteDocument(int id)
        {
            var entity = await englishContext.Documents.FirstOrDefaultAsync(m => m.Id == id);
            if(entity != null)
            {
                entity.DeletedAt = DateTime.Now;
                return await englishContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }

    public interface IDocumentService
    {
        Task<bool> CreateDocument(DocumentInsertDto input);
        Task<List<DocumentDto>> GetListDocument(string search = "");
        Task<DocumentDto> GetDocumentById(int Id);
        Task<bool> DeleteDocument(int id);
    }
}
