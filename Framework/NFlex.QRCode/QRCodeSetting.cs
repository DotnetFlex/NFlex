using System.Drawing;

namespace NFlex.QRCode
{
    /// <summary>
    /// 二维码生成参数
    /// </summary>
    public class QRCodeSetting
    {
        /// <summary>
        /// 纠错级别
        /// </summary>
        public ErrorCorrectionLevel? ErrorCorrectionLevel { get; set; }

        /// <summary>
        /// 二维码颜色
        /// </summary>
        public Color? ForeColor { get; set; }

        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color? BackColor { get; set; }

        /// <summary>
        /// 元素尺寸
        /// </summary>
        public int? PieceSize { get; set; }

        /// <summary>
        /// 边距
        /// </summary>
        public QuietZoneModules? QuietZoneModule { get; set; }
    }
}
