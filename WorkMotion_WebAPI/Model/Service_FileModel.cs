using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Service_FileModel
    {
        public class Service_File
        {
            [Key]
            public int ID { get; set; }
            public int? Serviceinformation_ID { get; set; }
            public string File_Path { get; set; }
            public string File_Type { get; set; }
            public string File_Name { get; set; }
            public int? Service_Type { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }

        public class Service_File_Response
        {
            public int id { get; set; }
            public int? service_id { get; set; }
            public string file_path { get; set; }
            public string file_type { get; set; }
            public string file_name { get; set; }
            public int? service_type { get; set; }
        }
    }
}
