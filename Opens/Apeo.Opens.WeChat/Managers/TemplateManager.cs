using NFlex.Opens.Weixin.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Managers
{
    public class TemplateManager:ManagerBase
    {
        internal TemplateManager(string apiUrl, AccessTokenContainer tokenContainer) : base(apiUrl, tokenContainer) { }

        /// <summary>
        /// 设置所属行业
        /// </summary>
        /// <param name="primary">公众号模板消息所属主行业编号</param>
        /// <param name="secondary">公众号模板消息所属副行业编号</param>
        /// <returns></returns>
        public Result SetIndustry(IndustryType primary, IndustryType secondary)
        {
            var data = new
            {
                industry_id1 = primary,
                industry_id2 = secondary
            };

            return PostJson("/cgi-bin/template/api_set_industry", data);
        }

        /// <summary>
        /// 获取设置的行业信息
        /// </summary>
        /// <returns></returns>
        public GetIndustryResult GetIndustry()
        {
            return GetJson<GetIndustryResult>("/cgi-bin/template/get_industry");
        }

        /// <summary>
        /// 向公众号添加模板
        /// </summary>
        /// <param name="templateIdShort">模板库中模板的编号，有“TM**”和“OPENTMTM**”等形式</param>
        /// <returns></returns>
        public AddTemplateResult AddTemplate(string templateIdShort)
        {
            var data = new { template_id_short = templateIdShort };
            return PostJson<AddTemplateResult>("/cgi-bin/template/api_add_template", data);
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <returns></returns>
        public GetAllTemplateResult GetAllTemplate()
        {
            return GetJson<GetAllTemplateResult>("/cgi-bin/template/get_all_private_template");
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public Result DeleteTemplate(string templateId)
        {
            var data = new { template_id = templateId };
            return PostJson("/cgi-bin/template/del_private_template", data);
        }

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="toOpenId">接收消息用户的OpenID</param>
        /// <param name="templateId">模板编号</param>
        /// <param name="url">用户点击消息跳转的链接地址</param>
        /// <param name="values">模板参数值集合</param>
        /// <returns></returns>
        public SendTemplateMessageResult SendTemplateMessage(string toOpenId,string templateId,string url,List<ValueSet> values)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach(var val in values)
            {
                dic.Add(val.key, new { value = val.value, color = val.color });
            }
            var data = new
            {
                touser = toOpenId,
                template_id = templateId,
                url = url,
                data = dic
            };

            return PostJson<SendTemplateMessageResult>("/cgi-bin/message/template/send", data);
        }

        public class ValueSet
        {
            /// <summary>
            /// 参数名
            /// </summary>
            public string key { get; set; }
            /// <summary>
            /// 参数值
            /// </summary>
            public string value { get; set; }
            /// <summary>
            /// 颜色
            /// </summary>
            public string color { get; set; }
        }
    }
}
