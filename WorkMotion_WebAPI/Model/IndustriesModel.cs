using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class IndustriesModel
    {
        public class INDUSTRIES
        {
            [Key]
            public int Industries_ID { get; set; }
            public string Industries_Name { get; set; }
            public string Industries_Description { get; set; }
        }
    }
}
