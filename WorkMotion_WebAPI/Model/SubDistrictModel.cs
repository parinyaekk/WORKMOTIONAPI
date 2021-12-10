using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class SubDistrictModel
    {
        public class Sub_District
        {
            [Key]
            public int ID { get; set; }
            public int? Lang_ID { get; set; }
            public string Sub_District_Name { get; set; }
            public string Zip_Code { get; set; }
            public int? FK_District_ID { get; set; }
            public int? FK_Province_ID { get; set; }
        }
    }
}
