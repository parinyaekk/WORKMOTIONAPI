using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Care_AreaModel
    {
        public class Care_Area
        {
            [Key]
            public int ID { get; set; }
            public string Code { get; set; }
            public int? Province_ID { get; set; }
            public string Province_Name { get; set; }
            public int? Is_Active { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }
    }
}
