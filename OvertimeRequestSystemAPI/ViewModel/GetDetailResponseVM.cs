using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.ViewModel
{
    public class GetDetailResponseVM
    {
        public int NIP { get; set; }
        public int OvertimeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime Date { get; set; }
        public int SumOvertimeHour { get; set; }
        public int OvertimeDetailId { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
        public string TaskName { get; set; }
        public string LocationName { get; set; }
    }
}
