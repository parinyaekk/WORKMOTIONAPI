using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class BaseModel
    {
        public class ResponseModel
        {
            public string Message { get; set; }
            public APIStatus Status { get; set; }
            public object Data { get; set; }
            public object subData { get; set; }
            public int? data_count { get; set; }
            public string FileName { get; set; }
        }

        public enum APIStatus
        {
            Successful = 0,
            Error = 1,
            SystemError = 2,
            InformationError = 3
        }

    }
}
