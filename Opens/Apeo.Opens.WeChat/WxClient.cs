using NFlex.Opens.Weixin.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin
{
    public class WxClient
    {
        private string _apiUrl;
        private AccessTokenContainer _tokenContainer;

        /// <summary>
        /// 菜单管理
        /// </summary>
        public MenuManager Menu { get; set; }
        /// <summary>
        /// 客服管理
        /// </summary>
        public CustomerServiceManager Customer { get; set; }
        /// <summary>
        /// 群发消息管理
        /// </summary>
        public MassMessageManager MassMessage { get; set; }
        /// <summary>
        /// 模板管理
        /// </summary>
        public TemplateManager Template { get; set; }
        /// <summary>
        /// 素材管理
        /// </summary>
        public MaterialManager Material { get; set; }
        /// <summary>
        /// 用户管理
        /// </summary>
        public UserManager User { get; set; }
        /// <summary>
        /// 账户管理
        /// </summary>
        public AccountManager Account { get; set; }
        /// <summary>
        /// 卡券管理
        /// </summary>
        public CardManager Card { get; set; }

        public WxClient(string apiUrl, AccessTokenContainer tokenContainer)
        {
            _apiUrl = apiUrl;
            _tokenContainer = tokenContainer;

            Menu = new MenuManager(apiUrl, _tokenContainer);
            Customer = new CustomerServiceManager(apiUrl, _tokenContainer);
            MassMessage = new MassMessageManager(apiUrl, _tokenContainer);
            Template = new TemplateManager(apiUrl, _tokenContainer);
            Material = new MaterialManager(apiUrl, _tokenContainer);
            User = new UserManager(apiUrl, _tokenContainer);
            Account = new AccountManager(apiUrl, _tokenContainer);
            Card = new CardManager(apiUrl, _tokenContainer);
        }
    }
}
