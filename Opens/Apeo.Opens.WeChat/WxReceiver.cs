using NFlex.Opens.Weixin.PushMessage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static NFlex.Opens.Weixin.EventDelegate;

namespace NFlex.Opens.Weixin
{
    public class WxReceiver
    {
        private Dictionary<MessageType, ReceiveEventType, Action<RequestData>> handlerList;
        /// <summary>
        /// 微信鉴权验证凭证
        /// </summary>
        public string Token { get; set; }
        public string EncodingAESKey { get; set; }

        public WxReceiver(string token,string encodingAESKey)
        {
            Token = token;
            EncodingAESKey = encodingAESKey;
            handlerList = new Dictionary<MessageType, ReceiveEventType, Action<RequestData>>();
            InitEventsHandle();
        }

        #region 事件定义
        #region 普通消息处理事件定义
        /// <summary>
        /// 收到文本消息
        /// </summary>
        public event ReceiveTextEventHandler ReceiveTextMessage;

        /// <summary>
        /// 收到图片消息
        /// </summary>
        public event ReceiveImageEventHandler ReceiveImageMessage;

        /// <summary>
        /// 收到语音消息
        /// </summary>
        public event ReceiveVoiceEventHandler ReceiveVoiceMessage;

        /// <summary>
        /// 收到视频消息
        /// </summary>
        public event ReceiveVideoEventHandler ReceiveVideoMessage;

        /// <summary>
        /// 收到小视频消息
        /// </summary>
        public event ReceiveShortVideoEventHandler ReceiveShortVideoMessage;

        /// <summary>
        /// 收到地理位置消息
        /// </summary>
        public event ReceiveLocationEventHandler ReceiveLocationMessage;

        /// <summary>
        /// 收到链接消息
        /// </summary>
        public event ReceiveLinkEventHandler ReceiveLinkMessage;
        #endregion

        #region 推送事件处理事件定义
        /// <summary>
        /// 公众号被关注事件
        /// </summary>
        public event SubscribeEventHandler SubscribeEvent;

        /// <summary>
        /// 取消关注事件
        /// </summary>
        public event UnSubscribeEventHandler UnSubscribeEvent;

        /// <summary>
        /// 扫描带参数二维码事件
        /// </summary>
        public event ScanEventHandler ScanEvent;

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        public event LocationEventHandler LocationEvent;

        /// <summary>
        /// 自定义菜单事件
        /// </summary>
        public event MenuClickEventHandler MenuClickEvent;

        /// <summary>
        /// 点击菜单跳转链接时的事件推送
        /// </summary>
        public event ViewEventHandler ViewEvent;

        /// <summary>
        /// 群发任务完成事件
        /// </summary>
        public event MessageEndJobFinishEventHandler MessageEndJobFinishEvent;

        /// <summary>
        /// 模板消息发送完成事件
        /// </summary>
        public event TemplateSendJobFinishEventHandler TemplateSendJobFinishEvent;

        /// <summary>
        /// 买单事件推送
        /// </summary>
        public event UserPayFromPayCellHandler UserPayFromPayCellEvent;

        /// <summary>
        /// 卡券审核事件
        /// <para>生成的卡券通过审核时会触发此事件</para>
        /// </summary>
        public event CardPassCheckHandler CardPassCheckEvent;

        /// <summary>
        /// 卡券领取事件
        /// <para>用户在领取卡券时会触发此事件</para>
        /// </summary>
        public event UserGetCardHandler UserGetCardEvent;

        /// <summary>
        /// 卡券转赠事件
        /// <para>用户在转赠卡券时会触发此事件</para>
        /// </summary>
        public event UserGiftingCardHandler UserGiftingCardEvent;

        /// <summary>
        /// 卡券删除事件
        /// <para>用户在删除卡券时会触发此事件</para>
        /// </summary>
        public event UserDelCardHandler UserDelCardEvent;

        /// <summary>
        /// 卡券核销事件
        /// <para>卡券被核销时会触发此事件</para>
        /// </summary>
        public event UserConsumeCardHandler UserConsumeCardEvent;

