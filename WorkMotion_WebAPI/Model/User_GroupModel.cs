using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class User_GroupModel
    {
        public class User_Group
        {
            [Key]
            public int ID { get; set; }
            public string Groupname { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }

        public class PermissionPerPageModel
        {
            public int? ID { get; set; }
            public int? PerPage { get; set; }
            public int? Page { get; set; }
            public string Search { get; set; }           
        }
    }
}
