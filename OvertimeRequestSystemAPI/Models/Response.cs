using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.Models
{
    [Table("TB_M_Response")]
    public class Response
    {
        [Key]
        public int ResponseId { get; set; }
        public string ResponseDescription { get; set; }
        public int ManagerOrFinanceId { get; set; }
        public int OvertimeId { get; set; }
        //penghubung one to many dengan overtime
        public virtual Overtime Overtime { get; set; }
        //penghubung one to many dengan employee
        [ForeignKey("ManagerOfFinanceId")]
        public virtual Employee Employee { get; set; }
    }
}
