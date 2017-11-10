using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace NFlex
{
    public static partial class Extensions
    {
        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        public static T JsonTo<T>(this string json)
        {
            if (string.IsNullOrEmpty(json))
                return default(T);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="camelCase">是否驼峰命名</param>
        /// <param name="indented">是否缩排</param>
        /// <param name="isConvertSingleQuotes">是否将双引号转成单引号</param>
        public static string ToJson<T>(this T target, bool camelCase = false, bool indented = false, bool isConvertSingleQuotes = false)
        {
            var options = new JsonSerializerSettings();
            options.ReferenceLoopHandling =ReferenceLoopHandling.Ignore;
            options.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            });

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }
            if (target == null)
                return "{}";
            var result = JsonConvert.SerializeObject(target, options);
            if (isConvertSingleQuotes)
                result = result.Replace("\"", "'");
            return result;
        }
    }
}
