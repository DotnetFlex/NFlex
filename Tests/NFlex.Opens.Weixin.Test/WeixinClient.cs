using System;
using System.Collections.Generic;

namespace NFlex.Opens.Weixin.Test
{
    public class WeixinClient
    {

        private static WxClient _client;

        public static WxClient Instance
        {
            get
            {
                var accountList = new List<AccountInfo>
                {
                    new AccountInfo("wx27d1fff72840c691","8a83383d96abdfb811525c99a220bd4a"),
                    //Test
                    new AccountInfo("wx76a6328ff702de4e","004a2eaffd4babb1da8ed20931c9f28a")
                };
                var account = accountList[1];

                if (_client == null)
                    _client = new WxClient("https://api.weixin.qq.com", new TokenContainer());//new DefaultTokenContainer("https://api.weixin.qq.com", account.OpenId, account.Secret));
                return _client;
            }
        }

        public class AccountInfo
        {
            public string OpenId { get; set; }
            public string Secret { get; set; }

            public AccountInfo(string id,string sec)
            {
                OpenId = id;
                Secret = sec;
            }
        }

        public class TokenContainer : AccessTokenContainer
        {
            public override string GetToken()
            {
                return "7_vwttESBcR7gsZFJoSDoQt92Nvt7F1bKwtywmClz5JIvqd4PzDcrQYm60xl2Al_KgFlTs5-fkGG9V_7dgyGR2xnStU5eAL2XQknlxasVKRCaFU3pzGEoD0I33mgo8unRuLAf7m-T3OrSMXH-dJEAeAFAZUQ";
            }
        }
    }
}
