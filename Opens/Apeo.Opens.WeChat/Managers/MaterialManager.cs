using NFlex.Opens.Weixin.Models.Results;
using NFlex.Opens.Weixin.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Opens.Weixin.Managers
{
    public class MaterialManager:ManagerBase
    {
        internal MaterialManager(string apiUrl, AccessTokenContainer tokenContainer) : base(apiUrl, tokenContainer) { }

        /// <summary>
        /// 上传临时素材
        /// </summary>
        /// <param name="type">素材类型</param>
        /// <param name="fileName">素材文件路径</param>
        /// <returns></returns>
        public UploadMediaResult UploadTempMedia(MediaType type,string fileName)
        {
            return GetClient()
                .AddQuery("type", type.ToString().ToLower())
                .AddFile("media", fileName)
                .Post("/cgi-bin/media/upload")
                .JsonTo<UploadMediaResult>();
        }

        /// <summary>
        /// 获取临时素材
        /// </summary>
        /// <param name="mediaId">临时素材ID</param>
        /// <param name="fileName">要保存的本地文件路径</param>
        /// <returns></returns>
        public Result GetTempMedia(string mediaId, string fileName)
        {
            var converter = GetClient()
                .AddQuery("media_id", mediaId)
                .Get("/cgi-bin/media/get");

            try
            {
                return converter.JsonTo<Result>();
            }
            catch
            {
                converter.ToFile(fileName);
                return new Result();
            }
        }

        /// <summary>
        /// 上传图文消息内的图片获取URL
        /// </summary>
        /// <param name="fileName">文件绝对路径</param>
        /// <returns></returns>
        public UploadImageResult UploadImage(string fileName)
        {
            return GetClient()
                .AddFile("file", fileName)
                .Post("/cgi-bin/media/uploadimg")
                .JsonTo<UploadImageResult>();
        }

        /// <summary>
        /// 新增永久图文素材
        /// </summary>
        /// <param name="articles"></param>
        /// <returns></returns>
        public UploadMediaResult AddNewsMaterial(List<Article> articles)
        {
            var data = new
            {
                articles = articles
            };

            return PostJson<UploadMediaResult>("/cgi-bin/material/add_news", data);
        }

        /// <summary>
        /// 新增其他类型永久素材
        /// </summary>
        /// <param name="type">永久素材类型</param>
        /// <param name="fileName">素材文件路径</param>
        /// <param name="title">视频素材的标题</param>
        /// <param name="desc">视频素材的描述</param>
        /// <returns></returns>

        public AddMaterialResult AddOtherMaterial(MediaType type, string fileName,string title="",string desc="")
        {
            var client = GetClient()
                .AddForm("type", type.ToString().ToLower())
                .AddFile("media", fileName);

            if (type==MediaType.Video)
            {
                var videoParams = new
                {
                    title = title,
                    introduction = desc
                };
                client.AddForm("description", videoParams.ToJson());
            }

            return client
                .Post("/cgi-bin/material/add_material")
                .JsonTo<AddMaterialResult>();
        }

        /// <summary>
        /// 获取图文素材
        /// </summary>
        /// <param name="mediaId">素材ID</param>
        public GetNewsMaterialResult GetNewsMaterial(string mediaId)
        {
            var data = new { media_id = mediaId };

            return PostJson<GetNewsMaterialResult>("/cgi-bin/material/get_material", data);
        }

        /// <summary>
        /// 获取视频素材
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public GetVideoMaterialResult GetVideoMaterial(string mediaId)
        {
            var data = new { media_id = mediaId };

            return PostJson<GetVideoMaterialResult>("/cgi-bin/material/get_material", data);
        }

        /// <summary>
        /// 获取其它素材
        /// </summary>
        /// <param name="mediaId">素材ID</param>
        /// <param name="fileName">要保存素材的本地文件名</param>
        /// <returns></returns>
        public Result GetOtherMaterial(string mediaId,string fileName)
        {
            var data = new { media_id = mediaId };

            var converter = GetClient()
                .AddQuery("media_id", mediaId)
                .SetJson(data)
                .Post("/cgi-bin/material/get_material");

            try
            {
                return converter.JsonTo<Result>();
            }
            catch
            {
                converter.ToFile(fileName);
                return new Result();
            }
        }

        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="mediaId">素材的media_id</param>
        /// <returns></returns>
        public Result DeleteMaterial(string mediaId)
        {
            var data = new { media_id = mediaId };
            return PostJson("/cgi-bin/material/del_material", data);
        }

        /// <summary>
        /// 修改图文素材
        /// </summary>
        /// <param name="mediaId">素材的media_id</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <param name="article"></param>
        /// <returns></returns>
        public Result UpdateNewsMaterial(string mediaId,int index,Article article)
        {
            var data = new
            {
                media_id = mediaId,
                index = index,
                articles = article
            };

            return PostJson("/cgi-bin/material/update_news", data);
        }

        /// <summary>
        /// 获取永久素材总数
        /// </summary>
        /// <returns></returns>
        public GetMaterialCountResult GetMaterialCount()
        {
            return GetJson<GetMaterialCountResult>("/cgi-bin/material/get_materialcount");
        }

        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <param name="type">素材的类型</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间，默认20</param>
        public GetNewsMaterialListResult GetNewsMaterialList(MediaType type,int offset,int count=20)
        {
            var data = new
            {
                type = type.ToString().ToLower(),
                offset = offset,
                count = count
            };

            return PostJson<GetNewsMaterialListResult>("/cgi-bin/material/batchget_material", data);
        }

        /// <summary>
        /// 获取其它素材列表
        /// </summary>
        /// <param name="type">素材的类型</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间，默认20</param>
        public GetOtherMaterialListResult GetOtherMaterialList(MediaType type, int offset, int count = 20)
        {
            var data = new
            {
                type = type.ToString().ToLower(),
                offset = offset,
                count = count
            };

            return PostJson<GetOtherMaterialListResult>("/cgi-bin/material/batchget_material", data);
        }
    }
}
