using System;

namespace NFlex
{
    public static partial class Extensions
    {
        /// <summary>
        /// 转换为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败返回的默认值</param>
        /// <returns></returns>
        public static T To<T>(this object value, T defaultValue = default(T))
        {
            if (value == null)
                return defaultValue;
            if (value is string && string.IsNullOrWhiteSpace(value.ToString()))
                return defaultValue;
            Type type = Common.GetType<T>();
            try
            {
                if (type.Name.ToLower() == "guid")
                    return (T)(object)new Guid(value.ToString());
                if (value is IConvertible)
                    return (T)Convert.ChangeType(value, type);
                return (T)value;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 将对象序列化为 Json 字符串
        /// </summary>
        public static string ToJson<T>(this T obj)
        {
            return Json.ToJson(obj);
        }
    }
}
