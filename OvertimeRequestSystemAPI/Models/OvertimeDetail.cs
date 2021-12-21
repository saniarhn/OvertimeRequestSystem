using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.Models
{
    [Table("TB_T_OvertimeDetail")]
    public class OvertimeDetail
    {
        [Key]
        public int OvertimeDetailId { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
        public string TaskName { get; set; }
        public string LocationName { get; set; }
        public int OvertimeId { get; set; }
        //penghubung one to many dengan overtime
        public virtual Overtime Overtime { get; set; }
    }
}
