using System.Drawing;

namespace NFlex
{
    public static partial class Extensions
    {
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="source">原图片</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>
        public static Image CreateThumbnail(this Image source, int width, int height, ThumbnailMode mode)
        {
            return Images.CreateThumbnail(source, width, height, mode);
        }
    }
}
