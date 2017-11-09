using NFlex.Opens.Weixin.Models;
using NFlex.Opens.Weixin.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Managers
{
    public class MenuManager:ManagerBase
    {
        internal MenuManager(string apiUrl, AccessTokenContainer tokenContainer) : base(apiUrl, tokenContainer) { }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public Result CreateMenu(params Button[] buttons)
        {
            var data = new
            {
                button = buttons
            };

            return PostJson("/cgi-bin/menu/create", data);
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public GetMenuResult GetMenuList()
        {
            return GetJson<GetMenuResult>("/cgi-bin/menu/get");
        }

        /// <summary>
        /// 删除所有菜单
        /// </summary>
        /// <returns></returns>
        public Result DeleteMenu()
        {
            return GetJson("/cgi-bin/menu/delete");
        }

        /// <summary>
        /// 创建个性化菜单
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public CreateConditionalMenuResult CreateConditionalMenu(Matchrule rule,params Button[] buttons)
        {
            var data = new
            {
                button = buttons,
                matchrule = rule
            };

            return PostJson< CreateConditionalMenuResult>("/cgi-bin/menu/addconditional", data);
        }

        /// <summary>
        /// 删除个性化菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Result DeleteConditionalMenu(int menuId)
        {
            var data = new
            {
                menuid = menuId
            };

            return PostJson("/cgi-bin/menu/delconditional", data);
        }

        /// <summary>
        /// 测试个性化菜单匹配结果
        /// </summary>
        /// <param name="menuId">可以是粉丝的OpenID，也可以是粉丝的微信号</param>
        /// <returns></returns>
        public TryMatchResult TryMatch(string userId)
        {
            var data = new
            {
                user_id = userId
            };

            return PostJson<TryMatchResult>("/cgi-bin/menu/trymatch", data);
        }

        public GetCurrentSelfMenuResult GetCurrentSelfMenu()
        {
            return GetJson<GetCurrentSelfMenuResult>("/cgi-bin/get_current_selfmenu_info");
        }
    }
}
