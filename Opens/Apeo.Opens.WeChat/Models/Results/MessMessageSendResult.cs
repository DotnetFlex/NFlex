using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Models.Results
{
    public class MessMessageSendResult:Result
    {
        /// <summary>
        /// 消息发送任务的ID
        /// </summary>
        public int msg_id { get; set; }
        /// <summary>
        /// 消息的数据ID，该字段只有在群发图文消息时，才会出现
        /// </summary>
        public int msg_data_id { get; set; }
    }
}
