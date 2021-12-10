using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static WorkMotion_WebAPI.Model.Product_PictureModel;

namespace WorkMotion_WebAPI.Model
{
    public class SparepartModel
    {
        public class Sparepart
        {
            [Key]
            public int ID { get; set; }
            public int? Lang_ID { get; set; }
            public int? FK_Product_ID { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public double? Price { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }

        public class RequiredID
        {
            public int? ID { get; set; }
            public int? Lang_ID { get; set; }
            public string Product_Old_Code { get; set; }
        }
        public class ResponseFile
        {
            public int? spare_id { get; set; }
            public int? install_id { get; set; }
            public string path { get; set; }
            public string type { get; set; }
            public string name { get; set; }
            public string link { get; set; }
            public string coverimage_path { get; set; }
        }

        public class RequiredSaveSparepart
        {
            public int? LangID { get; set; }
            public string ProductName { get; set; }
            public string Product_Old_Code { get; set; }
            public string Old_Product_Old_Code { get; set; }
            public int? SpareModel { get; set; }
            public int? SpareSubType { get; set; }
            public int? SpareType { get; set; }
            public int? InstallationModel { get; set; }
            public int? InstallationSubType { get; set; }
            public int? InstallationType { get; set; }
            public List<ResponseFile> File { get; set; }
            public List<ArrSparepart> ArrSparepart { get; set; }
            public List<ArrInstallation> ArrInstallation { get; set; }
            public string Action { get; set; }
            public int Status { get; set; }
            public int? AddType { get; set; }
        }
        public class ArrInstallation
        {
            public string Installation_id { get; set; }
            public string InstallationType { get; set; }
            public string InstallationHref { get; set; }
        }

        public class ArrSparepart
        {
            public string Sparepart_id { get; set; }
            public bool HaveFile { get; set; }
            public string SpareType { get; set; }
            public string SpareCode { get; set; }
            public string SpareHref { get; set; }
            public string SpareName { get; set; }
            public string SparePrice { get; set; }
        }


        public class InputModelSparepart
        {
            public int? ID { get; set; }
            public int? Lang_ID { get; set; }
            public string Model_Name { get; set; }
            public int? Order { get; set; }
            public string PathFile { get; set; }
            public string Link { get; set; }
            public int? Is_Active { get; set; }
        }

        public class SparepartOrderByModel
        {
            public string Field { get; set; }
            public string Order { get; set; }
        }

        public class SparepartPaginationModel
        {
            public int? PerPage { get; set; }
            public int? Page { get; set; }
            public string Search { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public SparepartOrderByModel Order { get; set; }
            public int? ID { get; set; }
        }

        public class SparepartInstallationExport
        {
            public int ลำดับ { get; set; }
            public string model { get; set; }
            public string classified { get; set; }
            public string subclassified { get; set; }
            public string รหัสสินค้า { get; set; }
            public string ชื่อสินค้า { get; set; }
            public string สถานะ { get; set; }
            public string Sparepart_Code { get; set; }
            public string Sparepart_Name { get; set; }
            public string Sparepart_Price { get; set; }
        }
    }
}
