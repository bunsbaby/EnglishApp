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

        public async Task<bool> RegisterAccount(RegisterDto input)
        {
            var anyEmail = await englishContext.Accounts.AnyAsync(m => m.Email == input.Email);
            if (anyEmail) return false;
            var anyUserName = await englishContext.Accounts.AnyAsync(m => m.UserName == input.UserName);
            if (anyUserName) return false;
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
            return flag > -1;
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
        Task<bool> RegisterAccount(RegisterDto input);
        Task<AccountDto> Login(LoginDto input);
    }
}
