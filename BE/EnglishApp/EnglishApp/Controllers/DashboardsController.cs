using EnglishApp.Models;
using EnglishApp.Models.Dashboards;
using EnglishApp.Services.Revenue;
using EnglishApp.Services.Student;
using EnglishApp.Services.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EnglishApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IRevenueService _revenueService;

        public DashboardsController(IStudentService studentService, ITeacherService teacherService, IRevenueService revenueService)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _revenueService = revenueService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new ResponseDto<DashboardDto>();
            try
            {
                var data = new DashboardDto();
                data.TotalTeacher = await _teacherService.GetTotalTeacher();
                data.TotalStudent = await _studentService.GetTotalStudent();
                data.DashboardCharts = _studentService.GetDashboardCharts();
                data.BarCharts = _revenueService.GetBarCharts();
                response.Data = data;
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
