using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class CustomerModel
    {
        public class Customer
        {
            [Key]
            public int ID { get; set; }
            public int? Customer_Type { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }                     
            public string Customer_Code { get; set; }
            public string Customer_Name { get; set; }
            public string Customer_Surname { get; set; }
            public string Customer_Tel { get; set; }
            public string Customer_Phone { get; set; }
            public string Customer_Email { get; set; }
            public string Customer_Address { get; set; }
            public string Customer_ZIP_Code { get; set; }
            public string Customer_Latitude { get; set; }
            public string Customer_Longitude { get; set; }
            public int? FK_Province_ID { get; set; }
            public int? FK_District_ID { get; set; }
            public int? FK_Sub_District_ID { get; set; }
            public string Service_Center { get; set; }
            public string Service_Center_Name { get; set; }
            public string Service_Agent_Name { get; set; }
            public int? Quota_Service { get; set; }
            public int? Flag_Member { get; set; }
            public int? Flag_Warranty { get; set; }
            public int? Is_Active { get; set; }
            public DateTime? Register_Date { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
            public string Customer_Company { get; set; }
            public int? Lang_ID { get; set; }
        }

        public class AddCustomer
        {
            public int? Customer_Type { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Member_Code { get; set; }
            public string Tel { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string ZIP_Code { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public int? FK_Province_ID { get; set; }
            public int? FK_District_ID { get; set; }
            public int? FK_Sub_District_ID { get; set; }
            public string Service_Center { get; set; }
            public int? Quota_Service { get; set; }
            public bool? IsMember { get; set; }
            public string Create_By { get; set; }
            public string Customer_Company { get; set; }
            public int? Lang_ID { get; set; }
        }

        public class UpdateCustomer
        {
            public int? ID { get; set; }
            public int? Customer_Type { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Member_Code { get; set; }
            public string Tel { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string ZIP_Code { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public int? FK_Province_ID { get; set; }
            public int? FK_District_ID { get; set; }
            public int? FK_Sub_District_ID { get; set; }
            public string Service_Center { get; set; }
            public int? Quota_Service { get; set; }
            public bool? IsMember { get; set; }
            public bool? IsActive { get; set; }
            public string Update_By { get; set; }
            public string Customer_Company { get; set; }
        }

        public class RequestID
        {
            public int ID { get; set; }
        }

        public class RequestProvince
        {
            public int ID { get; set; }
            public string Province { get; set; }
        }

        public class RequestDistrict
        {
            public int ID { get; set; }
            public string District { get; set; }
        }

        public class RequestSubDistrict
        {
            public int ID { get; set; }
            public string SubDistrict { get; set; }
        }

        public class CustomerOrderByModel
        {
            public string Field { get; set; }
            public string Order { get; set; }
        }

        public class CustomerPaginationModel
        {
            public int? PerPage { get; set; }
            public int? Page { get; set; }
            public string Search { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public int? Status { get; set; }
            public CustomerOrderByModel Order { get; set; }
            public int? ID { get; set; }
            public string CareArea { get; set; }
            public int? CustomerType { get; set; }
        }

        public class RequestRenewPassword
        {
            public int? ID { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
            public string ConfirmPassword { get; set; }
        }

        public class RegisterExport
        {
            public int ลำดับ { get; set; }
            public string วันเดือนปีที่ลงทะเบียน { get; set; }
            public string ประเภทสมาชิก { get; set; }
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
        }
        public class IgnoreExport
        {
            public int ลำดับ { get; set; }
            public string วันเดือนปีที่ลงทะเบียน { get; set; }
            public string ประเภทสมาชิก { get; set; }
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
            public string คำอธิบาย { get; set; }
            public string วันที่ดำเนินการ { get; set; }
        }
        public class MemberShipExport
        {
            public int ลำดับ { get; set; }
            public string วันเดือนปีที่สมัคร { get; set; }
            public string ประเภทสมาชิก { get; set; }
            public string รหัสลูกค้า { get; set; }
            public string จำนวนครั้งการบริการที่เหลือ_ระยะเวลาที่เหลือก่อนหมดอายุสมาชิก { get; set; }
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
        }

    }
}