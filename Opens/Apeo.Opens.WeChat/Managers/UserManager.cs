using NFlex.Opens.Weixin.Models;
using NFlex.Opens.Weixin.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Managers
{
    public class UserManager:ManagerBase
    {
        internal UserManager(string apiUrl, AccessTokenContainer tokenContainer) : base(apiUrl, tokenContainer) { }

        #region 用户信息管理
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="nextOpenId">第一个拉取的OPENID，不填默认从头开始拉取</param>
        public GetUserListResult GetUserList(string nextOpenId=null)
        {
            var client = GetClient();
            if (!string.IsNullOrEmpty(nextOpenId))
                client.AddQuery("next_openid", nextOpenId);
            return client.Get("/cgi-bin/user/get").JsonTo<GetUserListResult>();
        }

        /// <summary>
        /// 设置用户备注名
        /// </summary>
        /// <param name="openId">	用户标识</param>
        /// <param name="remarkName">新的备注名，长度必须小于30字符</param>
        public Result SetUserRemark(string openId, string remarkName)
        {
            var data = new
            {
                openid = openId,
                remark = remarkName
            };

            return PostJson("/cgi-bin/user/info/updateremark", data);
        }

        /// <summary>
        /// 获取用户基本信息（包括UnionID机制）
        /// </summary>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        public GetUserInfoResult GetUserInfo(string openId)
        {
            return GetClient()
                .AddQuery("openid", openId)
                .AddQuery("lang", "zh_CN")
                .Get("/cgi-bin/user/info")
                .JsonTo<GetUserInfoResult>();
        }

        /// <summary>
        /// 批量获取用户基本信息
        /// </summary>
        /// <param name="openIdList">用户标示列表</param>
        public BatchGetUserInfoResult BatchGetUserInfo(List<string> openIdList)
        {
            var list = openIdList.Select(t => new
            {
                openid = t,
                lang = "zh_CN"
            }).ToList();

            var data = new
            {
                user_list = list
            };

            return PostJson<BatchGetUserInfoResult>("/cgi-bin/user/info/batchget", data);
        }
        #endregion


        #region 用户标签管理
        /// <summary>
        /// 创建用户标签
        /// </summary>
        /// <param name="tagName"></param>
        public CreateTagResult CreateTag(string tagName)
        {
            var data = new
            {
                tag = new
                {
                    name = tagName
                }
            };

            return PostJson<CreateTagResult>("/cgi-bin/tags/create", data);
        }

        /// <summary>
        /// 获取用户标签列表
        /// </summary>
        public GetTagListResult GetTagList()
        {
            return GetJson<GetTagListResult>("/cgi-bin/tags/get");
        }

        /// <summary>
        /// 更新用户标签
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="tagName"></param>
        public Result UpdateTag(int tagId, string tagName)
        {
            var data = new
            {
                tag = new
                {
                    id = tagId,
                    name = tagName
                }
            };

            return PostJson<Result>("/cgi-bin/tags/update", data);
        }

        /// <summary>
        /// 删除用户标签
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="tagName"></param>
        public Result DeleteTag(int tagId)
        {
            var data = new
            {
                tag = new
                {
                    id = tagId
                }
            };

            return PostJson<Result>("/cgi-bin/tags/delete", data);
        }

        /// <summary>
        /// 获取标签下粉丝列表
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="nextOpenId"></param>
        public GetTagUsersResult GetTagUsers(int tagId, string nextOpenId = "")
        {
            var data = new
            {
                tag = new
                {
                    id = tagId,
                    next_openid = nextOpenId
                }
            };

            return PostJson<GetTagUsersResult>("/cgi-bin/user/tag/get", data);
        }

        /// <summary>
        /// 批量为用户打标签
        /// </summary>
        /// <param name="openIds"></param>
        /// <param name="tagId"></param>
        public Result SetUsersTag(List<string> openIds, int tagId)
        {
            var data = new
            {
                openid_list = openIds,
                tagid = tagId
            };

            return PostJson<Result>("/cgi-bin/tags/members/batchtagging", data);
        }

        /// <summary>
        /// 批量为用户取消标签
        /// </summary>
        /// <param name="openIds"></param>
        /// <param name="tagId"></param>
        public Result UnSetUsersTag(List<string> openIds, int tagId)
        {
            var data = new
            {
                openid_list = openIds,
                tagid = tagId
            };

            return PostJson<Result>("/cgi-bin/tags/members/batchuntagging", data);
        }

        /// <summary>
        /// 获取用户身上的标签列表
        /// </summary>
        /// <param name="openId"></param>
        public GetUserTagsResult GetUserTags(string openId)
        {
            var data = new
            {
                openid = openId
            };

            return PostJson<GetUserTagsResult>("/cgi-bin/tags/getidlist", data);
        }
        #endregion

    }
}
