using OvertimeRequestSystemAPI.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OvertimeRequestSystemAPI.Models;
using OvertimeRequestSystemAPI.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OvertimeRequestSystemAPI.Context;
using System.Net;

namespace OvertimeRequestSystemAPI.Controllers.NewControllers
{
    [Route("OvertimeRequestSystemAPI/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, int>
    {
    
        private EmployeeRepository EmployeeRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;

     
        public EmployeesController(EmployeeRepository repository, IConfiguration configuration, MyContext myContext) : base(repository)
        {
            this.EmployeeRepository = repository;
            this._configuration = configuration;
            this.context = myContext;
        }
        [HttpGet("GetCountPosition")]
        public ActionResult GetCountPosition()
        {
            var result = EmployeeRepository.GetCountPosition();
            if (result.Key == null)
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Data Masih Kosong" });
            }
            return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data Ditampilkan" });
        }
        [HttpGet("GetCountRole")]
        public ActionResult GetCountRole()
        {
            var result = EmployeeRepository.GetCountRole();
            if (result.Key == null)
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Data Masih Kosong" });
            }
            return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data Ditampilkan" });
        }


    }
}
