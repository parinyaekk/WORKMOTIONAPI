using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class TeamModel
    {
        public class TEAM
        {
            [Key]
            public int Team_ID { get; set; }
            public string Team_Name { get; set; }
            public string Team_Image_Path { get; set; }
            public string Team_Position { get; set; }
            public string Team_Personal_Story { get; set; }
            public string Team_Education { get; set; }
            public string Team_Interest { get; set; }
            public string Team_Contact_Channels { get; set; }
            public bool? ActiveFlag { get; set; }
            public string CreateBy { get; set; }
            public DateTime? CreateDate { get; set; }
            public string UpdateBy { get; set; }
            public DateTime? UpdateDate { get; set; }
        }

        public class Request_Team
        {
            public int? Team_ID { get; set; }
            public string Team_Name { get; set; }
            public string Team_Image_Path { get; set; }
            public string Team_Position { get; set; }
            public string Team_Personal_Story { get; set; }
            public string Team_Education { get; set; }
            public string Team_Interest { get; set; }
            public string Team_Contact_Channels { get; set; }
        }
    }
}
