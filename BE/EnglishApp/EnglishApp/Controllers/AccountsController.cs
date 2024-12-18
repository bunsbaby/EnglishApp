using EnglishApp.Models;
using EnglishApp.Models.Accounts;
using EnglishApp.Services.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EnglishApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService) 
        { 
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterDto input)
        {
            var response = new ResponseDto<AccountDto>();
            try
            {
                var result = await _accountService.RegisterAccount(input);
                switch (result)
                {
                    case 0:
                        response.Status = false;
                        response.Message = "Đăng ký thất bại";
                        break;
                    case 1:
                        response.Status = true;
                        response.Message = "Đăng ký thành công";
                        break;
                    case 2:
                        response.Status = false;
                        response.Message = "Email đã được sử dụng";
                        break;
                    case 3:
                        response.Status = false;
                        response.Message = "Email không được đăng ký với tư cách này";
                        break;
                    case 4:
                        response.Status = false;
                        response.Message = "Tên đăng nhập đã được sử dụng";
                        break;
                }
            }
            catch(Exception ex) 
            {
                response.Status = false;
                response.Message = $"Lỗi hệ thống - {ex.Message}!";
            }
            return new ObjectResult(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto input)
        {
            var response = new ResponseDto<AccountDto>();
            try
            {
                var result = await _accountService.Login(input);
                if(result == null)
                {
                    response.Status = false;
                    response.Message = "Đăng nhập thất bại";
                }
                response.Data = result;
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
