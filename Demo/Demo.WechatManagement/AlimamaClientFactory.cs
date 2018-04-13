using NFlex;
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
                    var config = Config.LoadConfig<AlimamaConfig>("Alimama.config");
                    _instance = new AlimamaClient();
                    _instance.Login(config.UserName, config.Password);
                }
                return _instance;
            }
        }
    }
}