using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class GetSendStatusResult:Result
    {
        /// <summary>
        /// 群发消息后返回的消息id
        /// </summary>
        public int msg_id { get; set; }
        /// <summary>
        /// 消息发送后的状态，SEND_SUCCESS表示发送成功
        /// </summary>
        public string msg_status { get; set; }
    }
}
