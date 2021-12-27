using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.ViewModel
{
    public class RegisterVM
    {
      
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

      
        public string Degree { get; set; }
        public string GPA { get; set; }
        public int UniversityId { get; set; }
        public string Gender { get; internal set; }
    }


}

