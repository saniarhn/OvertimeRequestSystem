using OvertimeRequestSystemAPI.Context;
using OvertimeRequestSystemAPI.Repository;
using OvertimeRequestSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OvertimeRequestSystemAPI.VM;
using Microsoft.EntityFrameworkCore;

namespace OvertimeRequestSystemAPI.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, int>
    {
        private readonly MyContext context;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
        public int Login(LoginVM loginVM)
        {
            var getlogin = context.Employees.Include("Account").Where(e => e.Email == loginVM.Email).FirstOrDefault();
            if (getlogin != null)
            {
                var checkPassword = Hashing.Hashing.ValidatePassword(loginVM.Password, getlogin.Account.Password);
                if (checkPassword == true)
                {
                    return 2;
                }
                return 3;
            }
            return 1;
        }
        public IEnumerable<ProfileVM> GetProfile(LoginVM loginVM)
        {
            var profile = from e in context.Employees
                          where e.Email == loginVM.Email
                          join a in context.Accounts on e.NIP equals a.NIP
                          select new ProfileVM()
                          {
                              NIP = e.NIP,
                              Name = e.Name,
                              Email = e.Email
                          };
            return profile.ToList();
        }
    }
}
