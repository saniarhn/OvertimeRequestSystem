﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.ViewModel
{
    public class RegisterVMClient
    {
        public string NIK { get; set; }
        public string FullName { get; set; }
        public string Gender { get;  set; }
        public string Phone { get; set; }
/*        public DateTime BirthDate { get; set; }*/
        public int Salary { get; set; }
        public string Email { get; set; }
        public string GPA { get; set; }
        public string Degree { get; set; }
    
        public string UniversityName { get; set; }
  
    }
}
