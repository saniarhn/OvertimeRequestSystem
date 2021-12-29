using OvertimeRequestSystemAPI.Base;
using OvertimeRequestSystemAPI.Context;
using OvertimeRequestSystemAPI.Models;
using OvertimeRequestSystemAPI.Repository.Data;
using OvertimeRequestSystemAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.Controllers.NewControllers
{
    [Route("OvertimeRequestSystemAPI/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, int>
    {
        private AccountRepository accountRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;
      
     
        public AccountsController(AccountRepository repository, IConfiguration configuration, MyContext myContext) : base(repository)
        {
            this.accountRepository = repository;
            this._configuration = configuration;
            this.context = myContext;
        }

        [HttpPost("Login")]
        public ActionResult Post(LoginVM loginVM)
        {
            var result = accountRepository.Login(loginVM);
            if (result == 1)
            {
                /* return Ok(new { status = HttpStatusCode.OK, result, messageResult = "berhasil login" });/
                return RedirectToAction("GetAll",loginVM);
                /     return Redirect(“Accounts / All”);*/
                var getUserData = (
                                    from e in context.Set<Employee>()
                                    join a in context.Set<Account>() on e.NIP equals a.NIP
                                    join ar in context.Set<AccountRole>() on a.NIP equals ar.NIP
                                    join r in context.Set<Role>() on ar.RoleId equals r.RoleId
                                    where e.Email == loginVM.Email
                                    select new
                                    {
                                        Email = e.Email,
                                        NIP = e.NIP,
                                        Name = r.RoleName
                                    }).ToList();

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email, getUserData[0].Email),
                    new Claim(JwtRegisteredClaimNames.NameId, getUserData[0].NIP.ToString()),
                };

                foreach (var userRole in getUserData)
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, userRole.Name));
                }

                //foreach (var userNIP in getUserData)
                //{
                //    claims.Add(new Claim(ClaimTypes.NameIdentifier, userNIP.NIP.ToString()));

                //}

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

                /*   return Ok(result);*/
                return Ok(new JWTokenVM { Token = idtoken, Messages = "Login Sucsses" });
                /*                return Ok(new { status = HttpStatusCode.OK, token = idtoken, data = accountRepository.GetProfile(loginVM), Message = $"Login Berhasil" });*/
                /*            return Ok(new { status = HttpStatusCode.OK, Message = $"Login Berhasil" });*/
                /*             return Ok(new
                             {
                                 status = HttpStatusCode.OK,
                                 data = accountRepository.GetProfile(loginVM),
                                 message = "Berhasil Login"
                             });*/
               /* return Ok(new { status = HttpStatusCode.OK, token = idtoken, Message = $"Login Berhasil" });*/
            }
            else if (result == 2)
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Password Salah" });
            }
            else if (result == 3)
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Akun tidak ditemukan" });
            }

            return NotFound(new { status = HttpStatusCode.BadRequest, result, messageResult = "gagal login" });
        }

        [HttpPost("InsertAccount")]
        public  ActionResult<AccVM> Post(AccVM accVM)
        {
            try
            {
                var result = accountRepository.InsertAccount(accVM);
                return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Data telah berhasil di buat" });
          /*      return Ok(result);*/
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Gagal memasukan data" });
            }
        }

       /* [HttpPost("InsertAccount")]
        public ActionResult Post(Account account)
        {
            try
            {
                var result = accountRepository.InsertAccount(account);
                *//*   return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Data telah berhasil di buat" });*//*
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "Gagal memasukan data" });
            }
        }*/

    }
    
}
