using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Care_CenterModel
    {
        public class Care_Center
        {
            [Key]
            public int ID { get; set; }
            public int? Lang_ID { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Office_Hour { get; set; }
            public string Map_File { get; set; }
            public string Map_Href { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public int? Is_Active { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }

        public class GetAllDataCareCenterModel
        {
            public int ProvinceID { get; set; }
            public int? Lang_ID { get; set; }
        }
    }
}
