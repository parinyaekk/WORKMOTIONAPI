using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Menu_AdminModel
    {
        public class Menu_Admin
        {
            [Key]
            public int ID { get; set; }
            public string Menu_Name { get; set; }
            public string Menu_Desc { get; set; }
            public string Menu_Link { get; set; }
            public int? Menu_Order { get; set; }
            public int? FK_Menu_ID { get; set; }
            public int? Hide_Header { get; set; }
            public int? Hide_Footer { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }
    }
}