        /// <summary>
        /// 进入会员卡事件
        /// <para>用户在进入会员卡时会触发此事件</para>
        /// <para>需要开发者在创建会员卡时填入need_push_on_view	字段并设置为true</para>
        /// <para>开发者须综合考虑领卡人数和服务器压力，决定是否接收该事件</para>
        /// </summary>
        public event UserViewCardHandler UserViewCardEvent;

        /// <summary>
        /// 从卡券进入公众号会话事件
        /// <para>用户在进入会员卡时会触发此事件</para>
        /// <para>需要开发者在创建会员卡时填入need_push_on_view	字段并设置为true</para>
        /// <para>开发者须综合考虑领卡人数和服务器压力，决定是否接收该事件</para>
        /// </summary>
        public event UserEnterSessionFromCardHandler UserEnterSessionFromCardEvent;

        /// <summary>
        /// 会员卡内容更新事件
        /// <para>当用户的会员卡积分余额发生变动时会触发此事件</para>
        /// </summary>
        public event UpdateMemberCardHandler UpdateMemberCardEvent;

        /// <summary>
        /// 库存报警事件
        /// <para>当某个card_id的初始库存数大于200且当前库存小于等于100时，</para>
        /// <para>用户尝试领券会触发发送事件给商户，事件每隔12h发送一次。</para>
        /// </summary>
        public event CardSkuRemindHandler CardSkuRemindEvent;

        /// <summary>
        /// 券点流水详情事件
        /// <para>当商户朋友的券券点发生变动时会触发此事件</para>
        /// </summary>
        public event CardPayOrderHandler CardPayOrderEvent;

        /// <summary>
        /// 会员卡激活事件推送
        /// <para>当用户通过一键激活的方式提交信息并点击激活或者用户修改会员卡信息后触发此事件</para>
        /// </summary>
        public event SubmitMemberCardUserInfoHandler SubmitMemberCardUserInfoEvent;
        #endregion
        #endregion

