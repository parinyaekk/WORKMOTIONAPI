using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class LogModel
    {
        public class LOG
        {
            [Key]
            public int Log_ID { get; set; }
            public DateTime? Log_Date { get; set; }
            public string IP_Address { get; set; }
            public string Function_Name { get; set; }
            public string Function_Detail { get; set; }
            public string Error_Detail { get; set; }
        }
    }
}
