using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class ServiceInformationModel
    {
        public class ServiceInformation
        {
            [Key]
            public int ID { get; set; }
            public string Service_Number { get; set; }
            public string Customer_Name { get; set; }
            public int? Customer_Type { get; set; }
            public string Customer_Address { get; set; }
            public string Service_Center { get; set; }
            public string Service_Personnel { get; set; }
            public DateTime? ServiceDate { get; set; }
            public string ServiceSummary { get; set; }
            public string ServiceCharge { get; set; }
            public int? ServiceStatus { get; set; }
            public int? Customer_Code { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
            public string SpareCharge { get; set; }
            public string ServiceStaff { get; set; }
            public string ServiceTime { get; set; }
            public string ServiceSubject { get; set; }
            public int? ServiceCount { get; set; }
            public int? Score { get; set; }
            public string Feedback { get; set; }
            public string ServiceHistory { get; set; }
        }

        public class RequiredServiceInformation
        {
            public int? ID { get; set; }
            public string Service_Number { get; set; }
            public string Customer_Name { get; set; }
            public string Customer_ID { get; set; }
            //public int? Customer_Type { get; set; }
            public string Customer_Address { get; set; }
            public string Service_Center { get; set; }
            public string Service_Personnel { get; set; }
            public DateTime? ServiceDate { get; set; }
            public string ServiceSummary { get; set; }
            public string ServiceCharge { get; set; }
            public int? ServiceStatus { get; set; }
            public int? Customer_Code { get; set; }
            public List<int?> ServiceImage { get; set; }
            public int? MemberSignature { get; set; }
            public int? OfficerSignature { get; set; }
            public int? ReceiptImage { get; set; }
            public string SpareCharge { get; set; }
            public string ServiceTime { get; set; }
            public string ServiceSubject { get; set; }
            public string ServiceStaff { get; set; }
            public int? ServiceCount { get; set; }
            public int? ServiceStatusActive { get; set; }
            public string ServiceHistory { get; set; }
        }

        public class GetDataServiceInfoModel
        {
            public int? PerPage { get; set; }
            public int? Page { get; set; }
            public string Search { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public int? ID { get; set; }
            public string CareArea { get; set; }
            public int? CustomerType { get; set; }
        }

        public class ServiceInformationExport
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
            public string วันเดือนปีที่บริการ { get; set; }
            public string ใบบริการ_ใบเสร็จหมายเลข { get; set; }
            public string จำนวนครั้งบริการที่เหลือ { get; set; }
            public string เจ้าหน้าที่บริการ { get; set; }
            public string สถานะการบริการ { get; set; }
            public string สถานะ { get; set; }
        }

        public class GetDataInformationListModel
        {
            public int? PerPage { get; set; }
            public int? Page { get; set; }
            public string Search { get; set; }
            public int? CustomerID { get; set; }
        }

        public class AddDataSatisfactionAssessmentModel
        {
            public int? ServiceID { get; set; }
            public int? Score { get; set; }
            public string Feedback { get; set; }
        }
    }
}
