using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WorkMotion_WebAPI.Model
{
    public class WarrantyModel
    {
        public class Warranty
        {
            [Key]
            public int ID { get; set; }
            public int? FK_Customer_ID { get; set; }
            public int? FK_Province_ID { get; set; }
            public int? FK_Product_ID { get; set; }
            public int? FK_Store_ID { get; set; }
            public DateTime? Warranty_Date { get; set; }
            public string Store_Other_Name { get; set; }
            public string Receipt_Number { get; set; }
            public string Barcode_No { get; set; }
            public string Warranty_No { get; set; }
            public string Product_Code_Other { get; set; }
            public int? Product_QTY { get; set; }
            //public string Image_Path { get; set; }
            //public string Image { get; set; }
            public int? Score { get; set; }
            public string Description { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
            public string Remark { get; set; }
        }

        public class AddWarrantyModel
        {
            public string Customer_Code { get; set; }
            public string Customer_Firstname { get; set; }
            public string Customer_Lastname { get; set; }
            public string Customer_Tel { get; set; }
            public string Customer_Mobile { get; set; }
            public string Customer_Email { get; set; }
            public string Customer_Address { get; set; }
            public int? Customer_Province { get; set; }
            public int? Customer_District { get; set; }
            public int? Customer_SubDistrict { get; set; }
            public string Customer_ZipCode { get; set; }
            public string Customer_Latitude { get; set; }
            public string Customer_Longitude { get; set; }
            public int? Purchase_Province { get; set; }
            public DateTime? Purchase_Date { get; set; }
            public int? Store_ID { get; set; }
            public string Store_Name_Other { get; set; }
            public string Receipt_Number { get; set; }
            public string Barcode_Number { get; set; }
            public string Warranty_Number { get; set; }
            public int? Type_ID { get; set; }
            public int? Product_ID { get; set; }
            public int? Model_ID { get; set; }
            public string Product_Code_Other { get; set; }
            public int? QTY { get; set; } 
            public int? Score { get; set; }
            public string Description { get; set; }
            public HttpRequest File { get; set; }
        }

        public class DataWarranty
        {
            public int ID { get; set; }
            public string warranty_Date_Format { get; set; }
            public DateTime? Warranty_Date { get; set; }
            public string Product_Type_Name { get; set; }
            //public string Image_Path { get; set; }
            //public string Image { get; set; }
            public string Product_Name { get; set; }
            public string ProductCode { get; set; }
            public string Model_Name { get; set; }
            public string StoreName { get; set; }
            public string Province { get; set; }
            public string Customer_Address { get; set; }
            public string Customer_Name { get; set; }
            public string Customer_Surname { get; set; }
            public string Customer_ZipCode { get; set; }
            public string Customer_Tel { get; set; }
            public string Customer_Mobile { get; set; }
            public string Customer_Email { get; set; }
            public string Customer_Type { get; set; }
            public string Customer_Code { get; set; }
            public string ServiceCenter { get; set; }
            public string Quota { get; set; }
        }

        public class UpdateDataWarranty
        {
            public int ID { get; set; }
            public string CustomerCode { get; set; }
            public string CustomerAddress { get; set; }
            public string CustomerEmail { get; set; }
            public string CustomerMobile { get; set; }
            public string CustomerName { get; set; }
            public string CustomerSurname { get; set; }
            public string CustomerTel { get; set; }
            public string CustomerType { get; set; }
            public string CustomerZipCode { get; set; }
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public string ProductTypeName { get; set; }
            public int? Province { get; set; }
            public string Quota { get; set; }
            public string ServiceCenter { get; set; }
            public string StoreName { get; set; }
            public string ImagePath { get; set; }
        }

        public class RequestAddDataWarranty
        {
            public int? Purchase_Province { get; set; }
            public DateTime? Purchase_Date { get; set; }
            public int? Store_ID { get; set; }
            public string Store_Name_Other { get; set; }
            public string Receipt_Number { get; set; }
            public string Barcode_Number { get; set; }
            public string Warranty_Number { get; set; }
            public int? Type_ID { get; set; }
            public int? Product_ID { get; set; }
            public int? Model_ID { get; set; }
            public string Product_Code_Other { get; set; }
            public int? QTY { get; set; }
            public string Product_code { get; set; }
            public string product_Name { get; set; }
            public int? Lang_ID { get; set; }
            public string Customer_Code { get; set; }
            public string Customer_Firstname { get; set; }
            public string Customer_Lastname { get; set; }
            public string Customer_Tel { get; set; }
            public string Customer_Mobile { get; set; }
            public string Customer_Email { get; set; }
            public string Customer_Address { get; set; }
            public int? Customer_Province { get; set; }
            public int? Customer_District { get; set; }
            public int? Customer_SubDistrict { get; set; }
            public string Customer_ZipCode { get; set; }
            public string Customer_Latitude { get; set; }
            public string Customer_Longtitude { get; set; }
            public int? Score { get; set; }
            public string Description { get; set; }
            public string Service_Center { get; set; }
            public string Service_Center_Name { get; set; }
            public List<int?> Seq { get; set; }
        }

        public class DataSendMail
        {
            public string Customer_Email { get; set; }
            public string Customer_Name { get; set; }
            public string Customer_Surname { get; set; }
            public List<MailReceipt> MailReceiptList { get; set; }
        }

        public class MailReceipt
        {
            public string Receipt_Number { get; set; }
            public List<MailProduct> MailProductList { get; set; }
        }

        public class MailProduct
        {
            public string Barcode_No { get; set; }
            public string Warranty_No { get; set; }
            public string Product_Code_Other { get; set; }
            public string Product_Code { get; set; }
            public string Product_Name { get; set; }
            public string Product_QTY { get; set; }
            public List<int?> ID_File { get; set; }
        }

        public class WarrantyPaginationModel
        {
            public int? PerPage { get; set; }
            public int? Page { get; set; }
            public string Search { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public int? ProductTypeSearch { get; set; }
            public int? ID { get; set; }
            public string CareArea { get; set; }
            public string SearchTypeName { get; set; }
        }


        public class WarrantyExport
        {
            public int ลำดับ { get; set; }
            public DateTime? วันเดือนปีที่ลงทะเบียน { get; set; }
            public string ชื่อลูกค้า { get; set; }
            public string นามสกุล { get; set; }
            public string เบอร์โทรศัพท์ { get; set; }
            public string มือถือ { get; set; }
            public string อีเมล { get; set; }
            public string ที่อยู่ที่ติดตั้งสินค้า { get; set; }
            public string อำเภอ { get; set; }
            public string ตำบล { get; set; }
            public string จังหวัด { get; set; }
            public string รหัสไปรษณีย์ { get; set; }
            public string ศูนย์บริการสาขา { get; set; }
            public DateTime? วันเดือนปีที่ซื้อ { get; set; }
            public string ชื่อร้านตัวแทนจำหน่าย { get; set; }
            public string ชื่อร้านตัวแทนจำหน่าย_กรณีค้นหาไม่พบ { get; set; }
            public string หมายเลขใบเสร็จ { get; set; }
            public string หมายเลขรับประกัน { get; set; }
            public string ประเภทสินค้า { get; set; }
            public string รหัสสินค้า { get; set; }
            public string ชื่อสินค้า { get; set; }
            public string รหัสสินค้ากรณีค้นหาไม่พบ { get; set; }
            public string จำนวนชิ้นที่ซื้อ { get; set; }
            public string แผนที่ { get; set; }
            public string สถานะ { get; set; }
            public string หลักฐานภาพใบเสร็จ_มีหรือไม่มี { get; set; }
            public string ระดับความพึงพอใจต่อสินค้าอเมริกันสแตนดาร์ด { get; set; }
            public string ข้อเสนอแนะ { get; set; }
            public string หมายเหตุ { get; set; }
        }

        public class WarrantyExportForm2
        {
            public string MONTH_YEAR { get; set; }
            public int? ลูกค้า_BKK { get; set; }
            public int? รายการ_BKK { get; set; }
            public int? ลูกค้า_CM { get; set; }
            public int? รายการ_CM { get; set; }
            public int? ลูกค้า_PKT { get; set; }
            public int? รายการ_PKT { get; set; }
            public int? ลูกค้า_PY { get; set; }
            public int? รายการ_PY { get; set; }
            public int? ลูกค้า_All_CCC { get; set; }
            public int? รายการ_All_CCC { get; set; }
        }
    }
}
