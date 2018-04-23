using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.WechatManagement;
using NFlex;
using NFlex.Opens.Weixin;
using NFlex.Opens.Weixin.PushMessage;
using NFlex.Logging.Log4Net;

namespace Demo.WechatManagement.Controllers
{
    public class ReceivePushController : Controller
    {
        // GET: ReceivePush
        public void Index()
        {
            Logger log = new Logger();
            log.Debug(Request.Url.ToString());
            WeixinManager.Instance.Receiver.ReceiveData();
        }
    }
}