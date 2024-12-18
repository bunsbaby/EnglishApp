using EnglishApp.Entity;
using EnglishApp.Models.Accounts;
using EnglishApp.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EnglishApp.Services.Account
{
    public class AccountService : GenericRepository<AccountEntity, EnglishContext>, IAccountService
    {
        private readonly IConfiguration _configuration;
        public AccountService(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }
        public async Task<AccountDto> Login(LoginDto input)
        {
            var accountE = await englishContext.Accounts.FirstOrDefaultAsync(m => m.UserName == input.UserName && m.Password == input.Password);
            if(accountE == null) return null;
            var account = new AccountDto
            {
                Email = accountE.Email,
                Id = accountE.Id,
                UserName = accountE.UserName,
            };
            var jwtToken = GenerateJwtToken(account);
            account.Token = jwtToken;
            return account;
        }

        public async Task<int> RegisterAccount(RegisterDto input)
        {
            var anyEmail = await englishContext.Accounts.AnyAsync(m => m.Email == input.Email);
            if (anyEmail) return 2;
            var anyUserName = await englishContext.Accounts.AnyAsync(m => m.UserName == input.UserName);
            if (anyUserName) return 4;
            var anyStudentEmail = await englishContext.Students.AnyAsync(m => m.Email == input.Email);
            var anyTeacherEmail = await englishContext.Teachers.AnyAsync(m => m.Email == input.Email);
            if ((input.Role == 1 && !anyStudentEmail) || (input.Role == 2 && !anyTeacherEmail)) return 3;
            var entity = new AccountEntity()
            {
                Email = input.Email,
                Role = input.Role,
                Password = input.Password,
                UserName = input.UserName,
                CreatedAt = DateTime.Now
            };
            englishContext.Accounts.Add(entity);
            var flag = await englishContext.SaveChangesAsync();
            var accountId = englishContext.Accounts.SingleOrDefault(m => m.Email == input.Email).Id;
            using (var db = new EnglishContext())
            {
                if (input.Role == 1)
                {
                    var studentEntity = db.Students.Where(m => m.Email == input.Email).First();
                    studentEntity.AccountId = accountId;
                    db.SaveChanges();
                }
                else if (input.Role == 2)
                {
                    var teacherEntity = db.Teachers.Where(m => m.Email == input.Email).First();
                    teacherEntity.AccountId = accountId;
                    db.SaveChanges();
                }
            }
            return flag > -1 ? 1 : 0;
        }

        private string GenerateJwtToken(AccountDto input)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Sub, input.Id.ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public interface IAccountService
    {
        Task<int> RegisterAccount(RegisterDto input);
        Task<AccountDto> Login(LoginDto input);
    }
}
