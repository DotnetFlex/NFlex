using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFlex.Opens.Weixin.Managers;
using Xunit;

namespace NFlex.Opens.Weixin.Test
{
    public class CardManagerTest
    {
        WxClient client = WeixinClient.Instance;

        [Fact]
        public void SetWhiteList()
        {
            var userList = client.User.GetUserList();
            var result=client.Card.SetCardWhiteOpenIds(userList.data.openid.ToArray());
        }

        [Fact]
        public void CreateCard()
        {
            var result = client.Card.CreateCashCard(20000, 1000, new CardManager.RequiredInfo
            {
                LogoUrl = "http://mmbiz.qpic.cn/mmbiz_jpg/KUuYeLldRiat5iaNRE6KKWoJAiaYNd6vUKVGm2ibJpMPw7Lmc9FAG4C4dSPueibkNroYLtyrPiaXjWibSr3ia7qQQICPrA/0",
                BrandName = "趣票网",
                CodeType = CardCodeType.CODE_TYPE_QRCODE,
                Color = CardBgColor.Color040,
                DateInfo = new CardManager.RequiredInfo.DateInfoEntity
                {
                    Type = CardDateType.DATE_TYPE_FIX_TIME_RANGE,
                    BeginTime = DateTime.Parse("2017-09-18"),
                    EndTime = DateTime.Parse("2017-12-16 23:59:59")
                },
                Title = "趣票网10元代金券",
                Notice = "仅限于英雄传说项目使用",
                Description = "1.本券使用方式：在趣票网微信购票支付页面选择代金券，立减对应金额。每笔交易限用一张，不可叠加使用。\r\n2.本券领取后将存储您的微信卡包",
                Quantity = 100
            }, new CardManager.BaseInfo
            {
                CanGiveFriend = true
            });
            //pMgrrwHHIznmB53Z5gJqelLiYmBM
        }

        [Fact]
        public void GetCardQrCode()
        {
            var result = client.Card.CreateCardQrcode("pMgrrwHHIznmB53Z5gJqelLiYmBM");
            //https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=gQGt8DwAAAAAAAAAAS5odHRwOi8vd2VpeGluLnFxLmNvbS9xLzAyVEN5djBtT2FmSDAxeWxfSE5yNEEAAgQVDMpZAwSAM_EB
        }
    }
}
