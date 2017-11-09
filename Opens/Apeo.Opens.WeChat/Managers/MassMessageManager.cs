using NFlex.Opens.Weixin.Models;
using NFlex.Opens.Weixin.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Managers
{
    public class MassMessageManager:ManagerBase
    {
        internal MassMessageManager(string apiUrl, AccessTokenContainer tokenContainer) : base(apiUrl, tokenContainer) { }

        /// <summary>
        /// 上传图文消息素材
        /// </summary>
        /// <param name="articles">图文消息，一个图文消息支持1到10条图文</param>
        /// <returns></returns>
        public UploadMediaResult UploadNews(List<Article> articles)
        {
            var data = new
            {
                articles = articles
            };

            return PostJson<UploadMediaResult>("/cgi-bin/media/uploadnews", data);
        }

        /// <summary>
        /// 上传群发视频素材
        /// </summary>
        /// <param name="fileName">文件绝对路径</param>
        /// <returns></returns>
        public UploadMediaResult UploadVideo(string fileName)
        {
            return GetClient()
                   .AddFile("file", fileName)
                   .Post("https://file.api.weixin.qq.com/cgi-bin/media/uploadvideo")
                   .JsonTo<UploadMediaResult>();
        }


        #region 群发消息
        /// <summary>
        /// 群发文本消息
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="groupId">群发到的分组的group_id，为空时发送给所有人</param>
        /// <returns></returns>
        public MessMessageSendResult SendText(string content, string groupId = null)
        {
            var data = GetMessageObject(MessageType.Text, groupId, null);
            data.text = new { content = content };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/sendall", data);
        }
        /// <summary>
        /// 群发文本消息
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="toUserId">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public MessMessageSendResult SendText(string content, List<string> toUserId)
        {
            var data = GetMessageObject(MessageType.Text, null, toUserId);
            data.text = new { content = content };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/send", data);
        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="mediaId">媒体文件/图文消息上传后获取的唯一标识</param>
        /// <param name="groupId">群发到的分组的group_id，为空时发送给所有人</param>
        /// <returns></returns>
        public MessMessageSendResult SendNews(string mediaId, string groupId = null)
        {
            var data = GetMessageObject(MessageType.MpNews, groupId, null);
            data.mpnews = new { media_id = mediaId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/sendall", data);
        }
        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="mediaId">媒体文件/图文消息上传后获取的唯一标识</param>
        /// <param name="toUserId">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public MessMessageSendResult SendNews(string mediaId, List<string> toUserId)
        {
            var data = GetMessageObject(MessageType.MpNews, null, toUserId);
            data.mpnews = new { media_id = mediaId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/send", data);
        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="mediaId">媒体文件/图文消息上传后获取的唯一标识</param>
        /// <param name="groupId">群发到的分组的group_id，为空时发送给所有人</param>
        /// <returns></returns>
        public MessMessageSendResult SendVoice(string mediaId, string groupId = null)
        {
            var data = GetMessageObject(MessageType.Voice, groupId, null);
            data.voice = new { media_id = mediaId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/sendall", data);
        }
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="mediaId">媒体文件/图文消息上传后获取的唯一标识</param>
        /// <param name="toUserId">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public MessMessageSendResult SendVoice(string mediaId, List<string> toUserId)
        {
            var data = GetMessageObject(MessageType.Voice, null, toUserId);
            data.voice = new { media_id = mediaId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/send", data);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="mediaId">媒体文件/图文消息上传后获取的唯一标识</param>
        /// <param name="groupId">群发到的分组的group_id，为空时发送给所有人</param>
        /// <returns></returns>
        public MessMessageSendResult SendImage(string mediaId, string groupId = null)
        {
            var data = GetMessageObject(MessageType.Image, groupId, null);
            data.image = new { media_id = mediaId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/sendall", data);
        }
        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="mediaId">媒体文件/图文消息上传后获取的唯一标识</param>
        /// <param name="toUserId">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public MessMessageSendResult SendImage(string mediaId, List<string> toUserId)
        {
            var data = GetMessageObject(MessageType.Image, null, toUserId);
            data.image = new { media_id = mediaId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/send", data);
        }

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="mediaId">媒体文件/图文消息上传后获取的唯一标识</param>
        /// <param name="groupId">群发到的分组的group_id，为空时发送给所有人</param>
        /// <returns></returns>
        public MessMessageSendResult SendVideo(string mediaId, string groupId = null)
        {
            var data = GetMessageObject(MessageType.MpVideo, groupId, null);
            data.mpvideo = new { media_id = mediaId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/sendall", data);
        }
        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="mediaId">媒体文件/图文消息上传后获取的唯一标识</param>
        /// <param name="toUserId">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public MessMessageSendResult SendVideo(string mediaId, List<string> toUserId)
        {
            var data = GetMessageObject(MessageType.MpVideo, null, toUserId);
            data.mpvideo = new { media_id = mediaId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/send", data);
        }

        /// <summary>
        /// 发送卡券
        /// </summary>
        /// <param name="cardId">卡券编号</param>
        /// <param name="groupId">群发到的分组的group_id，为空时发送给所有人</param>
        /// <returns></returns>
        public MessMessageSendResult SendCard(string cardId, string groupId = null)
        {
            var data = GetMessageObject(MessageType.WxCard, groupId, null);
            data.wxcard = new { card_id = cardId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/sendall", data);
        }
        /// <summary>
        /// 发送卡券
        /// </summary>
        /// <param name="cardId">卡券编号</param>
        /// <param name="toUserId">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public MessMessageSendResult SendCard(string cardId, List<string> toUserId)
        {
            var data = GetMessageObject(MessageType.WxCard, null, toUserId);
            data.wxcard = new { card_id = cardId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/send", data);
        }
        #endregion

        #region 发送群消息预览
        /// <summary>
        /// 群发文本消息
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="groupId">群发到的分组的group_id，为空时发送给所有人</param>
        /// <returns></returns>
        public MessMessageSendResult SendPreviewText(string content,AccountType accountType,string account)
        {
            var data = GetPreviewMessageObject(MessageType.Text, accountType, account);
            data.text = new { content = content };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/preview", data);
        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="mediaId">媒体文件/图文消息上传后获取的唯一标识</param>
        /// <param name="groupId">群发到的分组的group_id，为空时发送给所有人</param>
        /// <returns></returns>
        public MessMessageSendResult SendPreviewNews(string mediaId, AccountType accountType, string account)
        {
            var data = GetPreviewMessageObject(MessageType.MpNews, accountType, account);
            data.mpnews = new { media_id = mediaId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/preview", data);
        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="mediaId">媒体文件/图文消息上传后获取的唯一标识</param>
        /// <param name="groupId">群发到的分组的group_id，为空时发送给所有人</param>
        /// <returns></returns>
        public MessMessageSendResult SendPreviewVoice(string mediaId, AccountType accountType, string account)
        {
            var data = GetPreviewMessageObject(MessageType.Voice, accountType, account);
            data.voice = new { media_id = mediaId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/preview", data);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="mediaId">媒体文件/图文消息上传后获取的唯一标识</param>
        /// <param name="groupId">群发到的分组的group_id，为空时发送给所有人</param>
        /// <returns></returns>
        public MessMessageSendResult SendPreviewImage(string mediaId, AccountType accountType, string account)
        {
            var data = GetPreviewMessageObject(MessageType.Image, accountType, account);
            data.image = new { media_id = mediaId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/preview", data);
        }

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="mediaId">媒体文件/图文消息上传后获取的唯一标识</param>
        /// <param name="groupId">群发到的分组的group_id，为空时发送给所有人</param>
        /// <returns></returns>
        public MessMessageSendResult SendPreviewVideo(string mediaId, AccountType accountType, string account)
        {
            var data = GetPreviewMessageObject(MessageType.MpVideo, accountType, account);
            data.mpvideo = new { media_id = mediaId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/preview", data);
        }

        /// <summary>
        /// 发送卡券
        /// </summary>
        /// <param name="cardId">卡券编号</param>
        /// <param name="groupId">群发到的分组的group_id，为空时发送给所有人</param>
        /// <returns></returns>
        public MessMessageSendResult SendPreviewCard(string cardId, AccountType accountType, string account)
        {
            var data = GetPreviewMessageObject(MessageType.WxCard, accountType, account);
            data.wxcard = new { card_id = cardId };
            return PostJson<MessMessageSendResult>("/cgi-bin/message/mass/preview", data);
        }
        #endregion

        /// <summary>
        /// 取消群发
        /// </summary>
        /// <param name="messageId">群发消息后返回的消息id</param>
        /// <returns></returns>
        public Result CancelSend(string messageId)
        {
            var data = new
            {
                msg_id = messageId
            };

            return PostJson("/cgi-bin/message/mass/delete", data);
        }

        /// <summary>
        /// 查询群发消息发送状态
        /// </summary>
        /// <param name="messageId">群发消息后返回的消息id</param>
        /// <returns></returns>
        public GetSendStatusResult GetSendStatus(string messageId)
        {
            var data = new
            {
                msg_id = messageId
            };

            return PostJson<GetSendStatusResult>("/cgi-bin/message/mass/get", data);
        }


        private dynamic GetMessageObject(MessageType msgType,string tagId=null,List<string> toUserId=null)
        {
            dynamic obj = new System.Dynamic.ExpandoObject();
            if(toUserId!=null && toUserId.Count>0)
                obj.touser = toUserId;
            if(string.IsNullOrWhiteSpace(tagId))
            {
                obj.filter = new
                {
                    is_to_all = true,
                    tag_id = ""
                };
            }
            else
            {
                obj.filter = new
                {
                    is_to_all = false,
                    tag_id = tagId
                };
            }
            obj.msgtype = msgType.ToString().ToLower();
            return obj;
        }

        private dynamic GetPreviewMessageObject(MessageType msgType,AccountType accountType,string account)
        {
            dynamic obj = new System.Dynamic.ExpandoObject();
            if(accountType==AccountType.OpenID)
            {
                obj.touser = account;
            }
            else
            {
                obj.towxname = account;
            }
            obj.msgtype = msgType.ToString().ToLower();
            return obj;
        }
    }
}
