using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.Models
{
    [Table("TB_T_Account")]
    public class Account
    {
        [Key]
        public int NIP { get; set; }
        public string Password { get; set; }
        public string AccountStatus { get; set; }
        //penghubung one to one dengan employee
        public virtual Employee Employee { get; set; }
        //penghubung one to many dengan account_role
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
