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
                    Log.Debug("登录阿里账号...");
                    _instance.Login(config.UserName, config.Password);
                    Log.Debug("阿里账号登录成功");
                }
                return _instance;
            }
        }
    }
}