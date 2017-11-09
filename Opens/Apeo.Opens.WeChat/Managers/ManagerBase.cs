using NFlex.Opens.Weixin.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Managers
{
    public abstract class ManagerBase
    {
        protected AccessTokenContainer TokenContainer { get; set; }
        protected string ApiUrl { get; set; }

        public ManagerBase(string apiUrl,AccessTokenContainer tokenContainer)
        {
            TokenContainer = tokenContainer;
            ApiUrl = apiUrl;
        }

        protected HttpClient GetClient()
        {
            return new HttpClient(ApiUrl)
                .AddQuery("access_token", TokenContainer.GetToken());
        }

        protected T PostJson<T>(string url,object json)
        {
            var result = GetClient()
                .SetJson(json)
                .Post(url)
                .JsonTo<T>();
            return result;
        }

        protected Result PostJson(string url,object json)
        {
            return PostJson<Result>(url, json);
        }

        protected T GetJson<T>(string url)
        {
            var result = GetClient()
                .Get(url)
                .JsonTo<T>();
            return result;
        }

        protected Result GetJson(string url)
        {
            return GetJson<Result>(url);
        }
    }
}
