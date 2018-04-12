using NFlex.Opens.Taobao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.WechatManagement
{
    public class AlimamaClientFactory
    {
        private static AlimamaClient _instance;

        public static AlimamaClient Instance
        {
            get
            {
                if(_instance==null)
                {
                    _instance = new AlimamaClient();
                    _instance.Login("lingcnet", "ljzlingc.223");
                }
                return _instance;
            }
        }
    }
}