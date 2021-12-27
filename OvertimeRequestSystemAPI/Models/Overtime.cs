using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.Models
{
    [Table("TB_T_Overtime")]
    public class Overtime
    {
        [Key]
        public int OvertimeId { get; set; }

        public DateTime Date { get; set; }

        public int SumOvertimeHour { get; set; }
        public float OvertimeSalary { get; set; }
        public string StatusByManager { get; set; }
        public string StatusByFinance { get; set; }
        public int NIP { get; set; }
        [JsonIgnore]
        //penghubung one to many dengan overtime_detail
        public ICollection<OvertimeDetail> overtimeDetails { get; set; }
        [JsonIgnore]
        //penghubung one to many dengan Response
        public ICollection<Response> responses { get; set; }
        //penghubung one to many dengan employee
        [JsonIgnore]
        [ForeignKey("NIP")]
        public virtual Employee Employee { get; set; }
    }
}
