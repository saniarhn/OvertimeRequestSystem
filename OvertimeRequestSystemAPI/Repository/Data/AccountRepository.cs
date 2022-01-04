
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using OvertimeRequestSystemAPI.Context;
using OvertimeRequestSystemAPI.Models;
using OvertimeRequestSystemAPI.Repository;
using OvertimeRequestSystemAPI.ViewModel;



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
            var result = 0;
            try
            {
                var getEmail = context.Employees.Where(e => e.Email == loginVM.Email).FirstOrDefault();
                var getNIP = getEmail.NIP;
                var getDataAccount = context.Accounts.Find(getNIP);
                var getStatusAccount = getDataAccount.AccountStatus;

         /*       var getStatus = (from e in context.Set<Employee>()
                                 join a in context.Set<Account>() on e.NIP equals a.NIP
                                 where e.Email == loginVM.Email
                                 && a.AccountStatus == "Active"
                                 select a.AccountStatus).Single();*/

                var pass = (from e in context.Set<Employee>()
                            join a in context.Set<Account>() on e.NIP equals a.NIP
                            where e.Email == loginVM.Email
                            select a.Password).Single();

                if (getEmail != null)
                {
                    if(getStatusAccount == "Active")
                    {
                        var getPassword = Hashing.Hashing.ValidatePassword(loginVM.Password, pass);
                        /*    var getPassword = context.Accounts.Where(e => e.Password == loginVM.Password).FirstOrDefault();*/
                        if (getPassword)
                        {
                            result = 1;
                        }
                        else
                        {
                            result = 2;
                        }

                    }
                    else
                    {
                        result = 3;
                    }
                
                }
                else
                {
                    result = 3;
                }

            }
            catch (Exception)
            {
                result = 0;
            }

            return result;

        }
        public int InsertAccount(AccVM accVM)
        {

            /*    var check = context.AccVM.Add(accVM);
                if (check == null)
                {*/
            Account a = new Account()
            {
                NIP = accVM.NIP,
                Password = Hashing.Hashing.HashPassword(accVM.Password),
                AccountStatus = accVM.AccountStatus,
            };
            context.Accounts.Add(a);
            /*    }*/
            var result = context.SaveChanges();

            AccountRole b = new AccountRole()
            {
                NIP = a.NIP,
                RoleId = accVM.RoleId,
            };
            context.AccountRoles.Add(b);
            result = context.SaveChanges();
            return result;


        }
        public IEnumerable<GetDetailAccountVM> GetDetailAccount(int nip)
        {
            var getData = from r in context.Roles
                          join ar in context.AccountRoles on r.RoleId equals ar.RoleId
                          join a in context.Accounts on ar.NIP equals a.NIP
                          join e in context.Employees on a.NIP equals e.NIP
                          where e.NIP == nip
                          select new GetDetailAccountVM()
                          {
                              NIP = e.NIP,
                              Name = e.Name,
                              Email = e.Email,
                              Position = e.Position,
                              BasicSalary = e.BasicSalary,
                              ManagerId = e.ManagerId,
                              AccountStatus = a.AccountStatus,
                              RoleId = ar.RoleId,
                              RoleName = r.RoleName
                          };
            return getData.ToList();
        }
        /*        public override int InsertAccount(Account account)
                {

                    var check = context.Accounts.Add(account);
                    if (check == null)
                    {
                        Account a = new Account()
                        {
                            NIP = account.NIP,
                            Password = Hashing.Hashing.HashPassword(account.Password)
                        };
                        context.Accounts.Add(a);
                    }
                    var result = context.SaveChanges();
                    return result;

                }*/
    }
}
