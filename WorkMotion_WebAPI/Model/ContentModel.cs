using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class ContentModel
    {
        public class Content
        {
            [Key]
            public int ID { get; set; }
            public int? Lang_ID { get; set; }
            public int? Line_Status { get; set; }
            public int? FK_Menu_ID { get; set; }
            public string Content_Title { get; set; }
            public string Content_Desc { get; set; }
            public int? Content_Type { get; set; }
            public string Content_Body { get; set; }
            public int? Content_Order { get; set; }
            public string Content_Col1 { get; set; }
            public string Content_Col2 { get; set; }
            public string Content_Col3 { get; set; }
            public string Content_Col4 { get; set; }
            public string Content_Col5 { get; set; }
            public string Content_Col6 { get; set; }
            public int? Content_Col { get; set; }
            public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }

        public class InputModelContent
        {
            public int? Lang_ID { get; set; }
        }

        public class Input_File_Content
        {
            public int id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
        }

        public class InputContent
        {
            public int LangID { get; set; }
            public string Menu { get; set; }
            public int? Line_Status { get; set; }
            public int? Content_ID { get; set; }
            public string Content_Title { get; set; }
            public string Content_Desc { get; set; }
            public int? Content_Type { get; set; }
            public int? Content_Order { get; set; }
            public int? Content_Col { get; set; }
            public string Content_Col1 { get; set; }
            public string Content_Col2 { get; set; }
            public string Content_Col3 { get; set; }
            public string Content_Col4 { get; set; }
            public string Content_Col5 { get; set; }
            public string Content_Col6 { get; set; }
            public object Content_Body { get; set; }
            public List<Input_File_Content> File { get; set; }
        }

        public class GetDataContentModel
        {
            public int? LangID { get; set; }
            public int? ID { get; set; }
        }

        public class ContentOrderByModel
        {
            public string Field { get; set; }
            public string Order { get; set; }
        }

        public class ContentPaginationModel
        {
            public int? PerPage { get; set; }
            public int? Page { get; set; }
            public string Search { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public ContentOrderByModel Order { get; set; }
            public int? ID { get; set; }
        }
    }
}
