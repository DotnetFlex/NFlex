using NFlex;
using NFlex.Opens.Weixin;
using NFlex.Opens.Weixin.PushMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace Demo.WechatManagement
{
    public class WeixinManager
    {
        public WxReceiver Receiver { get; set; }
        public WxClient Client { get; set; }

        private static WeixinManager _manager;

        public static WeixinManager Instance
        {
            get
            {
                if (_manager == null)
                    Init();
                return _manager;
            }
        }

        private WeixinManager(WeixinConfig config)
        {
            Client = new WxClient(config.ApiUrl, new DefaultTokenContainer(config.ApiUrl, config.AppId, config.AppSecret));
            Receiver = new WxReceiver(config.Token, config.EncodingAESKey);

            Receiver.ReceiveTextMessage += Receiver_ReceiveTextMessage;
            Receiver.SubscribeEvent += Receiver_SubscribeEvent;
        }

        private void Receiver_SubscribeEvent(ScanEventArgs eventArgs, Replier replier)
        {
            if(string.IsNullOrEmpty(eventArgs.EventKey))
            {
                replier.ReplyText("终于等到你，还好我没放弃！\r\n在深圳购买演出票记得上趣票哦！！！");
            }
            else
            {

                replier.ReplyNews(new Replier.NewsContent
                {
                    Title = "“创作随你”德国红点设计大展暨万象天地创作纪念展",
                    PicUrl = "http://imgcdn.qupiaowang.com:9007//TicketPic/20170904/20170904173449578_B64354B5918146DBB8447009A7927B7E_-591542336.jpg",
                    Url = "wechat.qupiaowang.com/infor/show-5814.html",
                    Description = "设计届的奥斯卡，世界三大设计奖项之首，红点跨界合作商业中心，17年获奖作品首次亮相中国"
                });
            }
        }

        private void Receiver_ReceiveTextMessage(TextMessage message, Replier replier)
        {
            replier.ReplyText("你发送的："+message.Content);
        }

        private static void Init()
        {
            var config = Config.LoadConfig<WeixinConfig>("weixin.config");
            _manager = new WeixinManager(config);
        }
    }
}