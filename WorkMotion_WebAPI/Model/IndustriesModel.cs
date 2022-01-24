using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class IndustriesModel
    {
        public class INDUSTRIES
        {
            [Key]
            public int Industries_ID { get; set; }
            public string Industries_Name { get; set; }
            public string Industries_Image_Path { get; set; }
            public string Industries_Description { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }

        }

        public class Request_Industries
        {
            public int? Industries_ID { get; set; }
            public string Industries_Name { get; set; }
            public string Industries_Image_Path { get; set; }
            public string Industries_Description { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
        }
    }
}
