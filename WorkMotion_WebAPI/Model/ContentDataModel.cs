using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class ContentDataModel
    {
        public class ContentData
        {
            [Key]
            public int ID { get; set; }
            public int? FK_Menu_ID { get; set; }
            public int? FK_Content_Head_ID { get; set; }
            public string Name_Header { get; set; }
            public string Content_Body { get; set; }
            //public int? Is_Active { get; set; }
            public string Create_By { get; set; }
            public DateTime? Create_Date { get; set; }
            public string Update_By { get; set; }
            public DateTime? Update_Date { get; set; }
        }

        public class ConnectFile
        {
            public int No { get; set; }
            public string HrefLink { get; set; }
        }

        public class SubContent
        {
            public int No { get; set; }
            public List<ConnectFile> connectFile { get; set; }
            public string subHeaderName { get; set; }
            public string subContentBody { get; set; }
        }

        public class ReceiveDataModel
        {
            public int LangID { get; set; }
            public string Menu { get; set; }
            public string ContentHeader { get; set; }
            public string headerName { get; set; }
            public string ContentBody { get; set; }
            public List<SubContent> subContent { get; set; }
        }

        public class ReceiveEditDataModel
        {
            public int Id { get; set; }
            public int LangID { get; set; }
            public string Menu { get; set; }
            public string ContentHeader { get; set; }
            public string headerName { get; set; }
            public string ContentBody { get; set; }
            public List<SubContent> subContent { get; set; }
            public List<int> subConnectOld { get; set; }
        }
    }
}
