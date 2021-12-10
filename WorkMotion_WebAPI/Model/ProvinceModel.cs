using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class ProvinceModel
    {
        public class GROUP_COURSE
        {
            [Key]
            public long GROUP_COURSE_ID { get; set; }
            public string GROUP_COURSE_NAME { get; set; }
            public bool? IS_ACTIVE { get; set; }
            public string CREATE_BY { get; set; }
            public DateTime? CREATE_DATE { get; set; }
            public string UPDATE_BY { get; set; }
            public DateTime? UPDATE_DATE { get; set; }
        }
    }
}
