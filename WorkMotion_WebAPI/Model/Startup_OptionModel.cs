using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Startup_OptionModel
    {
        public class STARTUP_OPTION
        {
            [Key]
            public int Startup_Option_ID { get; set; }
            public string Startup_Option_Name { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
        }

        public class Request_STARTUP_Option
        {
            public int? Startup_Option_ID { get; set; }
            public string Startup_Option_Name { get; set; }
        }
    }
}
