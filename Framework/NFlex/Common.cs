using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NFlex
{
    public static class Common
    {
        private static readonly Random _random = new Random();
        private static readonly DateTime _unixEraTime = DateTime.Parse("1970-1-1").ToLocalTime();


        #region Random(生成随机数)
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <returns></returns>
        public static int Random()
        {
            return _random.Next();
        }
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="maxNum">随机数最大值（不含此值）</param>
        /// <returns></returns>
        public static int Random(int maxNum)
        {
            return _random.Next(maxNum);
        }
        /// <summary>
        /// 获取指定范围的随机整数，该范围包括最小值，但不包括最大值
        /// </summary>
        /// <param name="minNum">最小值</param>
        /// <param name="maxNum">最大值</param>
        public static int Random(int minNum, int maxNum)
        {
            return _random.Next(minNum, maxNum);
        }

        /// <summary>
        /// 获取双精度的浮点随机数
        /// </summary>
        public static double RandomDouble()
        {
            return _random.NextDouble();
        }
        #endregion

        #region GetType(获取类型)
        /// <summary>
        /// 获取类型,对可空类型进行处理
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static Type GetType<T>()
        {
            return Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        }
        #endregion

        #region 时间戳
        /// <summary>
        /// 获取Unix纪元时间(1970-1-1 00:00:00)
        /// </summary>
        public static DateTime UnixEraTime
        {
            get { return _unixEraTime; }
        }

        /// <summary>
        /// 获取当前系统时间戳
        /// </summary>
        public static long TimeStamp
        {
            get
            {
                TimeSpan ts = DateTime.Now - _unixEraTime;
                return Convert.ToInt64(ts.TotalSeconds); 
            }
        }
        #endregion

        #region 字符串编辑距离
        /// <summary>
        /// 计算字符编辑距离
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="isCaseSensitive"></param>
        /// <returns></returns>
        public static int LevenshteinDistance(string source, string target, bool isCaseSensitive = false)
        {
            if (string.IsNullOrEmpty(source))
            {
                if (String.IsNullOrEmpty(target))
                {
                    return 0;
                }
                else
                {
                    return target.Length;
                }
            }
            else if (String.IsNullOrEmpty(target))
            {
                return source.Length;
            }

            String From, To;
            if (isCaseSensitive)
            {   // 大小写敏感  
                From = source;
                To = target;
            }
            else
            {   // 大小写无关  
                From = source.ToLower();
                To = target.ToLower();
            }

            // 初始化  
            Int32 m = From.Length;
            Int32 n = To.Length;
            Int32[,] H = new Int32[m + 1, n + 1];
            for (Int32 i = 0; i <= m; i++) H[i, 0] = i;  // 注意：初始化[0,0]  
            for (Int32 j = 1; j <= n; j++) H[0, j] = j;

            // 迭代  
            for (Int32 i = 1; i <= m; i++)
            {
                Char SI = From[i - 1];
                for (Int32 j = 1; j <= n; j++)
                {   // 删除（deletion） 插入（insertion） 替换（substitution）  
                    if (SI == To[j - 1])
                        H[i, j] = H[i - 1, j - 1];
                    else
                        H[i, j] = Math.Min(H[i - 1, j - 1], Math.Min(H[i - 1, j], H[i, j - 1])) + 1;
                }
            }

            return H[m, n];    // 编辑距离  
        }
        #endregion

        public static string CreateStringId12()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            long long_guid = BitConverter.ToInt64(buffer, 0);

            string _Value = System.Math.Abs(long_guid).ToString();

            byte[] buf = new byte[_Value.Length];
            int p = 0;
            for (int i = 0; i < _Value.Length;)
            {
                byte ph = System.Convert.ToByte(_Value[i]);

                int fix = 1;
                if ((i + 1) < _Value.Length)
                {
                    byte pl = System.Convert.ToByte(_Value[i + 1]);
                    buf[p] = (byte)((ph << 4) + pl);
                    fix = 2;
                }
                else
                {
                    buf[p] = (byte)(ph);
                }

                if ((i + 3) < _Value.Length)
                {
                    if (System.Convert.ToInt16(_Value.Substring(i, 3)) < 256)
                    {
                        buf[p] = System.Convert.ToByte(_Value.Substring(i, 3));
                        fix = 3;
                    }
                }
                p++;
                i = i + fix;
            }
            byte[] buf2 = new byte[p];
            for (int i = 0; i < p; i++)
            {
                buf2[i] = buf[i];
            }
            string cRtn = System.Convert.ToBase64String(buf2);
            if (cRtn == null)
            {
                cRtn = "";
            }
            cRtn = cRtn.ToLower();
            cRtn = cRtn.Replace("/", "");
            cRtn = cRtn.Replace("+", "");
            cRtn = cRtn.Replace("=", "");
            if (cRtn.Length == 12)
            {
                return cRtn;
            }
            else
            {
                return CreateStringId12();
            }
        }
        public static string CreateStringId16()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        public static string CreateNumberId19()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0).ToString();
        }
    }
}
