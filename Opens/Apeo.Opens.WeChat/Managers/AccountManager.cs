using NFlex.Opens.Weixin.Models.Results;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Managers
{
    public class AccountManager:ManagerBase
    {
        internal AccountManager(string apiUrl,AccessTokenContainer tokenContainer) : base(apiUrl, tokenContainer) { }


        /// <summary>
        /// 生成带参数的二维码
        /// </summary>
        /// <param name="sceneId">场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000</param>
        /// <param name="expireSeconds">该二维码有效时间，以秒为单位。 最大不超过2592000（即30天）
        /// <para>不填写或填0时为永久二维码，否则为临时二维码</para>
        /// </param>
        public CreateQRCodeResult CreateQRCode(int sceneId,int expireSeconds=0)
        {
            var actionName = expireSeconds > 0 ? "QR_SCENE" : "QR_LIMIT_SCENE";
            var data = new
            {
                action_name = actionName,
                action_info = new
                {
                    scene = new { scene_id = sceneId }
                }
            };
            return PostJson<CreateQRCodeResult>("/cgi-bin/qrcode/create", data);
        }

        /// <summary>
        /// 生成带参数的二维码
        /// </summary>
        /// <param name="sceneStr">场景值ID（字符串形式的ID），字符串类型，长度限制为1到64</param>
        /// <param name="expireSeconds">该二维码有效时间，以秒为单位。 最大不超过2592000（即30天）
        /// <para>不填写或填0时为永久二维码，否则为临时二维码</para>
        /// </param>
        public CreateQRCodeResult CreateQRCode(string sceneStr, int expireSeconds = 0)
        {
            var actionName = expireSeconds > 0 ? "QR_STR_SCENE" : "QR_LIMIT_STR_SCENE";
            var data = new
            {
                action_name = actionName,
                action_info = new
                {
                    scene = new { scene_str = sceneStr }
                }
            };
            return PostJson<CreateQRCodeResult>("/cgi-bin/qrcode/create", data);
        }

        /// <summary>
        /// 通过Ticket换取二维码
        /// </summary>
        /// <param name="qrCodeTicket">二维码ticket</param>
        public Image GetQRCode(string qrCodeTicket)
        {
            return GetClient()
                .AddQuery("ticket", qrCodeTicket.UrlEncode())
                .Get("https://mp.weixin.qq.com/cgi-bin/showqrcode")
                .ToImage();
        }

        /// <summary>
        /// 长链接转短链接
        /// </summary>
        /// <param name="url">需要转换的长链接，支持http://、https://、weixin://wxpay 格式的url</param>
        public ConvertToShortUrlResult ConvertToShortUrl(string url)
        {
            var data = new
            {
                action = "long2short",
                long_url = url
            };
            return PostJson<ConvertToShortUrlResult>("/cgi-bin/shorturl", data);
        }
    }
}
