using NFlex.Opens.Weixin.Models;
using NFlex.Opens.Weixin.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Managers
{
    public class CustomerServiceManager:ManagerBase
    {
        internal CustomerServiceManager(string apiUrl, AccessTokenContainer tokenContainer) : base(apiUrl, tokenContainer) { }

        /// <summary>
        /// 添加客服账号
        /// </summary>
        /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="nickname">	客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="password">客服账号登录密码，格式为密码明文的32位加密MD5值。
        /// <para>该密码仅用于在公众平台官网的多客服功能中使用，若不使用多客服功能，则不必设置密码</para>
        /// </param>
        /// <returns></returns>
        public Result AddkfAccount(string account,string nickname,string password)
        {
            var data = new
            {
                kf_account = account,
                nickname = nickname,
                password = password
            };

            return PostJson("/customservice/kfaccount/add", data);
        }

        /// <summary>
        /// 修改客服账号
        /// </summary>
        /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="nickname">	客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="password">客服账号登录密码，格式为密码明文的32位加密MD5值。
        /// <para>该密码仅用于在公众平台官网的多客服功能中使用，若不使用多客服功能，则不必设置密码</para>
        /// </param>
        /// <returns></returns>
        public Result UpdatekfAccount(string account, string nickname, string password)
        {
            var data = new
            {
                kf_account = account,
                nickname = nickname,
                password = password
            };

            return PostJson("/customservice/kfaccount/update", data);
        }

        /// <summary>
        /// 删除客服账号
        /// </summary>
        /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="nickname">	客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="password">客服账号登录密码，格式为密码明文的32位加密MD5值。
        /// <para>该密码仅用于在公众平台官网的多客服功能中使用，若不使用多客服功能，则不必设置密码</para>
        /// </param>
        /// <returns></returns>
        public Result DeletekfAccount(string account, string nickname, string password)
        {
            var data = new
            {
                kf_account = account,
                nickname = nickname,
                password = password
            };

            return PostJson("/customservice/kfaccount/del", data);
        }

        /// <summary>
        /// 上传客服头像
        /// </summary>
        /// <param name="kfAccount">客服账号</param>
        /// <param name="imgFilePath">本地图片文件地址
        /// <para>头像图片文件必须是jpg格式，推荐使用640*640大小的图片以达到最佳效果</para>
        /// </param>
        /// <returns></returns>
        public Result UploadHeadImage(string kfAccount, string imgFilePath)
        {
            return null;
        }

        /// <summary>
        /// 获取客服列表
        /// </summary>
        /// <returns></returns>
        public GetCustomerServiceListResult GetCustomerServiceList()
        {
            return GetJson<GetCustomerServiceListResult>("/cgi-bin/customservice/getkflist");
        }

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="toOpenId">普通用户openid</param>
        /// <param name="text">文本消息内容</param>
        /// <param name="kfAccount">客服账号</param>
        /// <returns></returns>
        public Result SendText(string toOpenId, string text,string kfAccount="")
        {
            var msg = GetSubmitObject(toOpenId, MessageType.Text, kfAccount);
            msg.text = new { content = text };

            return PostJson("/cgi-bin/message/custom/send", msg);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="toOpenId">普通用户openid</param>
        /// <param name="mediaId">发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID</param>
        /// <param name="kfAccount">客服账号</param>
        /// <returns></returns>
        public Result SendImage(string toOpenId,string mediaId, string kfAccount = "")
        {
            var msg = GetSubmitObject(toOpenId, MessageType.Image, kfAccount);
            msg.image = new { media_id = mediaId };

            return PostJson("/cgi-bin/message/custom/send", msg);
        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="toOpenId">普通用户openid</param>
        /// <param name="mediaId">发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID</param>
        /// <param name="kfAccount">客服账号</param>
        /// <returns></returns>
        public Result SendVoice(string toOpenId, string mediaId, string kfAccount = "")
        {
            var msg = GetSubmitObject(toOpenId, MessageType.Voice, kfAccount);
            msg.voice = new { media_id = mediaId };

            return PostJson("/cgi-bin/message/custom/send", msg);
        }

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="toOpenId">普通用户openid</param>
        /// <param name="mediaId">发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID</param>
        /// <param name="thumbMediaId">缩略图的媒体ID</param>
        /// <param name="title">图文消息/视频消息/音乐消息的标题</param>
        /// <param name="desc">图文消息/视频消息/音乐消息的描述</param>
        /// <param name="kfAccount">客服账号</param>
        /// <returns></returns>
        public Result SendVideo(string toOpenId, string mediaId,string thumbMediaId,string title,string desc, string kfAccount = "")
        {
            var msg = GetSubmitObject(toOpenId, MessageType.Video, kfAccount);
            msg.video = new
            {
                media_id = mediaId,
                thumb_media_id = thumbMediaId,
                title = title,
                description = desc
            };

            return PostJson("/cgi-bin/message/custom/send", msg);
        }

        /// <summary>
        /// 发送音乐消息
        /// </summary>
        /// <param name="toOpenId">普通用户openid</param>
        /// <param name="title">图文消息/视频消息/音乐消息的标题</param>
        /// <param name="desc">图文消息/视频消息/音乐消息的描述</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="thumbId">缩略图的媒体ID</param>
        /// <param name="kfAccount">客服账号</param>
        /// <returns></returns>
        public Result SendMusic(string toOpenId, string title, string desc, string musicUrl, string hqMusicUrl, string thumbId, string kfAccount = "")
        {
            var msg = GetSubmitObject(toOpenId, MessageType.Music, kfAccount);
            msg.music = new
            {
                title = title,
                description = desc,
                musicurl = musicUrl,
                hqmusicurl = hqMusicUrl,
                thumb_media_id = thumbId
            };

            return PostJson("/cgi-bin/message/custom/send", msg);
        }

        public Result SendNews(string toOpenId,List<Article> list, string kfAccount = "")
        {
            var msg = GetSubmitObject(toOpenId, MessageType.News, kfAccount);
            msg.news =new{ articles=list};
            return PostJson("/cgi-bin/message/custom/send", msg);
        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="toOpenId">普通用户openid</param>
        /// <param name="mediaId">发送的图片/语音/视频/图文消息（点击跳转到图文消息页）的媒体ID</param>
        /// <param name="kfAccount">客服账号</param>
        /// <returns></returns>
        public Result SendNews(string toOpenId, string mediaId, string kfAccount = "")
        {
            var msg = GetSubmitObject(toOpenId, MessageType.News, kfAccount);
            msg.news = new { media_id = mediaId };

            return PostJson("/cgi-bin/message/custom/send", msg);
        }

        /// <summary>
        /// 发送卡券
        /// </summary>
        /// <param name="toOpenId">普通用户openid</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="kfAccount">客服账号</param>
        public Result SendCard(string toOpenId, string cardId, string kfAccount = "")
        {
            var msg = GetSubmitObject(toOpenId, MessageType.WxCard, kfAccount);
            msg.wxcard = new { card_id = cardId };
            return PostJson("/cgi-bin/message/custom/send", msg);
        }

        private dynamic GetSubmitObject(string toUserName, MessageType msgType,string kfAccount)
        {
            dynamic obj = new System.Dynamic.ExpandoObject();
            obj.touser = toUserName;
            obj.msgtype = msgType.ToString().ToLower();
            if(!string.IsNullOrEmpty(kfAccount))
            {
                obj.customservice = new { kf_account = kfAccount };
            }
            return obj;
        }

        public class Article
        {
            public string title { get; set; }
            public string description { get; set; }
            public string url { get; set; }
            public string picurl { get; set; }
        }
    }
}
