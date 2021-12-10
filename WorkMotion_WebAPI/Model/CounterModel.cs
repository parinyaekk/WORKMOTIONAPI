using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class CounterModel
    {
        public class Counters
        {
            public int ID { get; set; }
            public int? Counter { get; set; }
        }
    }
}
