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
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private AccountRoleRepository accountRoleRepository;

        public AccountRolesController(AccountRoleRepository repository) : base(repository)
        {
            this.accountRoleRepository = repository;
        }
        public IActionResult Index()
        {
            return View("Index");
        }

    }
}
