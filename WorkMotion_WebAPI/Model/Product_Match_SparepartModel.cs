using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Product_Match_SparepartModel
    {
        public class Product_Match_Sparepart
        {
            [Key]
            public int ID { get; set; }
            public int Product_ID { get; set; }
            public int Sparepart_ID { get; set; }
            public int? Is_Active { get; set; } 
            public string Create_By { get; set; } 
            public DateTime? Create_Date { get; set; } 
            public string Update_By { get; set; } 
            public DateTime? Update_Date { get; set; } 
        }
    }
}
