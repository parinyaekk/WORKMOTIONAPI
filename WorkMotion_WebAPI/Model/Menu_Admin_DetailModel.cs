using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Menu_Admin_DetailModel
    {
        public class Menu_Admin_Detail
        {
            [Key]
            public int ID { get; set; }
            public int? FK_Menu_ID { get; set; }
            public int? FK_User_Group_ID { get; set; }
            public string Permission { get; set; }
        }
    }
}
