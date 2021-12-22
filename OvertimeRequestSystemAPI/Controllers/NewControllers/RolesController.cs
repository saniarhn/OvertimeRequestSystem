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

namespace OvertimeRequestSystemAPI.Controllers.NewControllers
{
    [Route("OvertimeRequestSystemAPI/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Role, RoleRepository, int>
    {
   
        private RoleRepository RoleRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;

        public RolesController(RoleRepository repository, IConfiguration configuration, MyContext myContext) : base(repository)
        {
            this.RoleRepository = repository;
            this._configuration = configuration;
            this.context = myContext;
        }
    }
}
