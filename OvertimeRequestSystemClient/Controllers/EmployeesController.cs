using Microsoft.AspNetCore.Mvc;
using OvertimeRequestSystemAPI.Models;
using OvertimeRequestSystemClient.Base.Controllers;
using OvertimeRequestSystemClient.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemClient.Controllers
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, int>
    {
        private EmployeeRepository employeeRepository;

        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("Create");
        }
        public IActionResult AboutEmployee()
        {
            return View("AboutEmployee");
        }

        [HttpGet]
        public async Task<JsonResult> GetCountPosition()
        {
            var result = await employeeRepository.GetCountPosition();
            return Json(result);
        }
    }
 
}
