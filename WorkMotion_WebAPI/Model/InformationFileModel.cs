using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class InformationFileModel
    {
        public class INFORMATIONFILE
        {
            [Key]
            public int Information_File_ID { get; set; }
            public int FK_Information_ID { get; set; }
            public string Information_File_Path { get; set; }
            public string Information_File_Type { get; set; }
            public string Information_File_Name { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
        }

        public class Request_InformationFileModel
        {
            public int Information_File_ID { get; set; }
            public int FK_Information_ID { get; set; }
            public string Information_File_Path { get; set; }
            public string Information_File_Type { get; set; }
            public string Information_File_Name { get; set; }
        }
    }
}
