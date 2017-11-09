using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;

namespace NFlex.QRCode
{
    public static partial class Extensions
    {
        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="source">源字符</param>
        /// <param name="level">纠错级别</param>
        public static Image CreateQRCode(this string source, ErrorCorrectionLevel level)
        {
            return source.CreateQRCode(new QRCodeSetting { ErrorCorrectionLevel = level });
        }

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="source">源字符</param>
        /// <param name="level">纠错级别</param>
        /// <param name="pieceSize">每个元素像素大小</param>
        public static Image CreateQRCode(this string source, ErrorCorrectionLevel level,int pieceSize)
        {
            return source.CreateQRCode(new QRCodeSetting { ErrorCorrectionLevel = level,PieceSize=pieceSize });
        }

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="source">源字符</param>
        /// <param name="foreColor">二维码颜色</param>
        /// <param name="backColor">背景颜色</param>
        public static Image CreateQRCode(this string source, Color foreColor,Color backColor)
        {
            return source.CreateQRCode(new QRCodeSetting { ForeColor = foreColor, BackColor = backColor });
        }

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="source">源字符</param>
        /// <param name="setting">二维码生成设置</param>
        public static Image CreateQRCode(this string source,QRCodeSetting setting=null)
        {
            if (setting == null) setting = new QRCodeSetting();
            var level = setting.ErrorCorrectionLevel == null ? Gma.QrCodeNet.Encoding.ErrorCorrectionLevel.H : (Gma.QrCodeNet.Encoding.ErrorCorrectionLevel)setting.ErrorCorrectionLevel;
            var pieceSize = setting.PieceSize ?? 1;
            var foreColor = setting.ForeColor ?? Color.Black;
            var backColor = setting.BackColor ?? Color.White;
            var zone = setting.QuietZoneModule == null ? Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules.Two : (Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules)setting.QuietZoneModule;

            QrEncoder encoder = new QrEncoder(level);
            QrCode code = encoder.Encode(source);
            GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(pieceSize, zone),new SolidBrush(foreColor),new SolidBrush(backColor));
            DrawingSize dSize = render.SizeCalculator.GetSize(code.Matrix.Width);

            var image = new Bitmap(dSize.CodeWidth, dSize.CodeWidth);
            using (Graphics g = Graphics.FromImage(image))
                render.Draw(g, code.Matrix);
            return image;
        }
    }
}
