using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.ViewModel
{
    public class LoginVM
    {
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email2 { get; set; }
    }
}
