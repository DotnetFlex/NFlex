using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetMenuResult:Result
    {
        public Menu menu { get; set; }
        public ConditionalMenu conditionalmenu { get; set; }

        public class Menu
        {
            public List<Button> button { get; set; }
            public int menuid { get; set; }
        }

        public class ConditionalMenu:Menu
        {
            public Matchrule matchrule { get; set; }
        }
    }
}
