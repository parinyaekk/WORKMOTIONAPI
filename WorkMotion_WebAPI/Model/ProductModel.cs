using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WorkMotion_WebAPI.Model
{
    public class ProductModel
    {
        public class Product
        {
            [Key]
            public int ID { get; set; }
            public int? Lang_ID { get; set; }
            public int? FK_Model_ID { get; set; }
            public int? FK_Type_ID { get; set; }
            public string Product_Code { get; set; }
            public string Product_Old_Code { get; set; }
            public string Product_Barcode { get; set; }
            public string Product_Name { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }

        public class RequiredProductName
        {
            public int? Lang_ID { get; set; }
            public string Product_Name { get; set; }
            public int? Product_TypeID { get; set; }
        }

        public class RequiredProductCode
        {
            public int? Lang_ID { get; set; }
            public string Product_Code { get; set; }
            public int? Product_TypeID { get; set; }
        }

        public class RequiredProductBarcode
        {
            public int? Lang_ID { get; set; }
            public string Product_Barcode { get; set; }
            public int? Product_TypeID { get; set; }
        }

        public class RequiredLanguageID
        {
            public int? Lang_ID { get; set; }
        }

        public class RequiredStoreName
        {
            public int? Lang_ID { get; set; }
            public int? Province_ID { get; set; }
            public string Store_Name { get; set; }
        }

        public class GetDataProductModel
        {
            public string Search { get; set; }
            public int Page { get; set; }
            public int Perpage { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
        }

        public class AddDataMenuModel
        {
            public int? ID { get; set; }
            public int? Lang_ID { get; set; }
            public string MenuName { get; set; }
            public int? MainMenuName { get; set; }
            public int? OrderMenu { get; set; }
            public string LinkMenu { get; set; }
            public string DescriptionMenu { get; set; }
            public string Status { get; set; }
            public int? Active { get; set; }
            public int? HeaderStatus { get; set; }
            public int? FooterStatus { get; set; }
            public int? Link_Menu { get; set; }
        }
        
        public class SearchDataProductModel
        {
            public int? Lang_ID { get; set; }
            public string Product_Name { get; set; }
        }

        public class WarrantyProductExport
        {
            public int ลำดับ { get; set; }
            public string รหัสสินค้า { get; set; }
            public string รหัสสินค้าเก่า { get; set; }
            public string บาร์โค้ด { get; set; }
            public string ชื่อสินค้า { get; set; }
            public string ประเภทสินค้า { get; set; }
            public string สถานะ { get; set; }
        }

    }
}
