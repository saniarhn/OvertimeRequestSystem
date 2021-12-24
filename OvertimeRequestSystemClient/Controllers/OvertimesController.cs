using Microsoft.AspNetCore.Mvc;
using OvertimeRequestSystemAPI.Models;
using OvertimeRequestSystemAPI.ViewModel;
using OvertimeRequestSystemClient.Base.Controllers;
using OvertimeRequestSystemClient.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemClient.Controllers
{
    public class OvertimesController : BaseController<Overtime, OvertimeRepository, int>
    {
        private OvertimeRepository overtimeRepository;

        public OvertimesController(OvertimeRepository repository) : base(repository)
        {
            this.overtimeRepository = repository;
        }
        public IActionResult Index()
        {
            return View("OvertimeRequest");
        }

        [HttpPost]
        public JsonResult PostOvertimeRepository(OvertimeRequestVM overtimerequestVM)
        {
            var result = overtimeRepository.PostOvertimeRequest(overtimerequestVM);
            return Json(result);
        }
    }
}
