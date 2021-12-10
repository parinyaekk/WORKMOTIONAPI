using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class DistrictModel
    {
        public class District
        {
            [Key]
            public int ID { get; set; }
            public int? Lang_ID { get; set; }
            public int? FK_Province_ID { get; set; }
            public string District_Name { get; set; }
        }
    }
}
