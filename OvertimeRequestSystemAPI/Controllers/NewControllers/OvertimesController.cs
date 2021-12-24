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
using OvertimeRequestSystemAPI.ViewModel;
using System.Net;

namespace OvertimeRequestSystemAPI.Controllers.NewControllers
{
    [Route("OvertimeRequestSystemAPI/[controller]")]
    [ApiController]
    public class OvertimesController : BaseController<Overtime, OvertimeRepository, int>
    {
   

        private OvertimeRepository OvertimeRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;

        public OvertimesController(OvertimeRepository repository, IConfiguration configuration, MyContext myContext) : base(repository)
        {
            this.OvertimeRepository = repository;
            this._configuration = configuration;
            this.context = myContext;
        }
        [HttpPost("OvertimeRequest")]
        public ActionResult Post(OvertimeRequestVM overtimerequestVM)
        {
            var result = OvertimeRepository.OvertimeRequest(overtimerequestVM);
            if (result == 1)
            {
                /*return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data berhasil ditambahkan" });*/
                return Ok(result);

            }
            else if (result == 2)
            {
                /*      return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "NIK sudah digunakan" });*/
                return Ok(new { status = HttpStatusCode.OK, result, messageResult = "berhasil masuk" });
            }
            else if (result == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "Email sudah digunakan" });
            }
            else if (result == 4)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "Phone sudah digunakan" });
            }
            else if (result == 5)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "UniversityId tidak ditemukan" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "Sepertinya terjadi kesalahan, periksa kembali!" });

        }
    }
}
