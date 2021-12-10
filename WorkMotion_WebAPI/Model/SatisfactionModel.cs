using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class SatisfactionModel
    {
        public class RequestSatisfactionModel
        {
            public int? PerPage { get; set; }
            public int? Page { get; set; }
            public string Search { get; set; }
            public int? ID { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public string CareArea { get; set; }

        }

        public class SatisfactionWaranntyExport
        {
            public int ลำดับ { get; set; }
            public string วันเดือนปีที่ลงทะเบียน { get; set; }
            public string ชื่อ { get; set; }
            public string นามสกุล { get; set; }
            public string โทรศัพท์มือถือ { get; set; }
            public string รหัสสินค้า { get; set; }
            public string วันเดือนปีที่ซื้อ { get; set; }
            public string จังหวัดที่ซื้อ { get; set; }
            public string ชื่อร้านตัวแทนจำหน่าย { get; set; }
            public string ศูนย์บริการสาขา { get; set; }
            public string ระดับความพึงพอใจ { get; set; }
            public string ข้อเสนอแนะ { get; set; }
        }

        public class SummarySatisfactionWaranntyExport
        {
            public string Point_Evaluate { get; set; }
            public string Amount_Of_Assessment { get; set; }
        }

        public class SatisfactionServiceExport
        {
            public int ลำดับ { get; set; }
            public string วันเดือนปีที่บริการ { get; set; }
            public string รหัสลูกค้า { get; set; }
            public string ชื่อลูกค้า { get; set; }
            public string นามสกุล { get; set; }
            public string เบอร์โทร { get; set; }
            public string เจ้าหน้าที่บริการ { get; set; }
            public string ศูนย์บริการ { get; set; }
            public string ใบบริการ_ใบเสร็จหมายเลข { get; set; }
            public string ค่าบริการ { get; set; }
            public string ค่าอะไหล่ { get; set; }
            public string จำนวนครั้งบริการที่เหลือ { get; set; }
            public string ระดับความพึงพอใจ { get; set; }
            public string ข้อเสนอแนะ { get; set; }
        }
    }
}
