using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Customer_RenewModel
    {
        public class Customer_Renew
        {
            [Key]
            public int ID { get; set; }
            public string Renew_Number { get; set; }
            public int? Renew_Type { get; set; }
            public int? FK_Customer_ID { get; set; }
            public int? FK_Renew_ID { get; set; }
            public string Renew_Desc { get; set; }
            public string Renew_Center { get; set; }
            public DateTime? Renew_Date { get; set; }
            public string Service_Form { get; set; }
            public string Renew_Receipt { get; set; }
            public string Customer_Signature { get; set; }
            public string Employee_Signature { get; set; }
            public int? Renew_Status { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }

        public class Request_Renew
        {
            public int? ID { get; set; }
            public int? Customer_ID { get; set; }
            public int? Renew_Type { get; set; }
            public string Renew_Center { get; set; }
            public string Create_By { get; set; }
        }

        public class RenewPaginationModel
        {
            public int? PerPage { get; set; }
            public int? Page { get; set; }
            public string Search { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public int? ID { get; set; }
            public string CareArea { get; set; }
            public int? CustomerType { get; set; }
            public int? Status { get; set; }
        }

        public class RenewExport
        {
            public int ลำดับ { get; set; }
            public string วันเดือนปีที่สมัคร { get; set; }
            public string วันเดือนปีที่ต่ออายุ { get; set; }
            public string ประเภทสมาชิก { get; set; }
            public string รหัสลูกค้า { get; set; }
            public string ชื่อที่จะระบุสมาชิกหรือผู้ติดต่อ_กรณีบุคคล { get; set; }
            public string ชื่อที่จะระบุสมาชิก_กรณีบริษัทโรงแรมสำนักงานฯลฯ { get; set; }
            public string มือถือ { get; set; }
            public string ที่อยู่ที่ติดตั้งสินค้า { get; set; }
            public string เขต_อำเภอ { get; set; }
            public string ตำบล_แขวง { get; set; }
            public string จังหวัด { get; set; }
            public string รหัสไปรษณีย์ { get; set; }
            public string ศูนย์บริการสาขา { get; set; }
            public string แผนที่ { get; set; }
            public string สถานะ { get; set; }
            public string สถานะลูกค้า { get; set; }
            public string วันที่ดำเนินการ { get; set; }
            public string เจ้าหน้าที่ { get; set; }
        }
    }
}
