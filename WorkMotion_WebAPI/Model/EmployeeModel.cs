using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class EmployeeModel
    {
        public class Employee
        {
            [Key]
            public int ID { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Employee_Code { get; set; }
            public string Employee_Name { get; set; }
            public string Employee_Surname { get; set; }
            public string Employee_Tel { get; set; }
            public string Employee_Phone { get; set; }
            public string Employee_Email { get; set; }
            public string Employee_Address { get; set; }
            public string Employee_ZIP_Code { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
            public int? FK_UserGroup_ID { get; set; }
            public int? FK_Manager_ID { get; set; }
            public int? ServiceCenter { get; set; }
        }

        public class UpdateEmployee
        {
            public int ID { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Employee_Code { get; set; }
            public string Employee_Name { get; set; }
            public string Employee_Surname { get; set; }
            public string Employee_Tel { get; set; }
            public string Employee_Phone { get; set; }
            public string Employee_Email { get; set; }
            public string Employee_Address { get; set; }
            public string Employee_ZIP_Code { get; set; }
            public int? ServiceCenter { get; set; }
            public int? Is_Active { get; set; }
            public int? UserGroup { get; set; }
        }

        public class EmployeePaginationModel
        {
            public int? PerPage { get; set; }
            public int? Page { get; set; }
            public string Search { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public int? ID { get; set; }
        }
        public class EmployeeExport
        {
            public int ลำดับ { get; set; }
            public string UserID { get; set; }
            public string ชื่อ { get; set; }
            public string โทรศัพท์ { get; set; }
            public string โทรศัพท์มือถือ { get; set; }
            public string อีเมล { get; set; }
            public string ศูนย์บริการ { get; set; }
            public string สถานะ { get; set; }
        }
    }
}
