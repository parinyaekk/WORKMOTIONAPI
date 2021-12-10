using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Customer_Renew_StatusModel
    {
        public class Customer_Renew_Status
        {
            [Key]
            public int ID { get; set; }
            public int? FK_Renew_ID { get; set; }
            public int? Status { get; set; }
            public string Decription { get; set; }
            public string Receipt_Image { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
            public string Employee_Signature_Image { get; set; }
        }

        public class Request_Customer_Renew_Status
        {
            public int? Renew_ID { get; set; }
            public int? Customer_ID { get; set; }
            public int? Status_ID { get; set; }
            public string Description { get; set; }
            public string Path_Slip { get; set; }
            public string Employee_Signature { get; set; }
            public string Create_By { get; set; }
        }
    }
}
