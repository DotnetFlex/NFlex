using NFlex.Opens.Weixin.Models;
using NFlex.Opens.Weixin.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin
{
    public class Authorization
    {
        //微信授权跳转链接
        string authUrl;
        string authTokenUrl;
        string authUserInfoUrl;

        public WeixinConfig Setting { get; set; }

        internal Authorization(WeixinConfig set)
        {
            Setting = set;
            authUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect";
            authTokenUrl = Setting.ApiUrl + "/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
            authUserInfoUrl = Setting.ApiUrl + "/sns/userinfo?access_token={0}&openid={1}";
        }
        
        /// <summary>
        /// 获取授权跳转链接地址
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="redirectUrl"></param>
        /// <param name="scope"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public string GetOAuthUrl(string redirectUrl, ScopeType scope, string state)
        {
            return string.Format(authUrl, Setting.AppID, redirectUrl, scope.ToString(), state);
        }

        public GetOAuthTokenResult GetOAuthToken(string code)
        {
            string url = string.Format(authTokenUrl, Setting.AppID, Setting.AppSecret, code);
            HttpClient client = new HttpClient(url);
            client.Encoding = Encoding.UTF8;
            var info = client.Get(url).JsonTo<GetOAuthTokenResult>();
            return info;
        }

        public GetUserInfoResult GetUserInfo(string oAuthccessToken,string openId)
        {
            string url = string.Format(authUserInfoUrl, oAuthccessToken, openId);
            HttpClient client = new HttpClient(url);
            client.Encoding = Encoding.UTF8;
            var info = client.Get(url).JsonTo<GetUserInfoResult>();
            return info;
        }
    }
}
