using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.Models
{
    [Table("TB_T_AccountRole")]
    public class AccountRole
    {
        [Key]
        public int AccountRoleId { get; set; }
        public int NIP { get; set; }
        public int RoleId { get; set; }
        //penghubung one to many dengan account
        [ForeignKey("NIP")]
        public virtual Account Account { get; set; }
        //penghubung one to many dengan role
        public virtual Role Role { get; set; }
    }
}
