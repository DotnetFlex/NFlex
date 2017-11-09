using NFlex.Opens.Weixin.PushMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin
{
    public class EventDelegate
    {
        //文本消息
        public delegate void ReceiveTextEventHandler(TextMessage message, Replier replier);
        //图片消息
        public delegate void ReceiveImageEventHandler(ImageMessage message, Replier replier);
        //语音消息
        public delegate void ReceiveVoiceEventHandler(VoiceMessage message, Replier replier);
        //视频消息
        public delegate void ReceiveVideoEventHandler(VideoMessage message, Replier replier);
        //小视频消息
        public delegate void ReceiveShortVideoEventHandler(ShortVideoMessage message, Replier replier);
        //地理位置消息
        public delegate void ReceiveLocationEventHandler(LocationMessage message, Replier replier);
        //链接消息
        public delegate void ReceiveLinkEventHandler(LinkMessage message, Replier replier);


        //公众号被关注事件
        public delegate void SubscribeEventHandler(ScanEventArgs eventArgs, Replier replier);
        //取消关注事件
        public delegate void UnSubscribeEventHandler(EventArgsBase eventArgs);
        //扫描带参数二维码事件
        public delegate void ScanEventHandler(ScanEventArgs eventArgs, Replier replier);
        //上报地理位置事件
        public delegate void LocationEventHandler(LocationEventArgs eventArgs);
        //自定义菜单事件
        public delegate void MenuClickEventHandler(WithKeyEventArgs eventArgs, Replier replier);
        //点击菜单跳转链接时的事件推送
        public delegate void ViewEventHandler(WithKeyEventArgs eventArgs);
        //群发任务完成事件
        public delegate void MessageEndJobFinishEventHandler(MessageEndJobFinishEventArgs eventArgs);
        //模板消息发送完成事件
        public delegate void TemplateSendJobFinishEventHandler(TemplateSendJobFinishEventArgs  eventArgs);
        //买单事件
        public delegate void UserPayFromPayCellHandler(UserPayFromPayCellEventArgs eventArgs,Replier replier);
        //卡券审核事件
        public delegate void CardPassCheckHandler(CardPassCheckEventArgs eventArgs,Replier replier);
        //卡券领取事件
        public delegate void UserGetCardHandler(UserGetCardEventArgs eventArgs,Replier replier);
        //卡券转赠事件
        public delegate void UserGiftingCardHandler(UserGiftingCardEventArgs eventArgs,Replier replier);
        //卡券删除事件
        public delegate void UserDelCardHandler(UserDelCardEventArgs eventArgs,Replier replier);
        //卡券核销事件
        public delegate void UserConsumeCardHandler(UserConsumeCardEventArgs eventArgs,Replier replier);
        //进入会员卡事件
        public delegate void UserViewCardHandler(UserViewCardEventArgs eventArgs,Replier replier);
        //从卡券进入公众号会话事件
        public delegate void UserEnterSessionFromCardHandler(UserEnterSessionFromCardEventArgs eventArgs,Replier replier);
        //会员卡内容更新事件
        public delegate void UpdateMemberCardHandler(UpdateMemberCardEventArgs eventArgs,Replier replier);
        //会员卡库存报警事件
        public delegate void CardSkuRemindHandler(CardSkuRemindEventArgs eventArgs,Replier replier);
        //券点流水详情事件
        public delegate void CardPayOrderHandler(CardPayOrderEventArgs eventArgs,Replier replier);
        //会员卡激活事件
        public delegate void SubmitMemberCardUserInfoHandler(SubmitMemberCardUserInfoEventArgs eventArgs,Replier replier);


    }
}
