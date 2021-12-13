using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class BannerModel
    {
        public class BANNER
        {
            [Key]
            public int Banner_ID { get; set; }
            public string Banner_Name { get; set; }
            public string Banner_Topic { get; set; }
            public string Banner_Description { get; set; }
            public string Banner_Image_Path { get; set; }
            public bool? Is_Display { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
        }

        public class Request_Banner
        {
            public int? Banner_ID { get; set; }
            public string Banner_Name { get; set; }
            public string Banner_Topic { get; set; }
            public string Banner_Description { get; set; }
            public string Banner_Image_Path { get; set; }
        }
    }
}
