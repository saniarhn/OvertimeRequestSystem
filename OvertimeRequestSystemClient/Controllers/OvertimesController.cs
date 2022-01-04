using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [Authorize]
        public IActionResult Index()
        {
            var c = HttpContext.Session.GetString("NIP");
            var d = Int32.Parse(c);
            ViewData["nip"] = d;
            return View("OvertimeRequest");
        }
        [Authorize(Roles = "Manager")]
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
            var sessionEmail = HttpContext.Session.GetString("Email");
            var result = overtimeRepository.PostOvertimeRequest(overtimerequestVM, sessionEmail);
            return Json(result);
        }

        public IActionResult OvertimeResponse()
        {
            var c = HttpContext.Session.GetString("Role");
            var e = HttpContext.Session.GetString("NIP");
            var d = Int32.Parse(e);
            ViewData["nip"] = d;
            if (c == "manager")
            {
                return View("OvertimeResponse");
            }
            else if (c == "finance")
            {
                return View("OvertimeResponseFinance");
            }
            else
            {
                return View("OvertimeResponseDirector");
            }
          
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
        [HttpPut]
        public JsonResult PutOvertimeResponseDirector(OvertimeResponseVM overtimeResponseVM)
        {

            var result = overtimeRepository.OvertimeResponseDirector(overtimeResponseVM);
            return Json(result);
        }

        /*   [HttpGet]
           public JsonResult GetOvertimeHistory()
           {
               var getSessionNIP = HttpContext.Session.GetString("NIP");
               var sessionNIP = Int32.Parse(getSessionNIP);
               var result = overtimeRepository.OvertimeHistory(sessionNIP);
               return Json(result);
           }*/

        [HttpGet]
        public async Task<JsonResult> GetOvertimeHistory()
        {
            var getSessionNIP = HttpContext.Session.GetString("NIP");
            var sessionNIP = Int32.Parse(getSessionNIP);
            var result = await overtimeRepository.GetOvertimeHistory(sessionNIP);
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetResponseForManager()
        {
            var getSessionNIP = HttpContext.Session.GetString("NIP");
            var sessionNIP = Int32.Parse(getSessionNIP);
            var result = await overtimeRepository.GetResponseForManager(sessionNIP);
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetResponseForFinance()
        {
            var result = await overtimeRepository.GetResponseForFinance();
            return Json(result);
        }


        /*        [HttpGet]
                public async Task<JsonResult> GetDetailResponse()
                {
                    var getSessionNIP = HttpContext.Session.GetString("NIP");
                    var sessionNIP = Int32.Parse(getSessionNIP);
                    var result = await overtimeRepository.GetDetailResponse(sessionNIP);
                    return Json(result);
                }*/
        [Route("~/overtimes/GetDetailResponse/{overtimeId}")]
        public async Task<JsonResult> GetDetailResponse(int overtimeId)
        {
            var result = await overtimeRepository.GetDetailResponse(overtimeId);
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetResponseForDirector()
        {
            var getSessionNIP = HttpContext.Session.GetString("NIP");
            var sessionNIP = Int32.Parse(getSessionNIP);
            var result = await overtimeRepository.GetResponseForDirector(sessionNIP);
            return Json(result);
        }


        [HttpGet]
        public async Task<JsonResult> GetCountSalary()
        {
            var getSessionNIP = HttpContext.Session.GetString("NIP");
            var sessionNIP = Int32.Parse(getSessionNIP);
            var result = await overtimeRepository.GetCountSalary(sessionNIP);
            return Json(result);
        }


    }
}
