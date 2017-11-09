using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Sms
{
    public enum SmsResultCode
    {
        /// <summary>
        /// 发送成功
        /// </summary>
        Ok,
        /// <summary>
        /// 账户验证失败
        /// </summary>
        验证失败,
        余额不足,
        /// <summary>
        /// 短信内容为空
        /// </summary>
        ContentIsEmpty,
    }
}
