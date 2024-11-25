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
                response.Status = result;
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
