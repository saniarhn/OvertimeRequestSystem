using OvertimeRequestSystemAPI.ViewModel;
using OvertimeRequestSystemClient.Base.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OvertimeRequestSystemAPI.Models;
using OvertimeRequestSystemClient.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace OvertimeRequestSystemClient.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, int>
    {
        private AccountRepository accountRepository;

        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.accountRepository = repository;
        }
        public IActionResult Login()
        {
            return View("Login");
        }
        public IActionResult CreateAccount()
        {
            return View("CreateAccount");
        }
        [HttpPost]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await accountRepository.Auth(login);
            var token = jwtToken.Token;

            var token2 = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var email = token2.Claims.First(c => c.Type == "email").Value;
            var nip = token2.Claims.First(c => c.Type == "nameid").Value;
            var role = token2.Claims.First(c => c.Type == "unique_name").Value;

            if (token == null)
            {

                return Json(Url.Action("Login", "Accounts"));
            }

            HttpContext.Session.SetString("JWToken", token);

            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("NIP", nip);
            HttpContext.Session.SetString("Role", role);
            /*            var a = HttpContext.Session.GetString("Email");
                        var b = HttpContext.Session.GetString("NIP");
                        var c = HttpContext.Session.GetString("Role");*/
            /*       return Json(Url.Action("DashboardAdmin", "Home"));*/
            return Json(Url.Action("Index", "Overtimes"));
        }


        public IActionResult Logout()
        {
            var sessionData = HttpContext.Session.GetString("JWToken");

            if (sessionData != null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("DashboardAdmin", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("Create");
        }
    }
 }
