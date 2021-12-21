using OvertimeRequestSystemAPI.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OvertimeRequestSystemAPI.Models;
using OvertimeRequestSystemAPI.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.Controllers.NewControllers
{
    [Route("OvertimeRequestSystemAPI/[controller]")]
    [ApiController]
    public class OvertimeDetailsController : BaseController<OvertimeDetail, OvertimeDetailRepository, int>
    {
        private OvertimeDetailRepository OvertimeDetailRepository;
        public OvertimeDetailsController(OvertimeDetailRepository repository) : base(repository)
        {
            this.OvertimeDetailRepository = repository;
        }
    }
}

