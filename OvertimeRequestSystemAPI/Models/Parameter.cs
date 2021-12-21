﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.Models
{
    [Table("TB_M_Parameter")]
    public class Parameter
    {
        [Key]
        public int ParameterId { get; set; }
        public string ParameterName { get; set; }
        public float Value { get; set; }

    }
}
