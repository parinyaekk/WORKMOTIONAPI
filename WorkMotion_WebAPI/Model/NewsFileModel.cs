using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class NewsFileModel
    {
        public class NEWSFILE
        {
            [Key]
            public int News_File_ID { get; set; }
            public int FK_News_ID { get; set; }
            public string News_File_Path { get; set; }
            public string News_File_Type { get; set; }
            public string News_File_Name { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
        }

        public class Request_NewsFile
        {
            public int News_File_ID { get; set; }
            public int FK_News_ID { get; set; }
            public string News_File_Path { get; set; }
            public string News_File_Type { get; set; }
            public string News_File_Name { get; set; }
            public bool? ActiveFlag { get; set; }

        }
    }
}
