using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class InformationModel
    {
        public class INFORMATION
        {
            [Key]
            public int Information_ID { get; set; }
            public int? FK_Startup_Option_ID { get; set; }
            public int? FK_Industries_ID { get; set; }
            public int? FK_Categories_ID { get; set; }
            public int? FK_HDYH_Option_ID { get; set; }
            public int? Information_Country_ID { get; set; }
            public string Information_Startup_Option_Text { get; set; }
            public string Information_Industries_Text { get; set; }
            public string Information_Categories_Text { get; set; }
            public string Information_HDYH_Text { get; set; }
            public string Information_HDYH_Other { get; set; }
            public string Information_Company_Name { get; set; }
            public string Information_Email { get; set; }
            public string Information_Country_Name { get; set; }
            public string Information_Phone_Number { get; set; }
            public string Information_Profile { get; set; }
            public string Information_Detail { get; set; }
            public string Information_Looking_For { get; set; }
            public string Information_Looking_For_Other { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
        }
        public class Request_Information
        {
            public int? Information_ID { get; set; }
            public int? FK_Startup_Option_ID { get; set; }
            public int? FK_Industries_ID { get; set; }
            public int? FK_Categories_ID { get; set; }
            public int? FK_HDYH_Option_ID { get; set; }
            public int? Information_Country_ID { get; set; }
            public string Information_Startup_Option_Text { get; set; }
            public string Information_Industries_Text { get; set; }
            public string Information_Categories_Text { get; set; }
            public string Information_HDYH_Text { get; set; }
            public string Information_HDYH_Other { get; set; }
            public string Information_Company_Name { get; set; }
            public string Information_Email { get; set; }
            public string Information_Country_Name { get; set; }
            public string Information_Phone_Number { get; set; }
            public string Information_Profile { get; set; }
            public string Information_Detail { get; set; }
            public string Information_Looking_For { get; set; }
            public string Information_Looking_For_Other { get; set; }
        }
        public class OldFile
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public class Export_INFORMATION
        {
            public int? SEQ { get; set; }
            public string Information_Startup_Option_Text { get; set; }
            public string Information_Industries_Text { get; set; }
            public string Information_Categories_Text { get; set; }
            public string Information_HDYH_Text { get; set; }
            public string Information_HDYH_Other { get; set; }
            public string Information_Company_Name { get; set; }
            public string Information_Email { get; set; }
            public string Information_Country_Name { get; set; }
            public string Information_Phone_Number { get; set; }
            public string Information_Profile { get; set; }
            public string Information_Detail { get; set; }
            public string Information_Looking_For { get; set; }
            public string Information_Looking_For_Other { get; set; }
        }

    }
}
