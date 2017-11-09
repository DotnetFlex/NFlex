using System;

namespace NFlex.Opens.Weixin
{
    /// <summary>
    /// 默认微信AccessToken管理容器
    /// </summary>
    public sealed class DefaultTokenContainer : AccessTokenContainer
    {
        private int _refreshMinutes = 60;   //AccessToken刷新时时间（分钟）
        private string _accessToken;        //AccessToken值
        private DateTime _expires;          //AccessToken有效截止日期

        string _apiUrl;
        string _appId;
        string _appSecret;
        
        public DefaultTokenContainer(string apiUrl,string appId,string appSecret) : base() {
            _apiUrl = apiUrl;
            _appId = appId;
            _appSecret = appSecret;
        }

        public override string GetToken()
        {
            if (!string.IsNullOrEmpty(_accessToken) && (_expires - DateTime.Now).TotalMinutes > _refreshMinutes)
                return _accessToken;
            else
            {
                var result = GetAccessToken(_apiUrl, _appId, _appSecret);
                if (result == null) return "";
                _accessToken = result.access_token;
                _expires = DateTime.Now.AddSeconds(result.expires_in);
                return _accessToken;
            }
        }
    }
}
