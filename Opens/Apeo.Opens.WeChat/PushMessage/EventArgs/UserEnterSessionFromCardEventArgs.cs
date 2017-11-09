using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.PushMessage
{
    public class UserEnterSessionFromCardEventArgs: EventArgsBase
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// Code码
        /// </summary>
        public string UserCardCode { get; set; }
    }
}
