using EnglishApp.Models;
using EnglishApp.Models.Classes;
using EnglishApp.Models.Students;
using EnglishApp.Models.Teachers;
using EnglishApp.Services.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EnglishApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;
        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClassInsertDto input)
        {
            var response = new ResponseDto<ClassDto>();
            try
            {
                var result = await _classService.CreateClass(input);
                response.Status = result;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Lỗi hệ thống - {ex.Message}!";
            }
            return new ObjectResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] ClassInsertDto input, int id)
        {
            var response = new ResponseDto<ClassDto>();
            try
            {
                var result = await _classService.UpdateClass(input, id);
                response.Status = result;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Lỗi hệ thống - {ex.Message}!";
            }
            return new ObjectResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string search = "")
        {
            var response = new ResponseDto<ClassDto>();
            try
            {
                var result = await _classService.GetListClass(search);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Lỗi hệ thống - {ex.Message}!";
            }
            return new ObjectResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = new ResponseDto<ClassDto>();
            try
            {
                var result = await _classService.GetClassById(id);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Lỗi hệ thống - {ex.Message}!";
            }
            return new ObjectResult(response);
        }

        [HttpGet("GetLessons")]
        public async Task<IActionResult> GetLessons()
        {
            var response = new ResponseDto<LessonDto>();
            try
            {
                var results = await _classService.GetLessons();
                response.Data = results;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Message = $"Lỗi hệ thống - {ex.Message}!";
            }
            return new ObjectResult(response);
        }

        [HttpPost("GetCalendars")]
        public async Task<IActionResult> GetCalendars([FromBody] CalendarRequest request)
        {
            var response = new ResponseDto<LessonDto>();
            try
            {
                var results = await _classService.GetCalendars(request);
                response.Data = results;
            }
            catch (Exception ex)
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
                var result = await _classService.DeleteClass(id);
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
