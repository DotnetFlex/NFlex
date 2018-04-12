using NFlex;
using NFlex.Opens.Taobao.AlimamaResult;
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
            var searchStr = GetSearchContent(message.Content);
            if(string.IsNullOrEmpty(searchStr))
            {
                replier.ReplyText("根据您输入的内容无法找到优惠商品");
                return;
            }

            var items = AlimamaClientFactory.Instance.SearchItems(searchStr);
            if(items.Count==0)
            {
                replier.ReplyText("没有找到此商品的优惠链接");
                return;
            }

            #region 转换商品链接
            var item = items.First();
            var adzones = AlimamaClientFactory.Instance.GetAdzones(item.auctionId);
            var adzone = adzones.webAdzones.FirstOrDefault();
            var adzoneId = adzone.id;
            var siteId = adzone.sub.FirstOrDefault().id;
            var urlInfo = AlimamaClientFactory.Instance.CreatePromotUrl(items[0].auctionId, siteId, adzoneId);

            string msg = string.Format("{0}\r\n{1}\r\n\r\n原价：{2}元\r\n优惠券：{3}\r\n券后价：{4}\r\n\r\n{5}\r\n【长按复制本条信息，然后打开手机淘宝领券下单即可】",
                item.title,
                urlInfo.couponLinkTaoToken ?? urlInfo.taoToken,
                item.zkPrice,
                item.couponInfo,
                item.zkPrice - item.couponAmount,
                urlInfo.couponShortLinkUrl ?? urlInfo.shortLinkUrl
                );
            #endregion

            replier.ReplyText(msg);
        }

        private string GetSearchContent(string str)
        {
            //获取文本中所有Url链接
            var urls = str.GetUrls();

            //如果文本中不含链接，则试着当做商品名称来查询
            if (urls.Count == 0)
            {
                return str;
            }

            foreach(string url in urls)
            {
                if (!string.IsNullOrEmpty(url.GetUrlParam("id"))) return string.Format("https://item.taobao.com/item.htm?id={0}", url.GetUrlParam("id"));

                HttpClient client = new HttpClient();
                var subUrls = client.Get(url).ToString().GetUrls();
                var subUrl = subUrls.FirstOrDefault(t => !string.IsNullOrEmpty(t.GetUrlParam("id")));
                if (!string.IsNullOrEmpty(subUrl.GetUrlParam("id"))) return string.Format("https://item.taobao.com/item.htm?id={0}", subUrl.GetUrlParam("id"));
            }

            return "";
        }

        private static void Init()
        {
            var config = Config.LoadConfig<WeixinConfig>("weixin.config");
            _manager = new WeixinManager(config);
        }
    }
}