        /// <summary>
        /// 初始化微信消息处理事件
        /// </summary>
        private void InitEventsHandle()
        {
            #region 普通消息
            //文本消息
            handlerList.Add(MessageType.Text, ReceiveEventType.None, (data) =>
            {
                ReceiveTextMessage?.Invoke(data.ConvertBodyTo<TextMessage>(), Replier.Create(data));
            });

            //图片消息
            handlerList.Add(MessageType.Image, ReceiveEventType.None, (data) =>
            {
                ReceiveImageMessage?.Invoke(data.ConvertBodyTo<ImageMessage>(), Replier.Create(data));
            });

            //语音消息
            handlerList.Add(MessageType.Voice, ReceiveEventType.None, (data) =>
            {
                ReceiveVoiceMessage?.Invoke(data.ConvertBodyTo<VoiceMessage>(), Replier.Create(data));
            });

            //视频消息
            handlerList.Add(MessageType.Video, ReceiveEventType.None, (data) =>
            {
                ReceiveVideoMessage?.Invoke(data.ConvertBodyTo<VideoMessage>(), Replier.Create(data));
            });

            //小视频消息
            handlerList.Add(MessageType.ShortVideo, ReceiveEventType.None, (data) =>
            {
                ReceiveShortVideoMessage?.Invoke(data.ConvertBodyTo<ShortVideoMessage>(), Replier.Create(data));
            });

            //地理位置消息
            handlerList.Add(MessageType.Location, ReceiveEventType.None, (data) =>
            {
                ReceiveLocationMessage?.Invoke(data.ConvertBodyTo<LocationMessage>(), Replier.Create(data));
            });

            //链接消息
            handlerList.Add(MessageType.Link, ReceiveEventType.None, (data) =>
            {
                ReceiveLinkMessage?.Invoke(data.ConvertBodyTo<LinkMessage>(), Replier.Create(data));
            });
            #endregion

            #region 事件消息
            //关注事件
            handlerList.Add(MessageType.Event, ReceiveEventType.Subscribe, (data) =>
            {
                var _event = data.ConvertBodyTo<ScanEventArgs>();
                if (string.IsNullOrEmpty(_event.EventKey))
                {
                    SubscribeEvent?.Invoke(_event, Replier.Create(data));
                }
                else
                {
                    _event.EventKey = _event.EventKey.Substring("qrscene_".Length);
                    ScanEvent?.Invoke(_event, Replier.Create(data));
                }
            });

            //取消关注事件
            handlerList.Add(MessageType.Event, ReceiveEventType.UnSubscribe, (data) =>
            {
                PassReply(data);
                UnSubscribeEvent?.Invoke(data.ConvertBodyTo<EventArgsBase>());
            });

            //扫描带参数二维码事件
            handlerList.Add(MessageType.Event, ReceiveEventType.Scan, (data) =>
            {
                ScanEvent?.Invoke(data.ConvertBodyTo<ScanEventArgs>(), Replier.Create(data));
            });

            //上报地理位置事件
            handlerList.Add(MessageType.Event, ReceiveEventType.Location, (data) =>
            {
                PassReply(data);
                LocationEvent?.Invoke(data.ConvertBodyTo<LocationEventArgs>());
            });

            //点击菜单拉取消息事件
            handlerList.Add(MessageType.Event, ReceiveEventType.Click, (data) =>
            {
                MenuClickEvent?.Invoke(data.ConvertBodyTo<WithKeyEventArgs>(), Replier.Create(data));
            });

            //点击菜单跳转链接事件
            handlerList.Add(MessageType.Event, ReceiveEventType.View, (data) =>
            {
                PassReply(data);
                ViewEvent?.Invoke(data.ConvertBodyTo<WithKeyEventArgs>());
            });

            //群发任务完成事件
            handlerList.Add(MessageType.Event, ReceiveEventType.MessageEndJobFinish, (data) =>
            {
                PassReply(data);
                MessageEndJobFinishEvent?.Invoke(data.ConvertBodyTo<MessageEndJobFinishEventArgs>());
            });

            //模板消息发送完成事件
            handlerList.Add(MessageType.Event, ReceiveEventType.TemplateSendJobFinish, (data) =>
            {
                PassReply(data);
                TemplateSendJobFinishEvent?.Invoke(data.ConvertBodyTo<TemplateSendJobFinishEventArgs >());
            });

            //买单事件推送
            handlerList.Add(MessageType.Event, ReceiveEventType.user_pay_from_pay_cell, (data) =>
            {
                PassReply(data);
                UserPayFromPayCellEvent?.Invoke(data.ConvertBodyTo<UserPayFromPayCellEventArgs>(),Replier.Create(data));
            });

            //卡券审核事件
            handlerList.Add(MessageType.Event, ReceiveEventType.card_pass_check, (data) =>
            {
                PassReply(data);
                CardPassCheckEvent?.Invoke(data.ConvertBodyTo<CardPassCheckEventArgs>(),Replier.Create(data));
            });

            //卡券领取事件
            handlerList.Add(MessageType.Event, ReceiveEventType.user_get_card, (data) =>
            {
                PassReply(data);
                UserGetCardEvent?.Invoke(data.ConvertBodyTo<UserGetCardEventArgs>(),Replier.Create(data));
            });

            //卡券转赠事件
            handlerList.Add(MessageType.Event, ReceiveEventType.user_gifting_card, (data) =>
            {
                PassReply(data);
                UserGiftingCardEvent?.Invoke(data.ConvertBodyTo<UserGiftingCardEventArgs>(),Replier.Create(data));
            });

            //卡券删除事件
            handlerList.Add(MessageType.Event, ReceiveEventType.user_del_card, (data) =>
            {
                PassReply(data);
                UserDelCardEvent?.Invoke(data.ConvertBodyTo<UserDelCardEventArgs>(),Replier.Create(data));
            });

            //卡券核销事件
            handlerList.Add(MessageType.Event, ReceiveEventType.user_consume_card, (data) =>
            {
                PassReply(data);
                UserConsumeCardEvent?.Invoke(data.ConvertBodyTo<UserConsumeCardEventArgs>(),Replier.Create(data));
            });

            //进入会员卡事件
            handlerList.Add(MessageType.Event, ReceiveEventType.user_view_card, (data) =>
            {
                PassReply(data);
                UserViewCardEvent?.Invoke(data.ConvertBodyTo<UserViewCardEventArgs>(),Replier.Create(data));
            });

            //从卡券进入公众号会话事件
            handlerList.Add(MessageType.Event, ReceiveEventType.user_enter_session_from_card, (data) =>
            {
                PassReply(data);
                UserEnterSessionFromCardEvent?.Invoke(data.ConvertBodyTo<UserEnterSessionFromCardEventArgs>(),Replier.Create(data));
            });

            //会员卡内容更新事件
            handlerList.Add(MessageType.Event, ReceiveEventType.update_member_card, (data) =>
            {
                PassReply(data);
                UpdateMemberCardEvent?.Invoke(data.ConvertBodyTo<UpdateMemberCardEventArgs>(),Replier.Create(data));
            });

            //会员卡激活事件推送
            handlerList.Add(MessageType.Event, ReceiveEventType.submit_membercard_user_info, (data) =>
            {
                PassReply(data);
                SubmitMemberCardUserInfoEvent?.Invoke(data.ConvertBodyTo<SubmitMemberCardUserInfoEventArgs>(),Replier.Create(data));
            });

            //库存报警事件
            handlerList.Add(MessageType.Event, ReceiveEventType.card_sku_remind, (data) =>
            {
                PassReply(data);
                CardSkuRemindEvent?.Invoke(data.ConvertBodyTo<CardSkuRemindEventArgs>(),Replier.Create(data));
            });

            //券点流水详情事件
            handlerList.Add(MessageType.Event, ReceiveEventType.card_pay_order, (data) =>
            {
                PassReply(data);
                CardPayOrderEvent?.Invoke(data.ConvertBodyTo<CardPayOrderEventArgs>(),Replier.Create(data));
            });

            #endregion
        }

