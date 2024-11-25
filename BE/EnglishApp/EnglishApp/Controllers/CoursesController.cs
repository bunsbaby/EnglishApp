using EnglishApp.Models;
using EnglishApp.Models.Courses;
using EnglishApp.Models.Students;
using EnglishApp.Models.Teachers;
using EnglishApp.Services.Course;
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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CourseInsertDto input)
        {
            var response = new ResponseDto<CourseDto>();
            try
            {
                var result = await _courseService.CreateCourse(input);
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
        public async Task<IActionResult> Put([FromBody] CourseInsertDto input, int id)
        {
            var response = new ResponseDto<CourseDto>();
            try
            {
                var result = await _courseService.UpdateCourse(input, id);
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
            var response = new ResponseDto<CourseDto>();
            try
            {
                var result = await _courseService.GetListCourse(search);
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
            var response = new ResponseDto<CourseDto>();
            try
            {
                var result = await _courseService.GetCourseById(id);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Lỗi hệ thống - {ex.Message}!";
            }
            return new ObjectResult(response);
        }

        //[HttpGet("GetLessons")]
        //public async Task<IActionResult> GetLessons()
        //{
        //    var response = new ResponseDto<LessonDto>();
        //    try
        //    {
        //        var results = await _courseService.GetLessons();
        //        response.Data = results;
        //    }
        //    catch(Exception ex)
        //    {
        //        response.Status = false;
        //        response.Message = $"Lỗi hệ thống - {ex.Message}!";
        //    }
        //    return new ObjectResult(response);
        //}

        //[HttpPost("GetCalendars")]
        //public async Task<IActionResult> GetCalendars([FromBody] CalendarRequest request)
        //{
        //    var response = new ResponseDto<LessonDto>();
        //    try
        //    {
        //        var results = await _courseService.GetCalendars(request);
        //        response.Data = results;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = false;
        //        response.Message = $"Lỗi hệ thống - {ex.Message}!";
        //    }
        //    return new ObjectResult(response);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new ResponseDto<TeacherDto>();
            try
            {
                var result = await _courseService.DeleteCourse(id);
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
