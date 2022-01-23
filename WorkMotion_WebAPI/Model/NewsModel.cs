using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class NewsModel
    {
        public class NEWS
        {
            [Key]
            public int News_ID { get; set; }
            public string News_Title { get; set; }
            public string News_Content { get; set; }
            public string News_Main_Image_Path { get; set; }
            public string News_Author { get; set; }
            public string News_Tags { get; set; }
            public DateTime? News_Publish_Date { get; set; }
            public bool? Is_Display { get; set; }
            public bool? Is_Highlight { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
        }

        public class Request_News
        {
            public int? News_ID { get; set; }
            public string News_Title { get; set; }
            public string News_Content { get; set; }
            public string News_Main_Image_Path { get; set; }
            public string News_Author { get; set; }
            public string News_Tags { get; set; }
            public DateTime? News_Publish_Date { get; set; }
            public bool? Is_Display { get; set; }
            public bool? Is_Highlight { get; set; }
            public string CreateBy { get; set; }
        }

        public class OldFile
        {
            public int id { get; set; }
            public string name { get; set; }
        }

    }
}
