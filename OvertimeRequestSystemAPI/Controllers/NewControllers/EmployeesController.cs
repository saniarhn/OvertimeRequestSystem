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
    
    }
}
