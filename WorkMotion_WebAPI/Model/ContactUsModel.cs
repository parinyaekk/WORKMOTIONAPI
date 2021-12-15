using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class ContactUsModel
    {
        public class CONTACT_US
        {
            [Key]
            public int ContactUs_ID { get; set; }
            public string ContactUs_Address { get; set; }
            public string ContactUs_Email { get; set; }
            public string ContactUs_Phone { get; set; }
            public decimal? ContactUs_Latitude { get; set; }
            public decimal? ContactUs_Longitude { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
        }

        public class Request_ContactUs
        {
            public int? ContactUs_ID { get; set; }
            public string ContactUs_Address { get; set; }
            public string ContactUs_Email { get; set; }
            public string ContactUs_Phone { get; set; }
            public decimal? ContactUs_Latitude { get; set; }
            public decimal? ContactUs_Longitude { get; set; }
            public string CreateBy { get; set; }
        }
    }
}
