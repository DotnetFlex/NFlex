using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.PushMessage
{
    public class TemplateSendJobFinishEventArgs:EventArgsBase
    {
        /// <summary>
        /// 消息id
        /// </summary>
        public long MsgID { get; set; }

        /// <summary>
        /// 发送状态为成功
        /// <para>success：成功</para>
        /// <para>user block：送达由于用户拒收（用户设置拒绝接收公众号消息）而失败</para>
        /// <para>system failed：送达由于其他原因失败</para>
        /// </summary>
        public string Status { get; set; }
    }
}
