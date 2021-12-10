using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Product_Installation_ModelModel
    {
        public class Product_Installation_Model
        {
            [Key]
            public int ID { get; set; }
            public int? Lang_ID { get; set; }
            public int? FK_Type_ID { get; set; }
            public string Model_Name { get; set; }
            public string Cover_File { get; set; }
            public string Cover_Href { get; set; }
            public int? Is_Active { get; set; }
            public int? Model_Order { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }

        public class InputModelInstallation
        {
            public int? ID { get; set; }
            public int? Lang_ID { get; set; }
            public string Model_Name { get; set; }
            public int? Order { get; set; }
            public string PathFile { get; set; }
            public string Link { get; set; }
            public int? Is_Active { get; set; }
        }
    }
}
