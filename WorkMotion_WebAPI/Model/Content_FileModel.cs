using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Content_FileModel
    {
        public class Content_File
        {
            [Key]
            public int ID { get; set; }
            public int? FK_Content_ID { get; set; }
            public string File_Path { get; set; }
            public string File_Type { get; set; }
            public string File_Name { get; set; }
            public string Href_Link { get; set; }
            public string Description { get; set; }
            public string Link_Download { get; set; }
            public int? Flag_Button { get; set; }
            public int? File_Order { get; set; }
            public string CoverImage_Path { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }

        public class ResponseContent_File
        {
            public int id { get; set; }
            public string file_name { get; set; }
            public string file_type { get; set; }
            public string file_path { get; set; }
            public string link { get; set; }
            public string description { get; set; }
            public string link_download { get; set; }
            public int? file_order { get; set; }
            public int? flag_button { get; set; }
            public string coverimage_path { get; set; }
        }
    }
}
