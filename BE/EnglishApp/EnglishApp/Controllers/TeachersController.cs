using EnglishApp.Models.Accounts;
using EnglishApp.Models;
using EnglishApp.Models.Teachers;
using EnglishApp.Services.Teacher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using EnglishApp.Models.Courses;

namespace EnglishApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TeacherInsertDto input)
        {
            var response = new ResponseDto<TeacherDto>();
            try
            {
                var result = await _teacherService.CreateTeacher(input);
                switch (result)
                {
                    case 0:
                        response.Status = false;
                        response.Message = "Tạo giảng viên thất bại";
                        break;
                    case 1:
                        response.Status = true;
                        response.Message = "Tạo giảng viên thành công";
                        break;
                    case 2:
                        response.Status = false;
                        response.Message = "Email đã được sử dụng";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Lỗi hệ thống - {ex.Message}!";
            }
            return new ObjectResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] TeacherInsertDto input, int id)
        {
            var response = new ResponseDto<TeacherDto>();
            try
            {
                var result = await _teacherService.UpdateTeacher(input, id);
                switch (result)
                {
                    case 0:
                        response.Status = false;
                        response.Message = "Cập nhật thất bại";
                        break;
                    case 1:
                        response.Status = true;
                        response.Message = "Cập nhật thành công";
                        break;
                    case 2:
                        response.Status = false;
                        response.Message = "Email đã được sử dụng bởi giảng viên khác";
                        break;
                }
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
            var response = new ResponseDto<TeacherDto>();
            try
            {
                var result = await _teacherService.GetTeacherList(search);
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
            var response = new ResponseDto<TeacherDto>();
            try
            {
                var result = await _teacherService.GetTeacherById(id);
                response.Data = result;
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
                var result = await _teacherService.DeleteTeacher(id);
                response.Status = result;
                response.Message = "Xóa giảng viên thành công";
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Message = $"Lỗi hệ thống - {ex.Message}!";
            }
            return new ObjectResult(response);
        }
    }
}
