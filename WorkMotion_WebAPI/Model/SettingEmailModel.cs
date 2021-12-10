using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class SettingEmailModel
    {
        public class SettingEmail
        {
            [Key]
            public int ID { get; set; }
            public string Care_Center_Code { get; set; }
            public string Email { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }
        public class RequestSettingEmail
        {
            public int ID { get; set; }
            public string Care_Center_Code { get; set; }
            public string Email { get; set; }
            public int? Type { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
        }
    }
}
