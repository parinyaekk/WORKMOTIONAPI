using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Menu_LinkMenu
    {
        public class Menu_Link
        {
            public int ID { get; set; }
            public int? FK_MENU_TH_ID { get; set; }
            public int? FK_MENU_EN_ID { get; set; }
        }
    }
}
