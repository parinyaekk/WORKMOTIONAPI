using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Receipt_FileModel
    {
        public class Receipt_File
        {
            [Key]
            public int ID { get; set; }
            public int? FK_Warranty_ID { get; set; }
            public string File_Path { get; set; }
            public string File_Type { get; set; }
            public string File_Name { get; set; }
            public string Href_Link { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }

        public class OldFile
        {
            public int id { get; set; }
            public string name { get; set; }
        }
    }
}
