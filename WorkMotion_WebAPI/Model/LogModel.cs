using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class LogModel
    {
        public class Log
        {
            [Key]
            public int ID { get; set; }
            public string Function { get; set; }
            public string Message { get; set; }
            public DateTime DateTime { get; set; }
        }
    }
}
