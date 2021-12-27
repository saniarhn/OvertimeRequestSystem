using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.ViewModel
{
    public class OvertimeResponseVM
    {
        public string StatusByManager { get; set; }
        public string StatusByFinance { get; set; }
        public string ResponseDescription { get; set; }
        public int ManagerOrFinanceId { get; set; }
        public int OvertimeId { get; set; }
    }
}
