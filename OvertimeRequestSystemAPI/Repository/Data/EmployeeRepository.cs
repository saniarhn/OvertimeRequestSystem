using OvertimeRequestSystemAPI.Context;
using OvertimeRequestSystemAPI.Repository;
using OvertimeRequestSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OvertimeRequestSystemAPI.VM;

namespace OvertimeRequestSystemAPI.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, int>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
        public int Register(RegisterVM registerVM)
        {
            var checkNIP = context.Employees.Find(registerVM.NIP);
            var checkEmail = context.Employees.Where(x => x.Email == registerVM.Email).FirstOrDefault();
            if (checkNIP != null)
            {
                return 2;
            }
            else if (checkEmail != null)
            {
                return 3;
            }
            Employee employee = new Employee()
            {
                NIP = registerVM.NIP,
                Name = registerVM.Name,
                Email = registerVM.Email,
                Position = registerVM.Position,
            };
            Account account = new Account()
            {
                NIP = employee.NIP
                //Password = Hashing.Hashing.HashPassword(registerVM.Password)
                //Password = Hashing
            };
            context.Employees.Add(employee);
            context.Accounts.Add(account);
            context.SaveChanges();
            /*AccountRole accountRole = new AccountRole()
            {
                NIP = employee.NIP,
                RoleId = 2
            };
            context.AccountRoles.Add(accountRole);
            context.SaveChanges();*/
            return 1;
        }
    }
}
