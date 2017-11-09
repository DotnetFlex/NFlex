using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetCurrentSelfMenuResult:Result
    {
        public int is_menu_open { get; set; }
        public SelfMenuInfo selfmenu_info { get; set; }

        public class SelfMenuInfo
        {
            public List<Button> button { get; set; }
        }
    }
}
