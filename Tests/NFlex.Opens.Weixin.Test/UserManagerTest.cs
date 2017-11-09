using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFlex.Opens.Weixin.Test
{
    public class UserManagerTest
    {
        WxClient client = WeixinClient.Instance;

        [Fact]
        public void GetUserList()
        {
            var result = client.User.GetUserList();
        }

        [Fact]
        public void BatchGetUserInfo()
        {
            var openIds = client.User.GetUserList().data.openid;
            var result = client.User.BatchGetUserInfo(openIds);
        }
    }
}
