using OvertimeRequestSystemAPI.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OvertimeRequestSystemAPI.Models;
using OvertimeRequestSystemAPI.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OvertimeRequestSystemAPI.VM;
using OvertimeRequestSystemAPI.Context;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace OvertimeRequestSystemAPI.Controllers.NewControllers
{
    [Route("OvertimeRequestSystemAPI/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, int>
    {
        private AccountRepository AccountRepository;
        private readonly MyContext context;
        public IConfiguration _configuration;
        public AccountsController(AccountRepository repository, MyContext myContext) : base(repository)
        {
            this.AccountRepository = repository;
            this.context = myContext;
        }
        [HttpPost("Login")]
        public ActionResult Post(LoginVM loginVM)
        {
            var log = AccountRepository.Login(loginVM);
            var profile = AccountRepository.GetProfile(loginVM);
            if (log == 2)
            {
                var getUserData = (from e in context.Employees
                                   where e.Email == loginVM.Email
                                   join a in context.Accounts on e.NIP equals a.NIP
                                   join ar in context.AccountRoles on a.NIP equals ar.NIP
                                   join r in context.Roles on ar.RoleId equals r.RoleId
                                   select new
                                   {
                                       Email = e.Email,
                                       Name = r.RoleName
                                   }).ToList();

                var claims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Email, getUserData[0].Email)
                        };

                foreach (var userRole in getUserData)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole.Name));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn
                    );

                var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                claims.Add(new Claim("TokenSecurity", idtoken.ToString()));

                return Ok(new { status = HttpStatusCode.OK, idtoken, profile, Message = $"Login Berhasil" });
                //return Ok(new JWTokenVM { Token = idtoken, Messages = "Login Success" });
                //return Ok(new { status = HttpStatusCode.OK, result = profile, message = "Login Berhasil" });
                //return RedirectToAction("GetProfile", "Accounts", loginVM);
            }
            else if (log == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = log, message = "Password Salah, Login Gagal" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = log, message = "Email/Phone Number Tidak Ditemukan, Login Gagal" });
        }
    }
}
