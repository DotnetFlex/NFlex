using NFlex.Opens.Weixin.PushMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace NFlex.Opens.Weixin
{
    public sealed class Replier
    {
        HttpResponse Response { get; }

        /// <summary>
        /// 接收方帐号（收到的OpenID）
        /// </summary>
        public string ToUserName { get;}

        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string FromUserName { get;}

        /// <summary>
        /// 接收到请求的时间(超过5秒回复无效)
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取当前时间是否超过允许的回复时间
        /// </summary>
        public bool IsTimeout
        {
            get
            {
                return (DateTime.Now - CreateTime).TotalSeconds>= 5;
            }
        }

        private Replier(string f,string t,HttpResponse r)
        {
            Response = r;
            ToUserName = t;
            FromUserName = f;
            CreateTime = DateTime.Now;
        }

        internal static Replier Create(RequestData data)
        {
            var msg = data.ConvertBodyTo<PushObject>();
            if (msg == null) return null;
            var reply = new Replier(msg.ToUserName, msg.FromUserName, data.Response);
            return reply;
        }

        /// <summary>
        /// 放弃被动回复
        /// </summary>
        public void PassReply()
        {
            Response.Clear();
            Response.End();
        }

        /// <summary>
        /// 回复文本消息
        /// </summary>
        /// <param name="message">回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）</param>
        public void ReplyText(string message)
        {
            var msg = CreateMessage(MessageType.Text);
            msg.Content = message;
            Reply(msg);
        }

        /// <summary>
        /// 回复图片消息
        /// </summary>
        /// <param name="mediaId">通过素材管理接口上传多媒体文件，得到的id。</param>
        public void ReplyImage(string mediaId)
        {
            var msg = CreateMessage(MessageType.Image);
            msg.Image = new { MediaId = mediaId };
            Reply(msg);
        }

        /// <summary>
        /// 回复语音消息
        /// </summary>
        /// <param name="mediaId">通过素材管理接口上传多媒体文件，得到的id</param>
        public void ReplyVioce(string mediaId)
        {
            var msg = CreateMessage(MessageType.Voice);
            msg.Voice = new { MediaId = mediaId };
            Reply(msg);
        }

        /// <summary>
        /// 回复视频消息
        /// </summary>
        /// <param name="mediaId">通过素材管理接口上传多媒体文件，得到的id</param>
        /// <param name="title">视频消息的标题</param>
        /// <param name="desc">视频消息的描述</param>
        public void ReplyVideo(string mediaId,string title,string desc)
        {
            var msg = CreateMessage(MessageType.Video);
            msg.Video = new 
            {
                MediaId = mediaId,
                Title = title,
                Description = desc
            };
            Reply(msg);
        }

        /// <summary>
        /// 回复音乐消息
        /// </summary>
        /// <param name="title">音乐标题</param>
        /// <param name="desc">音乐描述</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高质量音乐链接，WIFI环境优先使用该链接播放音乐</param>
        /// <param name="thumbId">缩略图的媒体id，通过素材管理接口上传多媒体文件，得到的id</param>
        public void ReplyMusic(string title,string desc,string musicUrl,string hqMusicUrl,string thumbId)
        {
            var msg = CreateMessage(MessageType.Music);
            msg.Music = new 
            {
                Title = title,
                Description = desc,
                MusicUrl=musicUrl,
                HQMusicUrl=hqMusicUrl,
                ThumbMediaId=thumbId
            };
            Reply(msg);
        }

        /// <summary>
        /// 回复图文消息
        /// </summary>
        /// <param name="news">图文列表（最多8条）</param>
        public void ReplyNews(params NewsContent[] news)
        {
            if (news == null || !news.Any())
            {
                PassReply();
                return;
            }

            var _news = news.ToList();
            while(_news.Count > 8)
            {
                _news.RemoveAt(_news.Count - 1);
            }

            var msg = CreateMessage(MessageType.News);
            msg.Articles = news;
            msg.ArticleCount = _news.Count;
            Reply(msg);
        }

        private dynamic CreateMessage(MessageType msgType)
        {
            dynamic msg = new DynamicObject();
            msg.FromUserName = FromUserName;
            msg.ToUserName = ToUserName;
            msg.CreateTime = Common.TimeStamp;
            msg.MsgType = msgType.ToString().ToLower();
            return msg;
        }

        private void Reply(DynamicObject msg)
        {
            
            Response.Clear();
            var xml = msg.ToXml();
            Response.Write(xml);
            Response.End();
        }

        [XmlRoot("item")]
        public class NewsContent
        {
            /// <summary>
            /// 图文消息标题
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 图文消息描述
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
            /// </summary>
            public string PicUrl { get; set; }

            /// <summary>
            /// 点击图文消息跳转链接
            /// </summary>
            public string Url { get; set; }
        }
    }
}
