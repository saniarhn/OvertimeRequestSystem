using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.ViewModel
{
    public class GetDetailAccountVM
    {
        public int NIP { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public float BasicSalary { get; set; }
        public int ManagerId { get; set; }
        public string AccountStatus { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
