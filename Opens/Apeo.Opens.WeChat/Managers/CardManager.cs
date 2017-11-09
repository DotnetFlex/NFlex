using NFlex.Opens.Weixin.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Managers
{
    public class CardManager:ManagerBase
    {
        internal CardManager(string apiUrl, AccessTokenContainer tokenContainer) : base(apiUrl, tokenContainer) {}

        #region 创建卡券
        /// <summary>
        /// 创建团购券
        /// </summary>
        /// <param name="dealDetail">团购详情</param>
        /// <param name="required">必填信息</param>
        /// <param name="baseInfo">基本信息</param>
        /// <param name="advance">高级信息</param>
        public CreateCardResult CreateGrouponCard(string dealDetail, RequiredInfo required, BaseInfo baseInfo = null, AdvanceInfo advance = null)
        {
            dynamic card = new DynamicObject();
            card.deal_detail = dealDetail;
            return CreateCard(CardType.GROUPON, card, required, baseInfo, advance);
        }

        /// <summary>
        /// 创建代金券
        /// </summary>
        /// <param name="leastCost">起用金额（单位为分）,如果无起用门槛则填0</param>
        /// <param name="reduceCost">减免金额。（单位为分）</param>
        /// <param name="required">必填信息</param>
        /// <param name="baseInfo">基本信息</param>
        /// <param name="advance">高级信息</param>
        public CreateCardResult CreateCashCard(int leastCost, int reduceCost, RequiredInfo required, BaseInfo baseInfo = null, AdvanceInfo advance = null)
        {
            dynamic card = new DynamicObject();
            card.least_cost = leastCost;
            card.reduce_cost = reduceCost;
            return CreateCard(CardType.CASH, card, required, baseInfo, advance);
        }

        /// <summary>
        /// 创建折扣券
        /// </summary>
        /// <param name="discount">打折额度（百分比）。填30就是七折</param>
        /// <param name="required">必填信息</param>
        /// <param name="baseInfo">基本信息</param>
        /// <param name="advance">高级信息</param>
        public CreateCardResult CreateDiscountCard(int discount, RequiredInfo required, BaseInfo baseInfo = null, AdvanceInfo advance = null)
        {
            dynamic card = new DynamicObject();
            card.discount = discount;
            return CreateCard(CardType.DISCOUNT, card, required, baseInfo, advance);
        }

        /// <summary>
        /// 创建兑换券
        /// </summary>
        /// <param name="gift">兑换内容的名称</param>
        /// <param name="required">必填信息</param>
        /// <param name="baseInfo">基本信息</param>
        /// <param name="advance">高级信息</param>
        public CreateCardResult CreateGiftCard(string gift, RequiredInfo required, BaseInfo baseInfo = null, AdvanceInfo advance = null)
        {
            dynamic card = new DynamicObject();
            card.gift = gift;
            return CreateCard(CardType.GIFT, card, required, baseInfo, advance);
        }

        /// <summary>
        /// 创建优惠券
        /// </summary>
        /// <param name="defaultDetail">优惠详情</param>
        /// <param name="required">必填信息</param>
        /// <param name="baseInfo">基本信息</param>
        /// <param name="advance">高级信息</param>
        public CreateCardResult CreateGeneralCouponCard(string defaultDetail, RequiredInfo required, BaseInfo baseInfo = null, AdvanceInfo advance = null)
        {
            dynamic card = new DynamicObject();
            card.default_detail = defaultDetail;
            return CreateCard(CardType.GENERAL_COUPON, card, required, baseInfo, advance);
        }

        /// <summary>
        /// 创建卡券
        /// </summary>
        private CreateCardResult CreateCard(CardType cardType, dynamic card, RequiredInfo required, BaseInfo baseInfo, AdvanceInfo advance)
        {
            var dic = new Dictionary<string, object>();
            dic.Add("card_type", cardType.ToString());
            dic.Add(cardType.ToString().ToLower(), card);
            FillBaseInfo(card, required, baseInfo);
            FillAdvanceInfo(card, advance);
            var data = new
            {
                card = dic
            };
            return PostJson<CreateCardResult>("/card/create", data);
        }

        /// <summary>
        /// 填充卡券基本信息
        /// </summary>
        private void FillBaseInfo(dynamic card, RequiredInfo required, BaseInfo baseInfo)
        {
            card.base_info = new DynamicObject();
            card.base_info.logo_url = required.LogoUrl;
            card.base_info.code_type = required.CodeType.ToString();
            card.base_info.brand_name = required.BrandName;
            card.base_info.title = required.Title;
            card.base_info.color = required.Color.ToString();
            card.base_info.notice = required.Notice;
            card.base_info.description = required.Description;
            card.base_info.sku = new { quantity = required.Quantity };
            card.base_info.date_info = new
            {
                type = required.DateInfo.Type.ToString(),
                begin_timestamp = required.DateInfo.BeginTime.ToTimestamp(),
                end_timestamp = required.DateInfo.EndTime.ToTimestamp(),
                fixed_term = required.DateInfo.FixedTerm,
                fixed_begin_term = required.DateInfo.FixedBeginTerm
            };

            if (baseInfo != null)
            {
                card.base_info.use_custom_code = baseInfo.UseCustomCode;
                card.base_info.get_custom_code_mode = baseInfo.GetCustomCodeMode;
                card.base_info.bind_openid = baseInfo.BindOpenId;
                card.base_info.service_phone = baseInfo.ServicePhone;
                card.base_info.location_id_list = baseInfo.LocationIdList;
                card.base_info.use_all_locations = baseInfo.UseAllLocation;
                card.base_info.center_title = baseInfo.CenterTitle;
                card.base_info.center_sub_title = baseInfo.CenterSubTitle;
                card.base_info.center_url = baseInfo.CenterUrl;
                card.base_info.center_app_brand_user_name = baseInfo.CenterAppBrandUserName;
                card.base_info.center_app_brand_pass = baseInfo.CenterAppBrandPath;
                card.base_info.custom_url_name = baseInfo.CustomUrlName;
                card.base_info.custom_url = baseInfo.CustomUrl;
                card.base_info.custom_url_sub_title = baseInfo.CustomUrlSubTitle;
                card.base_info.custom_app_brand_user_name = baseInfo.CustomAppBrandUserName;
                card.base_info.custom_app_brand_pass = baseInfo.CustomAppBrandPath;
                card.base_info.promotion_url_name = baseInfo.PromotionUrlName;
                card.base_info.promotion_url = baseInfo.PromotionUrl;
                card.base_info.promotion_url_sub_title = baseInfo.PromotionUrlSubTitle;
                card.base_info.promotion_app_brand_user_name = baseInfo.PromotionAppBrandUserName;
                card.base_info.promotion_app_brand_pass = baseInfo.PromotionAppBrandPath;
                card.base_info.get_limit = baseInfo.GetLimit;
                card.base_info.use_limit = baseInfo.UseLimit;
                card.base_info.can_share = baseInfo.CanShare;
                card.base_info.can_give_friend = baseInfo.CanGiveFriend;
            }
        }

        private void FillAdvanceInfo(dynamic card, AdvanceInfo info)
        {
            if (info == null) return;

            card.advanced_info = new DynamicObject();

            if (info.UseCondition != null)
            {
                card.advanced_info.use_condition = new
                {
                    accept_category = info.UseCondition.AcceptCategory,
                    reject_category = info.UseCondition.RejectCategory,
                    least_cost = info.UseCondition.LeastCost,
                    object_use_for = info.UseCondition.ObjectUseFor,
                    can_use_with_other_discount = info.UseCondition.CanUseWithOtherDiscount
                };
            }

            if (info.Abstract != null)
            {
                card.advanced_info.@abstract = new
                {
                    @abstract = info.Abstract.Abstract,
                    icon_url_list = info.Abstract.IconUrlList
                };
            }

            if (info.TextImageList != null)
            {
                card.advanced_info.text_image_list = info.TextImageList.Select(t => new
                {
                    image_url = t.ImageUrl,
                    text = t.Text
                });
            }

            if (info.BusinessServices != null)
            {
                card.advanced_info.business_service = info.BusinessServices.Select(t => t.ToString());
            }

            if (info.TimeLimit != null)
            {
                card.advanced_info.time_limit = info.TimeLimit.Select(t => new
                {
                    type = t.Type.ToString(),
                    begin_hour = t.BeginHour,
                    begin_minute = t.BeginMinute,
                    end_hour = t.EndHour,
                    end_minute = t.EndMinute
                });
            }
        }
        #endregion

        #region 设置买单功能
        /// <summary>
        /// 设置买单接口
        /// <para>设置快速买单的卡券须支持至少一家有核销员门店，否则无法设置成功</para>
        /// <para>该卡券设置了center_url（居中使用跳转链接）,须先将该设置更新为空后再设置自快速买单方可生效</para>
        /// </summary>
        /// <param name="cardId">卡券ID</param>
        /// <param name="isOpen">是否开启买单功能</param>
        public Result SetPayCell(string cardId,bool isOpen)
        {
            var data = new
            {
                card_id = cardId,
                is_open = isOpen
            };
            return PostJson("/card/paycell/set", data);
        }
        #endregion

        #region 创建卡券投放二维码
        /// <summary>
        /// 创建卡券投放二维码
        /// </summary>
        /// <param name="cards">设置卡券信息，设置多个时代表扫一次码领取多张优惠券（最少1个，最多5个）</param>
        /// <param name="expireSeconds">指定二维码的有效时间，范围是60 ~ 1800秒。为null默认为365天有效</param>
        public CreateCardQrcodeResult CreateCardQrcode(IEnumerable<CreateQrcodeCardInfo> cards, int? expireSeconds=null)
        {
            dynamic data = new DynamicObject();
            data.action_name = cards.Count() > 1 ? "QR_MULTIPLE_CARD" : "QR_CARD";
            if (expireSeconds != null)
                data.expire_seconds = expireSeconds.Value;

            data.action_info = new DynamicObject();
            if(cards.Count()>1)
            {
                data.action_info.multiple_card = new
                {
                    card_list = cards.Select(t => new
                    {
                        card_id = t.CardId,
                        code = t.Code,
                        openid = t.OpenId,
                        is_unique_code = t.IsUniqueCode,
                        outer_str = t.OuterStr
                    })
                };
            }
            else
            {
                var card = cards.FirstOrDefault();
                if (card == null) throw new ArgumentException("参数 cards 集合最少要包含 1 个值");
                data.action_info.card = new
                {
                    card_id = card.CardId,
                    code = card.Code,
                    openid = card.OpenId,
                    is_unique_code = card.IsUniqueCode,
                    outer_str = card.OuterStr
                };
            }

            return PostJson<CreateCardQrcodeResult>("/card/qrcode/create", data);
        }

        /// <summary>
        /// 创建卡券投放二维码
        /// </summary>
        /// <param name="cardIds">卡券编号集合，此方法只能创建use_custom_code字段为false的卡券</param>
        /// <param name="expireSeconds">指定二维码的有效时间，范围是60 ~ 1800秒。为null默认为365天有效</param>
        public CreateCardQrcodeResult CreateCardQrcode(IEnumerable<string> cardIds,int? expireSeconds=null)
        {
            return CreateCardQrcode(cardIds.Select(t => new CreateQrcodeCardInfo { CardId = t }), expireSeconds);
        }

        public CreateCardQrcodeResult CreateCardQrcode(string cardId,int? expireSeconds=null)
        {
            return CreateCardQrcode(new List<string> { cardId }, expireSeconds);
        }
        #endregion

        #region 创建货架
        /// <summary>
        /// 创建货架
        /// </summary>
        /// <param name="banner">页面的banner图片链接，须调用，建议尺寸为640*300。</param>
        /// <param name="title">页面的title</param>
        /// <param name="canShare">页面是否可以分享</param>
        /// <param name="scene">投放页面的场景值</param>
        /// <param name="cards">卡券列表</param>
        public CreateLandingPageResult CreateLandingPage(string banner,string title,bool canShare, LandingPageScene scene,params LandingPageCardInfo[] cards)
        {
            var data = new
            {
                banner = banner,
                page_title = title,
                can_share = canShare,
                scene = scene.ToString(),
                card_list = cards.Select(t => new {
                    card_id = t.CardId,
                    thumb_url = t.ThumbUrl
                })
            };
            return PostJson<CreateLandingPageResult>("/card/landingpage/create", data);
        }
        #endregion

        #region 导入卡券Code
        /// <summary>
        /// 导入卡券Code
        /// </summary>
        /// <param name="cardId">需要进行导入code的卡券ID</param>
        /// <param name="codes">需导入微信卡券后台的自定义code，上限为100个</param>
        /// <returns></returns>
        public Result DepositCardCode(string cardId,List<string> codes)
        {
            var data = new
            {
                card_id = cardId,
                code = codes
            };
            return PostJson("/card/code/deposit", data);
        }
        #endregion

        #region 查询已导入卡券Code数量
        /// <summary>
        /// 查询已导入卡券Code数量
        /// </summary>
        /// <param name="cardId">卡券编号</param>
        /// <returns></returns>
        public GetDepositCountResult GetDepositCount(string cardId)
        {
            var data = new { card_id = cardId };
            return PostJson<GetDepositCountResult>("/card/code/getdepositcount", data);
        }
        #endregion

        #region 核查Code
        /// <summary>
        /// 核查Code
        /// </summary>
        /// <param name="cardId">进行导入code的卡券ID</param>
        /// <param name="codes">已经微信卡券后台的自定义code，上限为100个</param>
        public CheckCodeResult CheckCode(string cardId,List<string> codes)
        {
            var data= new
            {
                card_id = cardId,
                code = codes
            };
            return PostJson<CheckCodeResult>("/card/code/checkcode", data);
        }
        #endregion

        #region 获取卡券嵌入图文Html代码
        /// <summary>
        /// 获取卡券嵌入图文代码
        /// </summary>
        /// <param name="cardId">卡券ID</param>
        public GetCardNewsHtmlResult GetCardNewsHtml(string cardId)
        {
            var data = new { card_id = cardId };
            return PostJson<GetCardNewsHtmlResult>("/card/mpnews/gethtml", data);
        }
        #endregion

        #region 设置卡券白名单
        /// <summary>
        /// 设置卡券白名单
        /// </summary>
        public Result SetCardWhiteOpenIds(params string[] openIds)
        {
            var data = new { openid=openIds};
            return PostJson("/card/testwhitelist/set", data);
        }
        /// <summary>
        /// 设置卡券白名单
        /// </summary>
        public Result SetCardWhiteUserNames(params string[] userNames)
        {
            var data = new { username = userNames };
            return PostJson("/card/testwhitelist/set", data);
        }
        #endregion

        #region 获取卡券Code信息
        /// <summary>
        /// 获取卡券Code信息
        /// </summary>
        /// <param name="cardId">卡券ID</param>
        /// <param name="code">卡券Code</param>
        public GetCodeInfoResult GetCodeInfo(string cardId, string code)
        {
            var data = new
            {
                card_id = cardId,
                code = code,
                check_consume = false
            };
            return PostJson<GetCodeInfoResult>("/card/code/get", data);
        }
        #endregion

        #region 核销Code
        /// <summary>
        /// 核销Code
        /// </summary>
        /// <param name="code">需核销的Code码</param>
        /// <param name="cardId">卡券ID。创建卡券时use_custom_code填写true时必填。非自定义Code不必填写。</param>
        public ConsumeCodeResult ConsumeCode(string code, string cardId=null)
        {
            var data = new
            {
                code = code,
                card_id = cardId
            };
            return PostJson<ConsumeCodeResult>("/card/code/consume", data);
        }
        #endregion

        #region Code解码
        /// <summary>
        /// Code解码
        /// </summary>
        /// <param name="encryptCcode">经过加密的Code码</param>
        public DecryptCodeResult DecryptCode(string encryptCcode)
        {
            var data = new { encrypt_code = encryptCcode };
            return PostJson<DecryptCodeResult>("/card/code/decrypt", data);
        }
        #endregion

        #region 获取用户已领取卡券
        /// <summary>
        /// 获取用户已领取卡券
        /// </summary>
        /// <param name="openId">需要查询的用户openid</param>
        /// <param name="cardId">卡券ID。不填写时默认查询当前appid下的卡券</param>
        /// <returns></returns>
        public GetUserCardListResult GetUserCardList(string openId,string cardId=null)
        {
            var data = new
            {
                openid = openId,
                card_id = cardId
            };
            return PostJson<GetUserCardListResult>("/card/user/getcardlist", data);
        }
        #endregion

        #region 查看卡券详情 未完成
        /// <summary>
        /// 查看卡券详情
        /// </summary>
        /// <param name="cardId">卡券ID</param>
        public Result GetCardInfo(string cardId)
        {
            var data = new { card_id = cardId };
            return PostJson<Result>("/card/get", data);
        }
        #endregion

        #region 指查询卡券列表
        /// <summary>
        /// 指查询卡券列表
        /// </summary>
        /// <param name="offset">查询卡列表的起始偏移量，从0开始</para>
        /// <para>即offset: 5是指从从列表里的第六个开始读取</param>
        /// <param name="count">需要查询的卡片的数量（数量最大50）</param>
        /// <param name="status">卡券状态列表</param>
        public BatchGetCardResult BatchGetCard(int offset,int count,params CardStatus[] status)
        {
            var data = new
            {
                offset = offset,
                count = count,
                status_list = status.Select(t => t.ToString())
            };
            return PostJson<BatchGetCardResult>("/card/batchget", data);
        }
        #endregion

        #region 修改卡券库存
        /// <summary>
        /// 修改卡券库存
        /// </summary>
        /// <param name="cardId">卡券ID</param>
        /// <param name="increaseValue">增加多少库存，支持不填或填0</param>
        /// <param name="reduceValue">减少多少库存，可以不填或填0</param>
        public Result ModifyCardStock(string cardId,int increaseValue,int reduceValue)
        {
            var data = new
            {
                card_id = cardId,
                increase_stock_value = increaseValue,
                reduce_stock_value = reduceValue
            };
            return PostJson("/card/modifystock", data);
        }
        #endregion

        #region 更改Code
        /// <summary>
        /// 更改Code
        /// </summary>
        /// <param name="cardId">卡券ID。自定义Code码卡券为必填</param>
        /// <param name="code">需变更的Code码</param>
        /// <param name="newCode">变更后的有效Code码</param>
        public Result UpdateCode(string cardId,string code,string newCode)
        {
            var data = new
            {
                code = code,
                card_id = cardId,
                new_code = newCode
            };
            return PostJson("/card/code/update", data);
        }
        #endregion

        #region 删除卡券
        /// <summary>
        /// 删除卡券
        /// </summary>
        /// <param name="cardId">卡券ID</param>
        public Result DeleteCard(string cardId)
        {
            var data = new { card_id = cardId };
            return PostJson("/card/delete", data);
        }
        #endregion

        #region 设置卡券失效
        /// <summary>
        /// 设置卡券失效
        /// </summary>
        /// <param name="code">设置失效的Code码</param>
        /// <param name="reason">失效理由</param>
        /// <param name="cardId">卡券ID（自定义Code必须填写）</param>
        public Result UnavailableCode(string code,string reason,string cardId="")
        {
            var data = new
            {
                code = code,
                reason = reason,
                card_id = cardId
            };
            return PostJson("/card/code/unavailable", data);
        }
        #endregion

        #region 获取卡券概况数据
        /// <summary>
        /// 摘取卡券概况数据
        /// <para>查询时间区间需小于等于62天，否则报错</para>
        /// <para>该接口只能拉取非当天的数据，不能拉取当天的卡券数据，否则报错</para>
        /// </summary>
        /// <param name="beginDate">查询数据的起始时间</param>
        /// <param name="endDate">查询数据的截至时间</param>
        /// <param name="condSource">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        public GetCardStatisticsResult GetCardStatistics(DateTime beginDate,DateTime endDate,int condSource)
        {
            var data = new
            {
                begin_date = beginDate.ToString("yyyy-MM-dd"),
                end_date = endDate.ToString("yyyy-MM-dd"),
                cond_source = condSource
            };
            return PostJson<GetCardStatisticsResult>("datacube/getcardbizuininfo", data);
        }
        #endregion

        #region 获取免费券数据接口
        /// <summary>
        /// 获取免费券数据接口
        /// <para>（优惠券、团购券、折扣券、礼品券）</para>
        /// </summary>
        /// <param name="beginDate">查询数据的起始时间</param>
        /// <param name="endDate">查询数据的截至时间</param>
        /// <param name="condSource">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <param name="card_id">卡券ID。填写后，指定拉出该卡券的相关数据</param>
        public GetCardStatisticsResult GetFreeCardStatistics(DateTime beginDate,DateTime endDate,int condSource,string cardId="")
        {
            var data = new
            {
                begin_date = beginDate.ToString("yyyy-MM-dd"),
                end_date = endDate.ToString("yyyy-MM-dd"),
                cond_source = condSource,
                card_id=cardId
            };
            return PostJson<GetCardStatisticsResult>("/datacube/getcardcardinfo", data);
        }
        #endregion

        #region 获取会员卡概况数据
        /// <summary>
        /// 获取会员卡概况数据
        /// </summary>
        /// <param name="beginDate">查询数据的起始时间</param>
        /// <param name="endDate">查询数据的截至时间</param>
        /// <param name="condSource">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        public GetMemberCardStatisticsResult GetMemberCardStatistics(DateTime beginDate, DateTime endDate, int condSource)
        {
            var data = new
            {
                begin_date = beginDate.ToString("yyyy-MM-dd"),
                end_date = endDate.ToString("yyyy-MM-dd"),
                cond_source = condSource
            };
            return PostJson<GetMemberCardStatisticsResult>("/datacube/getcardmembercardinfo", data);
        }
        #endregion

        #region 获取单张会员卡数据
        /// <summary>
        /// 获取单张会员卡数据
        /// </summary>
        /// <param name="beginDate">查询数据的起始时间</param>
        /// <param name="endDate">查询数据的截至时间</param>
        /// <param name="cardId">卡券id</param>
        public GetMemberCardDetailResult GetMemberCardDetail(DateTime beginDate, DateTime endDate,string cardId)
        {
            var data = new
            {
                begin_date = beginDate.ToString("yyyy-MM-dd"),
                end_date = endDate.ToString("yyyy-MM-dd"),
                card_id = cardId
            };
            return PostJson<GetMemberCardDetailResult>("/datacube/getcardmembercarddetail", data);
        }
        #endregion

        #region 内部类
        /// <summary>
        /// 卡券基础信息（必填）
        /// </summary>
        public class RequiredInfo
        {
            /// <summary>
            /// 卡券的商户logo，建议像素为300*300。
            /// </summary>
            public string LogoUrl { get; set; }
            /// <summary>
            /// 码型
            /// </summary>
            public CardCodeType CodeType { get; set; }
            /// <summary>
            /// 商户名字,字数上限为12个汉字
            /// </summary>
            public string BrandName { get; set; }
            /// <summary>
            /// 卡券名，字数上限为9个汉字。(建议涵盖卡券属性、服务及金额)
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 券颜色
            /// </summary>
            public CardBgColor Color { get; set; }
            /// <summary>
            /// 卡券使用提醒，字数上限为16个汉字
            /// </summary>
            public string Notice { get; set; }
            /// <summary>
            /// 卡券使用说明，字数上限为1024个汉字
            /// </summary>
            public string Description { get; set; }
            /// <summary>
            /// 卡券库存的数量，上限为100000000
            /// </summary>
            public int Quantity { get; set; }
            /// <summary>
            /// 使用日期，有效期的信息
            /// </summary>
            public DateInfoEntity DateInfo { get; set; }

            /// <summary>
            /// 使用日期，有效期的信息
            /// </summary>
            public class DateInfoEntity
            {
                /// <summary>
                /// 使用时间的类型
                /// </summary>
                public CardDateType Type { get; set; }
                /// <summary>
                /// 表示起用时间（Type=FixRange时专用)
                /// </summary>
                public DateTime BeginTime { get; set; }
                /// <summary>
                /// 表示结束时间
                /// </summary>
                public DateTime EndTime { get; set; }
                /// <summary>
                /// 表示自领取后多少天内有效（Type=FixTerm时专用）
                /// </summary>
                public int FixedTerm { get; set; }
                /// <summary>
                /// 表示自领取后多少天开始生效，领取后当天生效填写0（Type=FixTerm时专用）
                /// </summary>
                public int FixedBeginTerm { get; set; }
            }

        }

        /// <summary>
        /// 卡券基础信息（非必填）
        /// </summary>
        public class BaseInfo
        {
            /// <summary>
            /// 是否自定义Code码，默认为false
            /// </summary>
            public bool UseCustomCode { get; set; }
            /// <summary>
            /// 填入GET_CUSTOM_CODE_MODE_DEPOSIT
            ///<para>表示该卡券为预存code模式卡券，须导入超过库存数目的自定义code后方可投放，</para>
            ///<para>填入该字段后，quantity字段须为0,须导入code后再增加库存</para>
            /// </summary>
            public string GetCustomCodeMode { get; set; }
            /// <summary>
            /// 是否指定用户领取，通常指定特殊用户群体，投放卡券或防止刷券时选择指定用户领取
            /// </summary>
            public bool BindOpenId { get; set; }
            /// <summary>
            /// 客服电话
            /// </summary>
            public string ServicePhone { get; set; }
            /// <summary>
            /// 门店位置poiid,具备线下门店的商户为必填
            /// </summary>
            public List<int> LocationIdList { get; set; }
            /// <summary>
            /// 设置本卡券支持全部门店，与 LocationIdList 互斥
            /// </summary>
            public bool UseAllLocation { get; set; }
            /// <summary>
            /// 卡券顶部居中的按钮，仅在卡券状态正常(可以核销)时显示
            /// </summary>
            public string CenterTitle { get; set; }
            /// <summary>
            /// 显示在入口下方的提示语，仅在卡券状态正常(可以核销)时显示
            /// </summary>
            public string CenterSubTitle { get; set; }
            /// <summary>
            /// 顶部居中的url，仅在卡券状态正常(可以核销)时显示
            /// </summary>
            public string CenterUrl { get; set; }
            /// <summary>
            /// 卡券跳转的小程序的user_name，仅可跳转该公众号绑定的小程序
            /// </summary>
            public string CenterAppBrandUserName { get; set; }
            /// <summary>
            /// 卡券跳转的小程序的path
            /// </summary>
            public string CenterAppBrandPath { get; set; }
            /// <summary>
            /// 自定义跳转外链的入口名字
            /// </summary>
            public string CustomUrlName { get; set; }
            /// <summary>
            /// 自定义跳转的URL
            /// </summary>
            public string CustomUrl { get; set; }
            /// <summary>
            /// 显示在入口右侧的提示语
            /// </summary>
            public string CustomUrlSubTitle { get; set; }
            /// <summary>
            /// 卡券跳转的小程序的user_name，仅可跳转该公众号绑定的小程序
            /// </summary>
            public string CustomAppBrandUserName { get; set; }
            /// <summary>
            /// 卡券跳转的小程序的path
            /// </summary>
            public string CustomAppBrandPath { get; set; }
            /// <summary>
            /// 营销场景的自定义入口名称
            /// </summary>
            public string PromotionUrlName { get; set; }
            /// <summary>
            /// 入口跳转外链的地址链接
            /// </summary>
            public string PromotionUrl { get; set; }
            /// <summary>
            /// 显示在营销入口右侧的提示语
            /// </summary>
            public string PromotionUrlSubTitle { get; set; }
            /// <summary>
            /// 卡券跳转的小程序的user_name，仅可跳转该公众号绑定的小程序
            /// </summary>
            public string PromotionAppBrandUserName { get; set; }
            /// <summary>
            /// 卡券跳转的小程序的path
            /// </summary>
            public string PromotionAppBrandPath { get; set; }
            /// <summary>
            /// 每人可领券的数量限制,不填写默认为50
            /// </summary>
            public int GetLimit { get; set; }
            /// <summary>
            /// 每人可核销的数量限制,不填写默认为50
            /// </summary>
            public int UseLimit { get; set; }
            /// <summary>
            /// 卡券领取页面是否可分享
            /// </summary>
            public bool CanShare { get; set; }
            /// <summary>
            /// 卡券是否可转赠
            /// </summary>
            public bool CanGiveFriend { get; set; }
        }
        /// <summary>
        /// 卡券高级信息
        /// </summary>
        public class AdvanceInfo
        {
            /// <summary>
            /// 使用门槛（条件）字段，若不填写使用条件则
            ///<para>在券面拼写：无最低消费限制，全场通用，不限品类；</para>
            ///<para>并在使用说明显示：可与其他优惠共享</para>
            /// </summary>
            public UseConditionInfo UseCondition { get; set; }
            /// <summary>
            /// 封面摘要结构体名称
            /// </summary>
            public AbstractInfo Abstract { get; set; }
            public List<ImageInfo> TextImageList { get; set; }
            /// <summary>
            /// 商家服务类型
            /// </summary>
            public List<BusinessServiceType> BusinessServices { get; set; }
            /// <summary>
            /// 使用时段限制
            /// </summary>
            public List<TimeLimitInfo> TimeLimit { get; set; }


            /// <summary>
            /// 使用门槛
            /// </summary>
            public class UseConditionInfo
            {
                /// <summary>
                /// 指定可用的商品类目，仅用于代金券类型，填入后将在券面拼写适用于xxx
                /// </summary>
                public string AcceptCategory { get; set; }
                /// <summary>
                /// 指定不可用的商品类目，仅用于代金券类型，填入后将在券面拼写不适用于xxxx
                /// </summary>
                public string RejectCategory { get; set; }
                /// <summary>
                /// 满减门槛字段，可用于兑换券和代金券，填入后将在全面拼写消费满xx元可用
                /// </summary>
                public int LeastCost { get; set; }
                /// <summary>
                /// 购买xx可用类型门槛，仅用于兑换，填入后自动拼写购买xxx可用
                /// </summary>
                public string ObjectUseFor { get; set; }
                /// <summary>
                /// 可不可以与其它优惠共享
                /// </summary>
                public bool CanUseWithOtherDiscount { get; set; }
            }
            /// <summary>
            /// 封面摘要结构体名称
            /// </summary>
            public class AbstractInfo
            {
                /// <summary>
                /// 封面摘要简介
                /// </summary>
                public string Abstract { get; set; }
                /// <summary>
                /// 封面图片列表，仅支持填入一个封面图片链接。建议图片尺寸像素850*350
                /// </summary>
                public List<string> IconUrlList { get; set; }
            }
            /// <summary>
            /// 显示在详情内页，优惠券券开发者须至少传入一组图文列表
            /// </summary>
            public class ImageInfo
            {
                public string ImageUrl { get; set; }
                public string Text { get; set; }
            }
            /// <summary>
            /// 使用时段限制
            /// </summary>
            public class TimeLimitInfo
            {
                /// <summary>
                /// 限制类型
                /// </summary>
                public TimeLimitType Type { get; set; }
                /// <summary>
                /// 当前type类型下的起始时间（小时），如当前结构体内填写了MONDAY，此处填写了10，则此处表示周一 10:00可用
                /// </summary>
                public int BeginHour { get; set; }
                /// <summary>
                /// 当前type类型下的起始时间（分钟），如当前结构体内填写了MONDAY，begin_hour填写10，此处填写了59，则此处表示周一 10:59可用
                /// </summary>
                public int BeginMinute { get; set; }
                /// <summary>
                /// 当前type类型下的结束时间（小时），如当前结构体内填写了MONDAY，此处填写了20，则此处表示周一 10:00-20:00可用
                /// </summary>
                public int EndHour { get; set; }
                /// <summary>
                /// 当前type类型下的结束时间（分钟），如当前结构体内填写了MONDAY，begin_hour填写10，此处填写了59，则此处表示周一 10:59-00:59可用
                /// </summary>
                public int EndMinute { get; set; }
            }
        }
        public class CreateQrcodeCardInfo
        {
            /// <summary>
            /// 卡券ID
            /// </summary>
            public string CardId { get; set; }
            /// <summary>
            /// 卡券Code码,use_custom_code字段为true的卡券必须填写，
            /// <para>非自定义code和导入code模式的卡券不必填写</para>
            /// </summary>
            public string Code { get; set; }
            /// <summary>
            /// 指定领取者的openid，只有该用户能领取。
            /// <para>bind_openid字段为true的卡券必须填写，非指定openid不必填写</para>
            /// </summary>
            public string OpenId { get; set; }
            /// <summary>
            /// outer_id字段升级版本，字符串类型，用户首次领卡时，会通过领取事件推送给商户；
            /// <para>对于会员卡的二维码，用户每次扫码打开会员卡后点击任何url，</para>
            /// <para>会将该值拼入url中，方便开发者定位扫码来源</para>
            /// </summary>
            public string OuterStr { get; set; }
            /// <summary>
            /// 指定下发二维码，生成的二维码随机分配一个code，领取后不可再次扫描。
            /// <para>填写true或false。默认false，注意填写该字段时，卡券须通过审核且库存不为0</para>
            /// </summary>
            public bool IsUniqueCode { get; set; }
        }

        public class LandingPageCardInfo
        {
            /// <summary>
            /// 卡券编号
            /// </summary>
            public string CardId { get; set; }
            /// <summary>
            /// 缩略图url
            /// </summary>
            public string ThumbUrl { get; set; }
        }
        #endregion
    }
}
