using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class PortfolioModel
    {
        public class PORTFOLIO
        {
            [Key]
            public int Portfolio_ID { get; set; }
            public int? FK_Industries_ID { get; set; }
            public int Portfolio_Section { get; set; }
            public string Portfolio_Name { get; set; }
            public string Portfolio_Logo_Path { get; set; }
            public string Portfolio_About { get; set; }
            public string Portfolio_Technology { get; set; }
            public string Portfolio_Location { get; set; }
            public string Portfolio_Contact_Website { get; set; }
            public string Portfolio_Contact_LinkedIn { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
        }

        public class Request_Portfolio
        {
            public int? Portfolio_ID { get; set; }
            public int? FK_Industries_ID { get; set; }
            public int Portfolio_Section { get; set; }
            public string Portfolio_Name { get; set; }
            public string Portfolio_Logo_Path { get; set; }
            public string Portfolio_About { get; set; }
            public string Portfolio_Technology { get; set; }
            public string Portfolio_Location { get; set; }
            public string Portfolio_Contact_Website { get; set; }
            public string Portfolio_Contact_LinkedIn { get; set; }
            public string CreateBy { get; set; }
        }
    }
}
