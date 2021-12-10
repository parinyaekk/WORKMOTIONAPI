using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Product_InstallationModel
    {
        public class Product_Installation
        {
            [Key]
            public int ID { get; set; } //
            public int Lang_ID { get; set; } //
            public int? FK_Model_ID { get; set; } //
            public int? FK_Classified_ID { get; set; } //
            public int? FK_Classified_ID2 { get; set; } //
            public string Product_Old_Code { get; set; } //
            public string Product_Name { get; set; } //
            public int? Is_Active { get; set; } //
            public string Create_By { get; set; } //
            public DateTime? Create_Date { get; set; } //
            public string Update_By { get; set; } //
            public DateTime? Update_Date { get; set; } //
        }

        public class GetAllDataInstallationModel
        {
            public int Page { get; set; }
            public int Perpage { get; set; }
            public string SearchValue { get; set; }
        }

        public class AddDataInstallationModel
        {
            public int? Lang_ID { get; set; }
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public int? Classified { get; set; }
            public int? Model { get; set; }
            public int? Active { get; set; }
        }

        public class InstallationOrderByModel
        {
            public string Field { get; set; }
            public string Order { get; set; }
        }

        public class InstallationPaginationModel
        {
            public int? PerPage { get; set; }
            public int? Page { get; set; }
            public string Search { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public InstallationOrderByModel Order { get; set; }
            public int? ID { get; set; }
        }
    }
}
