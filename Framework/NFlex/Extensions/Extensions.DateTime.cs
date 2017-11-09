using System;

namespace NFlex
{
    public static partial class Extensions
    {
        public static int ToTimestamp(this DateTime date)
        {
            return (date - Common.UnixEraTime).TotalSeconds.To<int>();
        }
    }
}
