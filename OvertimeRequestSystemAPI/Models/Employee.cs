using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.Models
{
    [Table("TB_T_Employee")]
    public class Employee
    {
        [Key]
        public int NIP { get; set; }
        public string Name { get; set; }
        [Index(IsUnique = true)]
        public string Email { get; set; }
        public string Position { get; set;  }
        public string BasicSalary { get; set; }
        public int ManagerId { get; set; }
        //penghubung one to one dengan Account
        public virtual Account Account { get; set; }
        //penghubung one to many dengan overtime
        public ICollection<Overtime> overtimes { get; set; }
        //penghubung one to many dengan response
        public ICollection<Response> responses { get; set; }
    }
}
