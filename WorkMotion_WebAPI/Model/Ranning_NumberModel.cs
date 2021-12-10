using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Ranning_NumberModel
    {
        public class Running_Number
        {
            [Key]
            public int ID { get; set; }
            public int? Number { get; set; }
            public string Service_Center { get; set; }
            public int? Member_Type { get; set; }
        }
    }
}
