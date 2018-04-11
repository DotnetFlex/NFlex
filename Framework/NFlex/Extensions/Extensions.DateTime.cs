using System;

namespace NFlex
{
    public static partial class Extensions
    {
        /// <summary>
        /// 将 DateTime 转换为时间戳（不含毫秒)
        /// </summary>
        /// <param name="date">要转换的 DateTime</param>
        /// <param name="withMilliseconds">时间戳是否包含毫秒</param>
        public static long ToTimestamp(this DateTime date, bool withMilliseconds = false)
        {
            if (withMilliseconds)
                return (date - Common.UnixEraTime).TotalMilliseconds.To<long>();
            else
                return (date - Common.UnixEraTime).TotalSeconds.To<int>();
        }

        /// <summary>
        /// 将时间戳转换为 DateTime
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <param name="withMilliseconds">时间戳是否包含毫秒</param>
        public static DateTime ToDateTime(this long timestamp, bool withMilliseconds = false)
        {
            if (withMilliseconds)
                return Common.UnixEraTime.AddMilliseconds(timestamp);
            else
                return Common.UnixEraTime.AddSeconds(timestamp);
        }

        /// <summary>
        /// 将时间戳转换为 DateTime
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <param name="withMilliseconds">时间戳是否包含毫秒</param>
        public static DateTime ToDateTime(this int timestamp, bool withMilliseconds = false)
        {
            if (withMilliseconds)
                return Common.UnixEraTime.AddMilliseconds(timestamp);
            else
                return Common.UnixEraTime.AddSeconds(timestamp);
        }
    }
}
