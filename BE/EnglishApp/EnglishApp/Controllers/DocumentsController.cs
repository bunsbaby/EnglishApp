using EnglishApp.Models;
using EnglishApp.Models.Documents;
using EnglishApp.Models.Teachers;
using EnglishApp.Services.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EnglishApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IHostingEnvironment _hostEnv;
        private readonly IDocumentService _documentService;
        public DocumentsController(IHostingEnvironment hostEnv, IDocumentService documentService)
        {
            _hostEnv = hostEnv;
            _documentService = documentService;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            var fileDic = "Files";
            string FilePath = Path.Combine(fileDic);
            if (!Directory.Exists(FilePath))
                Directory.CreateDirectory(FilePath);

            var fileName = $"{Guid.NewGuid()}{System.IO.Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(FilePath, fileName);
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
            }
            return new ObjectResult(new UploadFileDto { FileName = fileName });
        }

        [HttpGet("Download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var document = await _documentService.GetDocumentById(id);
                if (document == null)
                    return NotFound();
                var fileDic = "Files";
                string FilePath = Path.Combine(fileDic);
                var filePath = Path.Combine(FilePath, document.FileName);
                byte[] data = System.IO.File.ReadAllBytes(filePath);
                return File(data, "application/octet-stream", document.FileName);
            }
            catch(Exception) { }
            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DocumentInsertDto input)
        {
            var response = new ResponseDto<DocumentDto>();
            try
            {
                var result = await _documentService.CreateDocument(input);
                response.Status = result;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Message = $"Lỗi hệ thống - {ex.Message}!";
            }
            return new ObjectResult(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(string search = "")
        {
            var response = new ResponseDto<DocumentDto>();
            try
            {
                var results = await _documentService.GetListDocument(search);
                response.Data = results;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Message = $"Lỗi hệ thống - {ex.Message}!";
            }
            return new ObjectResult(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ResponseDto<DocumentDto>();
            try
            {
                var result = await _documentService.GetDocumentById(id);
                response.Data = result;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Message = $"Lỗi hệ thống - {ex.Message}!";
            }
            return new ObjectResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new ResponseDto<TeacherDto>();
            try
            {
                var result = await _documentService.DeleteDocument(id);
                response.Status = result;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Lỗi hệ thống - {ex.Message}!";
            }
            return new ObjectResult(response);
        }
    }
}
