using OvertimeRequestSystemAPI.JSON;
using OvertimeRequestSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.ViewModel
{
    public class OvertimeRequestVM
    {
        
        public DateTime Date { get; set; }
        public int SumOvertimeHour { get; set; }
        public int OvertimeSalary { get; set; }
        public string StatusByManager { get; set; }
        public string StatusByFinance { get; set; }
        public int NIP { get; set; }

   /*     public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
        public string TaskName { get; set; }
        public string LocationName { get; set; }*/
        public int OvertimeId { get; set; }
        [JsonPropertyName("ListDetail")]
        [JsonConverter(typeof(OvertimeDetaillToStringConverter))]
        public List<OvertimeDetail> ListDetail{ get; set; }
    }
}
