using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class HDYH_OptionModel
    {
        public class HDYH_OPTION
        {
            [Key]
            public int HDYH_Option_ID { get; set; }
            public string HDYH_Option_Name { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
        }

        public class Request_HDYH_Option
        {
            public int? HDYH_Option_ID { get; set; }
            public string HDYH_Option_Name { get; set; }
        }
    }
}
