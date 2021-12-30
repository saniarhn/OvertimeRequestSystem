using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.ViewModel
{
    public class GetResponseVM
    {
        public int NIP { get; set; }
        public int OvertimeId { get; set; }

        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime Date { get; set; }
        public int SumOvertimeHour { get; set; }
        public string StatusByManager { get; set; }
        public string StatusByFinance { get; set; }
    }
}
