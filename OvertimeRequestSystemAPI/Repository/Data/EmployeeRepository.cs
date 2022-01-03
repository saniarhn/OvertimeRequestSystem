using OvertimeRequestSystemAPI.Context;
using OvertimeRequestSystemAPI.Repository;
using OvertimeRequestSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using OvertimeRequestSystemAPI.ViewModel;

namespace OvertimeRequestSystemAPI.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, int>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

   /*     public KeyValuePair<List<string>, List<int>> GetCountPosition()
        {
            var getData = (from a in context.Accounts
                           join e in context.Employees on a.NIP equals e.NIP
                           group e by e.Position into count
                           select new
                           {
                               PositionName = count.Key,
                               NumberOfEmployees = count.Count()
                           }).ToList();
            List<string> PositionName = new List<string>();
            List<int> NumberOfEmployees = new List<int>();
            foreach (var x in getData)
            {
                PositionName.Add(x.PositionName);
                NumberOfEmployees.Add(x.NumberOfEmployees);
            }
            return new KeyValuePair<List<string>, List<int>>(PositionName, NumberOfEmployees);
        }*/

        /*public KeyValuePair<List<string>, List<int>> GetCountRole()
        {
            var getData = (from ar in context.AccountRoles
                           join r in context.Roles on ar.RoleId equals r.RoleId
                           group r by r.RoleName into count
                           select new
                           {
                               RoleName = count.Key,
                               NumberOfEmployees = count.Count()
                           }).ToList();
            List<string> RoleName = new List<string>();
            List<int> NumberOfEmployees = new List<int>();
            foreach (var x in getData)
            {
                RoleName.Add(x.RoleName);
                NumberOfEmployees.Add(x.NumberOfEmployees);
            }
            return new KeyValuePair<List<string>, List<int>>(RoleName, NumberOfEmployees);
        }
*/

        public IEnumerable<CountPositionVM> GetCountPosition()
        {
            var register = from a in context.Accounts
                           join e in context.Employees on a.NIP equals e.NIP
                           group e by e.Position into testcount
                           select new CountPositionVM()
                           {

                               PositionName = testcount.Key,
                               Quantity = testcount.Count(),

                           };
            return register.ToList();
        }

        public IEnumerable<CountRoleVM> GetCountRole()
        {
            var register = from ar in context.AccountRoles
                           join r in context.Roles on ar.RoleId equals r.RoleId
                           group r by r.RoleName into count
                           select new CountRoleVM()
                           {

                               RoleName = count.Key,
                               NumberOfEmployees = count.Count()

                           };
            return register.ToList();
        }
    }
}
