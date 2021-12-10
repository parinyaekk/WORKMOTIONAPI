using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class ProvinceModel
    {
        public class Province
        {
            [Key]
            public int ID { get; set; }
            public int? Lang_ID { get; set; }
            public string Province_Name { get; set; }
        }
    }
}
