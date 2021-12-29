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
        public IActionResult OvertimeRequestManager()
        {
            return View("OvertimeRequestManager");
        }
        public IActionResult OvertimeRequestEmployee()
        {
            return View("OvertimeRequestEmployee");
        }
        public IActionResult OvertimeHistory()
        {
            return View("OvertimeHistory");
        }

        [HttpPost]
        public JsonResult PostOvertimeRepository(OvertimeRequestVM overtimerequestVM)
        {
            var result = overtimeRepository.PostOvertimeRequest(overtimerequestVM);
            return Json(result);
        }

        public IActionResult OvertimeResponse()
        {
            return View("OvertimeResponse");
        }
        public IActionResult OvertimeResponseFinance()
        {
            return View("OvertimeResponseFinance");
        }
        [HttpPut]
        public JsonResult PutOvertimeResponseManager(OvertimeResponseVM overtimeResponseVM)
        {
            var result = overtimeRepository.OvertimeResponseManager(overtimeResponseVM);
            return Json(result);
        }
        [HttpPut]
        public JsonResult PutOvertimeResponseFinance(OvertimeResponseVM overtimeResponseVM)
        {
            var result = overtimeRepository.OvertimeResponseFinance(overtimeResponseVM);
            return Json(result);
        }

    }
}
