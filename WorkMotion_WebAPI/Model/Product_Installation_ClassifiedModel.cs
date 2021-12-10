using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Product_Installation_ClassifiedModel
    {
        public class Product_Installation_Classified
        {
            [Key]
            public int ID { get; set; }
            public int? Lang_ID { get; set; }
            public int? FK_Model_ID { get; set; }
            public int? FK_Classified_ID { get; set; }
            public string Classified_Name { get; set; }
            public int? Classified_Order { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }

        public class InputClassifiedInstallation
        {
            public int? ID { get; set; }
            public int? Lang_ID { get; set; }
            public int? FK_Model_ID { get; set; }
            public int? FK_Classified_ID { get; set; }
            public string Classified_Name { get; set; }
            public int? Order { get; set; }
            public int? Is_Active { get; set; }
        }
    }
}
