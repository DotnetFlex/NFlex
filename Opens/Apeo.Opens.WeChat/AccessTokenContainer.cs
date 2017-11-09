using NFlex.Opens.Weixin.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin
{
    /// <summary>
    /// 微信 AccessToken 容器
    /// </summary>
    public abstract class AccessTokenContainer
    {
        #region 获取AccessToken
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="apiUrl">微信API地址</param>
        /// <param name="appId">AppID</param>
        /// <param name="appSecret">AppSecret</param>
        public static GetAccessTokenResult GetAccessToken(string apiUrl, string appId, string appSecret)
        {
            int tryCount = 0;
            while (true)
            {
                try
                {
                    var result = new HttpClient()
                        .AddQuery("grant_type", "client_credential")
                        .AddQuery("appid", appId)
                        .AddQuery("secret", appSecret)
                        .Get("/cgi-bin/token")
                        .JsonTo<GetAccessTokenResult>();
                    return result;
                }
                catch (Exception ex)
                {
                    tryCount++;
                    if (tryCount >= 5)
                        throw ex;
                    Thread.Sleep(1000);
                }
            }
        }
        #endregion

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        public abstract string GetToken();
    }
}