        /// <summary>
        /// 接收微信推送的消息
        /// </summary>
        public void ReceiveData()
        {
            var context = HttpContext.Current;
            var response = context.Response;
            response.Clear();

            //获取微信推送的消息
            var data = GetRequestData(context);
            if (!CheckMessage(data)) throw new WeixinException("消息签名验证失败");

            if (!string.IsNullOrEmpty(data.Echostr))
            {
                response.Write(data.Echostr);
                response.End();
            }

            ReceiveDataHandle(data);
        }

        /// <summary>
        /// 处理微信推送的消息
        /// </summary>
        /// <param name="data"></param>
        private void ReceiveDataHandle(RequestData data)
        {
            var receiveInfo = data.ConvertBodyTo<EventArgsBase>();
            if (receiveInfo == null) return;

            var msgType = Enums.GetInstance<MessageType>(receiveInfo.MsgType);
            var eventType = Enums.GetInstance(receiveInfo.Event, ReceiveEventType.None);

            var action = handlerList[msgType, eventType];
            if (action == null)
                PassReply(data);
            else
                action.Invoke(data);
        }

        /// <summary>
        /// 获取微信推送的消息
        /// </summary>
        /// <param name="request"></param>
        private RequestData GetRequestData(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            var data = new RequestData();
            data.Signature = request.QueryString["signature"];
            data.Timestamp = request.QueryString["timestamp"];
            data.Nonce = request.QueryString["nonce"];
            data.Echostr = request.QueryString["echostr"].To("");
            data.OpenID = request.QueryString["openid"];
            data.Response = response;

            using (Stream stream = request.InputStream)
            {
                byte[] postBytes = new byte[stream.Length];
                stream.Read(postBytes, 0, (int)stream.Length);
                data.ReceiveBody = Encoding.UTF8.GetString(postBytes);
            }

            return data;
        }

        /// <summary>
        /// 消息签名验证
        /// </summary>
        /// <param name="msg"></param>
        private bool CheckMessage(RequestData msg)
        {
            if (string.IsNullOrEmpty(msg.Nonce)
                || string.IsNullOrEmpty(msg.Timestamp)) return false;

            ArrayList al = new ArrayList();
            al.Add(msg.Nonce);
            al.Add(msg.Timestamp);
            al.Add(Token);
            al.Sort();
            string s = string.Join("", al.ToArray());

            return msg.Signature == Encrypt.Sha1(s).ToLower();
        }

        private void PassReply(RequestData msg)
        {
            msg.Response.Clear();
            msg.Response.End();
        }
    }
}
