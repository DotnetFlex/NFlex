using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin
{
    /// <summary>
    /// 微信服务器推送的消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        Text,

        /// <summary>
        /// 图片消息
        /// </summary>
        Image,

        /// <summary>
        /// 语音消息
        /// </summary>
        Voice,

        /// <summary>
        /// 视频消息
        /// </summary>
        Video,

        /// <summary>
        /// 小视频消息
        /// </summary>
        ShortVideo,

        /// <summary>
        /// 地理位置消息
        /// </summary>
        Location,

        /// <summary>
        /// 链接消息
        /// </summary>
        Link,

        /// <summary>
        /// 事件
        /// </summary>
        Event,

        /// <summary>
        /// 音乐消息
        /// </summary>
        Music,

        /// <summary>
        /// 新闻消息
        /// </summary>
        News,

        /// <summary>
        /// 群发新闻消息
        /// </summary>
        MpNews,

        /// <summary>
        /// 群发视频消息
        /// </summary>
        MpVideo,

        /// <summary>
        /// 卡券消息
        /// </summary>
        WxCard
    }

    /// <summary>
    /// 微信服务器推送的事件类型
    /// </summary>
    public enum ReceiveEventType
    {
        /// <summary>
        /// 无事件
        /// </summary>
        None,

        /// <summary>
        /// 关注事件
        /// </summary>
        Subscribe,

        /// <summary>
        /// 取消关注事件
        /// </summary>
        UnSubscribe,

        /// <summary>
        /// 扫描带参数二维码事件
        /// </summary>
        Scan,

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        Location,

        /// <summary>
        /// 点击菜单拉取消息时的事件推送
        /// </summary>
        Click,

        /// <summary>
        /// 点击菜单跳转链接时的事件推送
        /// </summary>
        View,

        /// <summary>
        /// 群发消息完成事件
        /// </summary>
        MessageEndJobFinish,

        /// <summary>
        /// 模板消息发送完成事件
        /// </summary>
        TemplateSendJobFinish,

        /// <summary>
        /// 买单事件推送
        /// </summary>
        user_pay_from_pay_cell,
        /// <summary>
        /// 卡券审核事件
        /// </summary>
        card_pass_check,
        /// <summary>
        /// 卡券领取事件
        /// </summary>
        user_get_card,
        /// <summary>
        /// 卡券转赠事件
        /// </summary>
        user_gifting_card,
        /// <summary>
        /// 卡券删除事件
        /// </summary>
        user_del_card,
        /// <summary>
        /// 卡券核销事件
        /// </summary>
        user_consume_card,
        /// <summary>
        /// 进入会员卡事件
        /// </summary>
        user_view_card,
        /// <summary>
        /// 从卡券进入公众号会话事件
        /// </summary>
        user_enter_session_from_card,
        /// <summary>
        /// 会员卡内容更新事件
        /// </summary>
        update_member_card,
        /// <summary>
        /// 库存报警事件
        /// </summary>
        card_sku_remind,
        /// <summary>
        /// 券点流水详情事件
        /// </summary>
        card_pay_order,
        /// <summary>
        /// 会员卡激活事件
        /// </summary>
        submit_membercard_user_info
    }

    /// <summary>
    /// 行业类型
    /// </summary>
    public enum IndustryType
    {
        IT科技_互联网_电子商务 = 1,
        IT科技_IT软件与服务 = 2,
        IT科技_IT硬件与设备 = 3,
        IT科技_电子技术 = 4,
        IT科技_通信与运营商 = 5,
        IT科技_网络游戏 = 6,
        金融业_银行 = 7,
        金融业_基金_理财_信托 = 8,
        金融业_保险 = 9,
        餐饮_餐饮 = 10,
        酒店旅游_酒店 = 11,
        酒店旅游_旅游 = 12,
        运输与仓储_快递 = 13,
        运输与仓储_物流 = 14,
        运输与仓储_仓储 = 15,
        教育_培训 = 16,
        教育_院校 = 17,
        政府与公共事业_学术科研 = 18,
        政府与公共事业_交警 = 19,
        政府与公共事业_博物馆 = 20,
        政府与公共事业_公共事业_非盈利机构 = 21,
        医药护理_医药医疗 = 22,
        医药护理_护理美容 = 23,
        医药护理_保健与卫生 = 24,
        交通工具_汽车相关 = 25,
        交通工具_摩托车相关 = 26,
        交通工具_火车相关 = 27,
        交通工具_飞机相关 = 28,
        房地产_建筑 = 29,
        房地产_物业 = 30,
        消费品_消费品 = 31,
        商业服务_法律 = 32,
        商业服务_会展 = 33,
        商业服务_中介服务 = 34,
        商业服务_认证 = 35,
        商业服务_审计 = 36,
        文体娱乐_传媒 = 37,
        文体娱乐_体育 = 38,
        文体娱乐_娱乐休闲 = 39,
        印刷_印刷 = 40,
        其它_其它 = 41
    }

    /// <summary>
    /// 菜单类型
    /// </summary>
    public static class ButtonType
    {
        /// <summary>
        /// 点击推事件
        /// </summary>
        public static readonly string Click = "click";
        /// <summary>
        /// URL跳转
        /// </summary>
        public static readonly string View = "view";
        /// <summary>
        /// 扫码带提示
        /// </summary>
        public static readonly string ScancodeWaitMsg = "scancode_waitmsg";
        /// <summary>
        /// 扫码推事件
        /// </summary>
        public static readonly string Scancode_Push = "scancode_push";
        /// <summary>
        /// 系统拍照发图
        /// </summary>
        public static readonly string PicSysPhoto = "pic_sysphoto";
        /// <summary>
        /// 拍照或者相册发图
        /// </summary>
        public static readonly string PicPhotoOrAlbum = "pic_photo_or_album";
        /// <summary>
        /// 微信相册发图
        /// </summary>
        public static readonly string PicWeixin = "pic_weixin";
        /// <summary>
        /// 发送位置
        /// </summary>
        public static readonly string LocationSelect = "location_select";
        /// <summary>
        /// 图片
        /// </summary>
        public static readonly string MediaId = "media_id";
        /// <summary>
        /// 图文消息
        /// </summary>
        public static readonly string ViewLimited = "view_limited";
    }

    /// <summary>
    /// 微信账号类型
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// OpenID
        /// </summary>
        OpenID,
        /// <summary>
        /// 微信号
        /// </summary>
        WxName
    }

    /// <summary>
    /// 资源类型
    /// </summary>
    public enum MediaType
    {
        /// <summary>
        /// 图片
        /// </summary>
        Image,
        /// <summary>
        /// 语音
        /// </summary>
        Voice,
        /// <summary>
        /// 视频
        /// </summary>
        Video,
        /// <summary>
        /// 缩略图
        /// </summary>
        Thumb
    }

    /// <summary>
    /// 应用授权作用域
    /// </summary>
    public enum ScopeType
    {
        /// <summary>
        /// 不弹出授权页面，直接跳转，只能获取用户 OpenId
        /// </summary>
        snsapi_base,

        /// <summary>
        /// 弹出授权页面，可通过OpenId拿到用户基本信息，即使在未关注下，只要用户授权也能获取其信息
        /// </summary>
        snsapi_userinfo
    }

    public enum CardType
    {
        /// <summary>
        /// 团购券
        /// </summary>
        GROUPON,
        /// <summary>
        /// 代金券
        /// </summary>
        CASH,
        /// <summary>
        /// 折扣券
        /// </summary>
        DISCOUNT,
        /// <summary>
        /// 兑换券
        /// </summary>
        GIFT,
        /// <summary>
        /// 优惠券
        /// </summary>
        GENERAL_COUPON
    }
    /// <summary>
    /// 卡券背景颜色
    /// </summary>
    public enum CardBgColor
    {
        /// <summary>
        /// #63b359（绿）
        /// </summary>
        Color010,
        /// <summary>
        /// #2c9f67（深绿）
        /// </summary>
        Color020,
        /// <summary>
        /// #509fc9（蓝）
        /// </summary>
        Color030,
        /// <summary>
        /// #5885cf（深蓝）
        /// </summary>
        Color040,
        /// <summary>
        /// #9062c0（紫）
        /// </summary>
        Color050,
        /// <summary>
        /// #d09a45（土黄）
        /// </summary>
        Color060,
        /// <summary>
        /// #e4b138（黄）
        /// </summary>
        Color070,
        /// <summary>
        /// #ee903c（橙）
        /// </summary>
        Color080,
        /// <summary>
        /// #f08500
        /// </summary>
        Color081,
        /// <summary>
        /// #a9d92d  
        /// </summary>
        Color082,
        /// <summary>
        /// #dd6549（橙红）
        /// </summary>
        Color090,
        /// <summary>
        /// #cc463d（红）
        /// </summary>
        Color100,
        /// <summary>
        /// #cf3e36(枣红）
        /// </summary>
        Color101,
        /// <summary>
        /// #5E6671（灰）
        /// </summary>
        Color102
    }

    /// <summary>
    /// 卡券码型
    /// </summary>
    public enum CardCodeType
    {
        /// <summary>
        /// 文本
        /// </summary>
        CODE_TYPE_TEXT,

        /// <summary>
        /// 一维码
        /// </summary>
        CODE_TYPE_BARCODE,

        /// <summary>
        /// 二维码
        /// </summary>
        CODE_TYPE_QRCODE,

        /// <summary>
        /// 二维码无code显示
        /// </summary>
        CODE_TYPE_ONLY_QRCODE,

        /// <summary>
        /// 一维码无code显示
        /// </summary>
        CODE_TYPE_ONLY_BARCODE,

        /// <summary>
        /// 不显示code和条形码类型
        /// </summary>
        CODE_TYPE_NONE
    }

    /// <summary>
    /// 卡券有效期类型
    /// </summary>
    public enum CardDateType
    {
        /// <summary>
        /// 固定日期区间
        /// </summary>
        DATE_TYPE_FIX_TIME_RANGE,
        /// <summary>
        /// 固定时长（自领取后按天算）
        /// </summary>
        DATE_TYPE_FIX_TERM
    }

    /// <summary>
    /// 商家服务类型
    /// </summary>
    public enum BusinessServiceType
    {
        /// <summary>
        /// 外卖服务
        /// </summary>
        BIZ_SERVICE_DELIVER,
        /// <summary>
        /// 停车位
        /// </summary>
        BIZ_SERVICE_FREE_PARK,
        /// <summary>
        /// 可带宠物
        /// </summary>
        BIZ_SERVICE_WITH_PET,
        /// <summary>
        /// 免费Wifi
        /// </summary>
        BIZ_SERVICE_FREE_WIFI
    }

    public enum TimeLimitType
    {
        /// <summary>
        /// 周一
        /// </summary>
        MONDAY,
        /// <summary>
        /// 周二
        /// </summary>
        TUESDAY,
        /// <summary>
        /// 周三
        /// </summary>
        WEDNESDAY,
        /// <summary>
        /// 周四 
        /// </summary>
        THURSDAY,
        /// <summary>
        /// 周五 
        /// </summary>
        FRIDAY,
        /// <summary>
        /// 周六 
        /// </summary>
        SATURDAY,
        /// <summary>
        /// 周日 
        /// </summary>
        SUNDAY
    }

    /// <summary>
    /// 货架投放页面场景
    /// </summary>
    public enum LandingPageScene
    {
        /// <summary>
        /// 附近
        /// </summary>
        SCENE_NEAR_BY,
        /// <summary>
        /// 自定义菜单
        /// </summary>
        SCENE_MENU,
        /// <summary>
        /// 二维码
        /// </summary>
        SCENE_QRCODE,
        /// <summary>
        /// 公众号文章
        /// </summary>
        SCENE_ARTICLE,
        /// <summary>
        /// h5页面
        /// </summary>
        SCENE_H5,
        /// <summary>
        /// 自动回复
        /// </summary>
        SCENE_IVR,
        /// <summary>
        /// 卡券自定义cell
        /// </summary>
        SCENE_CARD_CUSTOM_CELL
    }

    /// <summary>
    /// 用户卡券状态
    /// </summary>
    public enum UserCardStatus
    {
        /// <summary>
        /// 正常 
        /// </summary>
        NORMAL,
        /// <summary>
        /// 已核销 
        /// </summary>
        CONSUMED,
        /// <summary>
        /// 已过期
        /// </summary>
        EXPIRE,
        /// <summary>
        /// 转赠中
        /// </summary>
        GIFTING,
        /// <summary>
        /// 转赠超时
        /// </summary>
        GIFT_TIMEOUT,
        /// <summary>
        /// 已删除
        /// </summary>
        DELETE,
        /// <summary>
        /// 已失效 
        /// </summary>
        UNAVAILABLE
    }

    /// <summary>
    /// 核销来源
    /// </summary>
    public enum ConsumeSource
    {
        /// <summary>
        /// API核销
        /// </summary>
        FROM_API,
        /// <summary>
        /// 公众平台核销
        /// </summary>
        FROM_MP,
        /// <summary>
        /// 卡券商户助手核销
        /// </summary>
        FROM_MOBILE_HELPER
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    public enum CardPayOrderStatus
    {
        /// <summary>
        /// 等待支付
        /// </summary>
        ORDER_STATUS_WAITING,

        /// <summary>
        /// 支付成功
        /// </summary>
        ORDER_STATUS_SUCC,

        /// <summary>
        /// 加代币成功
        /// </summary>
        ORDER_STATUS_FINANCE_SUCC,

        /// <summary>
        /// 加库存成功
        /// </summary>
        ORDER_STATUS_QUANTITY_SUCC,

        /// <summary>
        /// 已退币
        /// </summary>
        ORDER_STATUS_HAS_REFUND,

        /// <summary>
        /// 等待退币确认
        /// </summary>
        ORDER_STATUS_REFUND_WAITING,

        /// <summary>
        /// 已回退,系统失败
        /// </summary>
        ORDER_STATUS_ROLLBACK,

        /// <summary>
        /// 已开发票
        /// </summary>
        ORDER_STATUS_HAS_RECEIPT

    }

    /// <summary>
    /// 订单类型
    /// </summary>
    public enum CardPayOrderType
    {
        /// <summary>
        /// 平台赠送券点
        /// </summary>
        ORDER_TYPE_SYS_ADD,

        /// <summary>
        /// 充值券点 
        /// </summary>
        ORDER_TYPE_WXPAY,

        /// <summary>
        /// 库存未使用回退券点
        /// </summary>
        ORDER_TYPE_REFUND,

        /// <summary>
        /// 券点兑换库存
        /// </summary>
        ORDER_TYPE_REDUCE,

        /// <summary>
        /// 平台扣减
        /// </summary>
        ORDER_TYPE_SYS_REDUCE
    }

    /// <summary>
    /// 卡券状态
    /// </summary>
    public enum CardStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        CARD_STATUS_NOT_VERIFY,

        /// <summary>
        /// 审核失败
        /// </summary>
        CARD_STATUS_VERIFY_FAIL,

        /// <summary>
        /// 通过审核
        /// </summary>
        CARD_STATUS_VERIFY_OK,

        /// <summary>
        /// 卡券被商户删除
        /// </summary>
        CARD_STATUS_DELETE,

        /// <summary>
        /// 在公众平台投放过的卡券
        /// </summary>
        CARD_STATUS_DISPATCH

    }

}
