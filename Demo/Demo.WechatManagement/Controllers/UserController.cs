using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFlex;
using NFlex.Opens.Weixin;

namespace Demo.WechatManagement.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(string code,string state)
        {
            
            return View();
        }

        public ActionResult Login()
        {
            return new EmptyResult();
            //string redirect_uri = "http://ttmiao.tunnel.2bdata.com/WechatManagement/user";
            //Response.Redirect(WeixinManager.WeixinTest.Authorization.GetOAuthUrl(redirect_uri,ScopeType.snsapi_userinfo,"TestState"));
            //return new EmptyResult();
        }
    }
}











