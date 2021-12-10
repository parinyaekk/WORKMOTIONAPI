using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Product_PictureModel
    {
        public class Product_Picture
        {
            [Key]
            public int ID { get; set; }
            public int? FK_Product_ID { get; set; }
            public string File_Path { get; set; }
            public string File_Type { get; set; }
            public string File_Name { get; set; }
            public string Href_Link { get; set; }
            public string CoverImage_Path { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }

        public class ResponseProduct_Picture
        {
            public int? spare_id { get; set; }
            public int? install_id { get; set; }
            public string file_path { get; set; }
            public string file_type { get; set; }
            public string file_name { get; set; }
            public string link { get; set; }
            public string coverimage_path { get; set; }
        }

        public class OldFile
        {
            public int id { get; set; }
            public string name { get; set; }
        }
    }
}
