using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class CategoriesModel
    {
        public class CATEGORIES
        {
            [Key]
            public int Categories_ID { get; set; }
            public int FK_Industries_ID { get; set; }
            public int? Group_Number { get; set; }
            public string Categories_Name { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
        }
    }
}
