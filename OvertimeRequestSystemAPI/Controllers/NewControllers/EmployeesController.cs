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
using System.Net;

namespace OvertimeRequestSystemAPI.Controllers.NewControllers
{
    [Route("OvertimeRequestSystemAPI/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, int>
    {
        private EmployeeRepository EmployeeRepository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.EmployeeRepository = repository;
        }
        [HttpPost("Register")]
        public ActionResult Post(RegisterVM registerVM)
        {
            var masuk = EmployeeRepository.Register(registerVM);
            if (masuk == 1)
            {
                /*return Ok(new { status = HttpStatusCode.OK, result = masuk, message = "Pendaftaran Berhasil" });*/
                return Ok(masuk);
            }
            else if (masuk == 2)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = masuk, message = "NIK sudah terdaftar, Register Gagal" });
            }
            else if (masuk == 3)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = masuk, message = "Email Sudah terdaftar, Register Gagal" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = masuk, message = "Register Gagal" });
        }

    }
}
