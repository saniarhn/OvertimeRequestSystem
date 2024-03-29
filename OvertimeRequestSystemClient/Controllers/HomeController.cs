﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OvertimeRequestSystemClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login","Accounts");
       
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            var c = HttpContext.Session.GetString("Role");
          
            if (c == "employee")
            {
                return View("DashboardEmployee");
            }
            else if (c == "manager")
            {
                /*   return Json(Url.Action("OvertimeResponse", "Overtimes"));*/
                return View("DashboardManager");
            }
            else if (c == "finance")
            {
                return View("DashboardFinance");
            }
            else if (c == "director")
            {
                return View("DashboardDirector");
            }
            else if (c == "admin")
            {
                return View("DashboardAdmin");
            }
            else
            {
                return View("Index");
            }
        }
        public IActionResult DashboardEmployee()
        {
            return View("DashboardEmployee");
        }
        public IActionResult DashboardDirector()
        {
            return View("DashboardDirector");
        }
        public IActionResult DashboardFinance()
        {
            return View("DashboardFinance");
        }
        public IActionResult DashboardAdmin()
        {
            return View("DashboardAdmin");
        }

        [HttpGet("Unauthorized/")]
        public IActionResult Page_401()
        {
            return View("Page_401");
        }
        [HttpGet("Forbidden/")]
        public IActionResult Page_403()
        {
            return View("Page_403");
        }
        [HttpGet("NotFound/")]
        public IActionResult Page_404()
        {
            return View("Page_404");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
