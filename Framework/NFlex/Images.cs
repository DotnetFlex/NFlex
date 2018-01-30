using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace NFlex
{
    public static class Images
    {
        #region 生成缩略图
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="srcFile">原图片路径</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>
        public static Image CreateThumbnail(string srcFile,int width,int height,ThumbnailMode mode)
        {
            //加载原图片
            using (Image source = Image.FromFile(srcFile))
            {
                //生成缩略图
                return CreateThumbnail(source, width, height, mode);
            }
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="source">原图片</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>
        public static Image CreateThumbnail(Image source, int width,int height,ThumbnailMode mode)
        {
            //处理图片尺寸
            var size = new ThumbnailSize
            {
                OriginalHeight = source.Height,
                OriginalWidth = source.Width,
                TargetHeight = height,
                TargetWidth = width
            };
            ComputeThumbnailSize(size, mode);

            //新建一个bmp图片
            Image image = new Bitmap(size.TargetWidth, size.TargetHeight);
            //新建一个画板
            Graphics g = Graphics.FromImage(image);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(source,
                new Rectangle(0, 0, size.TargetWidth, size.TargetHeight),
                new Rectangle(size.RectX, size.RectY, size.RectWidth, size.RectHeight),
                GraphicsUnit.Pixel);
            g.Dispose();

            return image;
        }

        /// <summary>
        /// 计算缩放后的尺寸信息
        /// </summary>
        /// <param name="size"></param>
        /// <param name="mode"></param>
        private static void ComputeThumbnailSize(ThumbnailSize size,ThumbnailMode mode)
        {
            size.RectWidth = size.OriginalWidth;
            size.RectHeight = size.OriginalHeight;
            switch (mode)
            {
                case ThumbnailMode.WidthHeight:
                    break;
                case ThumbnailMode.Width:
                    size.TargetHeight = size.OriginalHeight * size.TargetWidth / size.OriginalWidth;
                    break;
                case ThumbnailMode.Height:
                    size.TargetWidth = size.OriginalWidth * size.TargetHeight / size.OriginalHeight;
                    break;
                case ThumbnailMode.Cut:
                    if((double)size.OriginalWidth/(double)size.OriginalHeight>(double)size.TargetWidth/(double)size.TargetHeight)
                    {
                        size.RectWidth = size.OriginalHeight * size.TargetWidth / size.TargetHeight;
                        size.RectX = (size.OriginalWidth - size.RectWidth) / 2;
                    }
                    else
                    {
                        size.RectHeight = size.OriginalWidth * size.TargetHeight / size.TargetWidth;
                        size.RectY = (size.OriginalHeight - size.RectHeight) / 2;
                    }
                    break;
                default:
                    break;
            }
        }

        private class ThumbnailSize
        {
            public int RectY { get; set; }
            public int RectX { get; set; }
            public int OriginalWidth { get; set; }
            public int OriginalHeight { get; set; }
            public int TargetWidth { get; set; }
            public int TargetHeight { get; set; }
            public int RectWidth { get; set; }
            public int RectHeight { get; set; }
        }
        #endregion

        #region 生成验证码
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="code">验证码字符</param>
        /// <param name="foreColors">验证码前景颜色代码列表</param>
        /// <param name="bgColors">验证码背景颜色代码列表</param>
        /// <param name="fontNames">验证码字体列表</param>
        /// <param name="fontSize">验证码字体大小列表</param>
        public static Image GetVerifyImage(string code, 
            IList<string> foreColors = null, 
            IList<string> bgColors=null,
            IList<string> fontNames=null,
            IList<float> fontSize=null)
        {
            string[] bgcolor = bgColors == null? new string[] { "#f7feec", "#edf7ff", "#f8f7fc", "#f8f8f8" }: bgColors.ToArray();
            string[] txcolor = bgColors==null?new string[] { "#57871a", "#cc2908", "#e5350e", "#0264a3" }: bgColors.ToArray();
            string[] fonts = fontNames==null? new string[] { "宋体", "黑体", "Georgia" }: fontNames.ToArray();
            float[] sizes = fontSize==null? new float[] { 32f, 26f, 20f }: fontSize.ToArray();

            System.Web.UI.WebControls.WebColorConverter wc = new System.Web.UI.WebControls.WebColorConverter();
            Bitmap bmp = new Bitmap(32 * code.Length + 20, 50);
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.High;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear((Color)wc.ConvertFromString(bgcolor[Common.Random(0, bgcolor.Length)]));


            float x = 5f;
            float y = 5f;
            Brush brush = new SolidBrush((Color)wc.ConvertFromString(txcolor[Common.Random(0, txcolor.Length)]));
            FontFamily ff = new FontFamily(fonts[Common.Random(0, fonts.Length)]);
            for (int i = 0; i < code.Length; i++)
            {
                float size = sizes[Common.Random(0, sizes.Length)];
                Font font = new Font(ff, size, FontStyle.Bold);
                int rangle = Common.Random(-15, 15);

                Bitmap unitBmp = new Bitmap(50, 50, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                Graphics unitG = Graphics.FromImage(unitBmp);
                unitG.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                unitG.DrawString(code[i].ToString(), font, brush, new Point(0, 0));
                font.Dispose();
                font = null;
                unitBmp = KiRotate(unitBmp, Common.Random(-45, 45), Color.Transparent);

                g.DrawImage(unitBmp, new PointF(x, (50 - unitBmp.Height) / 2));
                x += 30;
            }
            g.Dispose();

            return bmp;
        }

        private static Bitmap KiRotate(Bitmap bmp, float angle, Color bkColor)
        {
            int w = bmp.Width + 2;
            int h = bmp.Height + 2;

            PixelFormat pf;

            if (bkColor == Color.Transparent)
            {
                pf = PixelFormat.Format32bppArgb;
            }
            else
            {
                pf = bmp.PixelFormat;
            }

            Bitmap tmp = new Bitmap(w, h, pf);
            Graphics g = Graphics.FromImage(tmp);
            g.Clear(bkColor);
            g.DrawImageUnscaled(bmp, 1, 1);
            g.Dispose();

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(0f, 0f, w, h));
            Matrix mtrx = new Matrix();
            mtrx.Rotate(angle);
            RectangleF rct = path.GetBounds(mtrx);

            Bitmap dst = new Bitmap((int)rct.Width, (int)rct.Height, pf);
            g = Graphics.FromImage(dst);
            g.Clear(bkColor);
            g.TranslateTransform(-rct.X, -rct.Y);
            g.RotateTransform(angle);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(tmp, 0, 0);
            g.Dispose();

            tmp.Dispose();

            return dst;
        }
        #endregion
    }

    /// <summary>
    /// 缩略图生成方式
    /// </summary>
    public enum ThumbnailMode
    {
        /// <summary>
        /// 指定宽高缩放（图片可能会变形)
        /// </summary>
        WidthHeight,

        /// <summary>
        /// 指定宽，高按比例
        /// </summary>
        Width,

        /// <summary>
        /// 指定高，宽按比例
        /// </summary>
        Height,

        /// <summary>
        /// 指定宽高裁剪（中心为原点，不变形）
        /// </summary>
        Cut
    }        
}
