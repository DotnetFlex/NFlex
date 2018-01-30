using System.Drawing;
using System.IO;
using System.Text;

namespace NFlex
{
    public static partial class Extensions
    {
        public static string ToString(this byte[] bytes,Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        public static MemoryStream ToStream(this byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            return stream;
        }

        public static void ToFile(this byte[] bytes,string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }
        
        public static Image ToImage(this byte[] bytes)
        {
            MemoryStream stream = ToStream(bytes);
            Image img = Image.FromStream(stream);
            return img;
        }

        public static T JsonTo<T>(this byte[] bytes,Encoding encoding)
        {
            string jsonStr = ToString(bytes,encoding);
            return jsonStr.JsonTo<T>();
        }

        public static T XmlTo<T>(this byte[] bytes, Encoding encoding)
        {
            string xml = ToString(bytes, encoding);
            return xml.XmlTo<T>();
        }
    }
}
