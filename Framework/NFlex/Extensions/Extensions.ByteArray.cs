using System.Drawing;
using System.IO;
using System.Text;

namespace NFlex
{
    public static partial class Extensions
    {
        /// <summary>
        /// 将字节数组转换为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="encoding">字符编码</param>
        public static string ToString(this byte[] bytes,Encoding encoding)
        {
            return encoding.GetString(bytes);
        }


        /// <summary>
        /// 将字节数组转换为 Stream
        /// </summary>
        public static MemoryStream ToStream(this byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 将字节数组转换为文件
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="fileName">要保存文件的绝对地址</param>
        public static void ToFile(this byte[] bytes,string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        /// <summary>
        /// 将字节数组转换为 Image
        /// </summary>
        public static Image ToImage(this byte[] bytes)
        {
            MemoryStream stream = ToStream(bytes);
            Image img = Image.FromStream(stream);
            return img;
        }
    }
}
