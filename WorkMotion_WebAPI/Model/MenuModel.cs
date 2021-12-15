using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class MenuModel
    {
        public class MENU
        {
            [Key]
            public int Menu_ID { get; set; }
            public string Menu_Name { get; set; }
            public string Meta_Title { get; set; }
            public string Meta_Keyword { get; set; }
            public string Meta_Description { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }

        }

        public class Request_Menu
        {
            public int? Menu_ID { get; set; }
            public string Menu_Name { get; set; }
            public string Meta_Title { get; set; }
            public string Meta_Keyword { get; set; }
            public string Meta_Description { get; set; }
            public string CreateBy { get; set; }
        }
    }
}
