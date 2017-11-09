using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace NFlex
{
    public static partial class Extensions
    {
        /// <summary>
        /// 将对象序列化为XML字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToXml(this object obj)
        {
            return ToXml(obj, Encoding.UTF8);
        }

        /// <summary>
        /// 将对象序列化为XML字符串
        /// </summary>
        public static string ToXml(this object obj, Encoding encoding)
        {
            XmlSerializer xml = new XmlSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            xml.Serialize(ms, obj);
            return encoding.GetString(ms.ToArray());
        }

        /// <summary>
        /// 从XML代码反序列化为对象
        /// </summary>
        public static T XmlTo<T>(this string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                return default(T);

            using (StringReader reader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                var obj = serializer.Deserialize(reader);
                if (obj is T) return (T)obj;
                return default(T);
            }
        }
    }
}
