using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace NFlex
{
    public static partial class Extensions
    {
        /// <summary>
        /// 转换字符串为指定类型数组
        /// </summary>
        /// <param name="data">原始字符串</param>
        /// <param name="separator">分割符</param>
        public static List<T> ToList<T>(this string data, string separator = ",")
        {
            var result = new List<T>();
            if (!string.IsNullOrEmpty(data))
            {
                var array = Regex.Split(data, Regex.Escape(separator), RegexOptions.IgnoreCase);
                result.AddRange(from each in array where !string.IsNullOrWhiteSpace(each) select To<T>(each));
            }
            return result;
        }

        /// <summary>
        /// Url编码
        /// </summary>
        public static string UrlEncode(this string data) => Uri.EscapeDataString(data);

        /// <summary>
        /// Url解码
        /// </summary>
        public static string UrlDecode(this string data) => Uri.UnescapeDataString(data);

        /// <summary>
        /// Html编码
        /// </summary>
        public static string HtmlEncode(this string data) => HttpUtility.HtmlEncode(data);

        /// <summary>
        /// Html解码
        /// </summary>
        public static string HtmlDecode(this string data) => HttpUtility.HtmlDecode(data);

        public static string GetUrlParam(this string data,string key)
        {
            var uri = new Uri(data);
            var query = uri.Query.TrimStart('?');
            var paramList = query.ToList<string>("&");
            foreach(var p in paramList)
            {
                var _t = p.Split('=');
                if (_t.Length < 2) continue;
                if (_t[0].ToLower() == key.ToLower()) return _t[1];
            }
            return "";
        }

        #region 加密/解密

        #region Base64
        public static string Base64Encrypt(this string data)
        {
            return data.Base64Encrypt(Encoding.UTF8);
        }
        public static string Base64Encrypt(this string data,Encoding encoding)
        {
            if (string.IsNullOrEmpty(data)) return "";
            return Convert.ToBase64String(encoding.GetBytes(data));
        }
        public static string Base64Decrypt(this string data)
        {
            return data.Base64Decrypt(Encoding.UTF8);
        }
        public static string Base64Decrypt(this string data,Encoding encoding)
        {
            if (string.IsNullOrEmpty(data)) return "";
            return encoding.GetString(Convert.FromBase64String(data));
        }
        #endregion

        #region MD5
        /// <summary>
        /// MD5 16位加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string MD5By16(this string data,Encoding encoding)
        {
            return MD5(data, encoding, 4, 8);
        }

        /// <summary>
        /// MD5 16位加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string MD5By16(this string data)
        {
            return data.MD5By16(Encoding.UTF8);
        }

        /// <summary>
        /// MD5 32位加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string MD5(this string data,Encoding encoding)
        {
            return MD5(data, encoding, null, null);
        }

        /// <summary>
        /// MD5 32位加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string MD5(this string data)
        {
            return data.MD5(Encoding.UTF8);
        }

        /// <summary>
        /// Md5加密
        /// </summary>
        private static string MD5(string text, Encoding encoding, int? startIndex, int? length)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            var md5 = new MD5CryptoServiceProvider();
            string result;
            try
            {
                if (startIndex == null)
                    result = BitConverter.ToString(md5.ComputeHash(encoding.GetBytes(text)));
                else
                    result = BitConverter.ToString(md5.ComputeHash(encoding.GetBytes(text)), startIndex.To(0), length.To(0));
            }
            finally
            {
                md5.Clear();
            }
            return result.Replace("-", "");
        }
        #endregion

        #region SHA 加密

        /// <summary>
        /// SHA1 加密
        /// </summary>
        public static string Sha1(this string data)
        {
            return data.Sha1(Encoding.UTF8);
        }
        /// <summary>
        /// SHA1 加密
        /// </summary>
        public static string Sha1(this string data, Encoding encoding)
        {
            if (string.IsNullOrEmpty(data)) return null;
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            return HashAlgorithmComputeHash(sha1, data, encoding);
        }

        /// <summary>
        /// SHA256 加密
        /// </summary>
        public static string Sha256(this string data)
        {
            return data.Sha256(Encoding.UTF8);
        }
        /// <summary>
        /// SHA256 加密
        /// </summary>
        public static string Sha256(this string data, Encoding encoding)
        {
            if (string.IsNullOrEmpty(data)) return null;
            SHA256 sha256 = new SHA256Managed();
            return HashAlgorithmComputeHash(sha256, data, encoding);
        }

        /// <summary>
        /// SHA512 加密
        /// </summary>
        public static string Sha512(this string data)
        {
            return data.Sha512(Encoding.UTF8);
        }
        /// <summary>
        /// SHA512 加密
        /// </summary>
        public static string Sha512(this string data, Encoding encoding)
        {
            if (string.IsNullOrEmpty(data)) return null;
            SHA512 sha512 = new SHA512Managed();
            return HashAlgorithmComputeHash(sha512, data, encoding);
        }

        private static string HashAlgorithmComputeHash(HashAlgorithm hashAlgorithmObj, string data, Encoding encoding)
        {
            var bytes = encoding.GetBytes(data);
            var hashStr = hashAlgorithmObj.ComputeHash(bytes);
            string hash= BitConverter.ToString(hashStr).Replace("-", "");
            return hash.ToLower();
        }

        #endregion

        #endregion

        #region 进制转换
        private static char[] BASE_CHAR36 = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static char[] BASE_CHAR32 = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'T', 'U', 'V', 'W', 'X', 'Y' };

        /// <summary>
        /// 进制转换
        /// </summary>
        /// <param name="data">要转换的字符串</param>
        /// <param name="from">源字符串进制</param>
        /// <param name="to">目标字符串进制</param>
        /// <returns></returns>
        public static string SystemConvert(this string value, int from, int to)
        {
            if (string.IsNullOrEmpty(value.Trim())) return string.Empty;
            if (from < 2 || from > 36) throw new ArgumentException("源进制必须在2~36之间");
            if (to < 2 || to > 36) throw new ArgumentException("目标进制必须在2~36之间");

            long m = x2h(value, from);
            string r = h2x(m, to);
            return r;
        }

        private static long x2h(string value, int fromBase)
        {
            value = value.Trim();
            if (string.IsNullOrEmpty(value)) return 0L;
            var rDigits = GetBaseChars(fromBase);
            string sDigits = new string(rDigits, 0, fromBase);
            long result = 0;
            value = value.ToUpper();

            for (int i = 0; i < value.Length; i++)
            {
                if (!sDigits.Contains(value[i].ToString())) throw new ArgumentException(string.Format("字符 \"{0}\" 不在 {1} 进制字符表示范围内", value[i], fromBase));
                else
                {
                    try
                    {
                        result += (long)Math.Pow(fromBase, i) * GetCharIndex(rDigits, value[value.Length - i - 1]);//   2
                    }
                    catch
                    {
                        throw new OverflowException("运算溢出.");
                    }
                }
            }

            return result;
        }
        private static string h2x(long value, int toBase)
        {
            int digitIndex = 0;
            long longPositive = Math.Abs(value);
            int radix = toBase;
            char[] outDigits = new char[63];
            var rDigits = GetBaseChars(toBase);

            for (digitIndex = 0; digitIndex <= 64; digitIndex++)
            {
                if (longPositive == 0) { break; }

                outDigits[outDigits.Length - digitIndex - 1] = rDigits[longPositive % radix];
                longPositive /= radix;
            }

            return new string(outDigits, outDigits.Length - digitIndex, digitIndex);
        }
        private static int GetCharIndex(char[] arr, char value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == value) return i;
            }
            return 0;
        }
        private static char[] GetBaseChars(int system) => system > 32 ? BASE_CHAR36 : BASE_CHAR32;

        #endregion

        #region 验证
        private const string EMAIL_PATTERN = @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$";
        private const string MOBILE_PATTERN = @"^(0|86|17951)?(13[0-9]|15[012356789]|18[0-9]|14[57]|17[678])[0-9]{8}$";
        private const string CHINA_MOBILE_PATTERN = @"(^1(3[4-9]|4[7]|5[0-27-9]|7[8]|8[2-478])\d{8}$)|(^1705\d{7}$)";
        private const string CHINA_UNICOM_PATTERN= @"(^1(3[0-2]|4[5]|5[56]|7[6]|8[56])\d{8}$)|(^1709\d{7}$)";
        private const string CHINA_TELECOM_PATTERN = @"(^1(33|53|77|8[019])\d{8}$)|(^1700\d{7}$)";
        private const string IDCARD_15_PATTERN = @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$";
        private const string IDCARD_18_PATTERN = @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[Xx])$";
        private const string GUID_PATTERN = @"[A-F0-9]{8}(-[A-F0-9]{4}){3}-[A-F0-9]{12}|[A-F0-9]{32}";
        private const string URL_PATTERN = @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$";
        private const string IP_PATTERN = @"^(\d(25[0-5]|2[0-4][0-9]|1?[0-9]?[0-9])\d\.){3}\d(25[0-5]|2[0-4][0-9]|1?[0-9]?[0-9])\d$";

        /// <summary>
        /// 当前字符串是否匹配指定的正则表达式
        /// </summary>
        /// <param name="data">要匹配的字符串</param>
        /// <param name="pattern">要匹配的正则表达式</param>
        /// <param name="options">表达式设置</param>
        public static bool IsMatch(this string data,string pattern, RegexOptions options=RegexOptions.IgnoreCase)
        {
            return Regex.IsMatch(data, pattern, options);
        }

        private static bool IsNotEmptyAndMatch(string data,string pattern,RegexOptions options=RegexOptions.IgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(data)) return false;
            return data.IsMatch(pattern, options);
        }

        /// <summary>
        /// 是否是邮箱地址
        /// </summary>
        public static bool IsEmail(this string data) => IsNotEmptyAndMatch(data, EMAIL_PATTERN);

        /// <summary>
        /// 是否是手机号
        /// </summary>
        public static bool IsMobile(this string data) => IsNotEmptyAndMatch(data, MOBILE_PATTERN);

        /// <summary>
        /// 是否是中国移动手机号
        /// </summary>
        public static bool IsChinaMobile(this string data) => IsNotEmptyAndMatch(data, CHINA_MOBILE_PATTERN);

        /// <summary>
        /// 是否是中国联通手机号
        /// </summary>
        public static bool IsChinaUnicomPhone(this string data) => IsNotEmptyAndMatch(data, CHINA_UNICOM_PATTERN);

        /// <summary>
        /// 是否是中国电信手机号
        /// </summary>
        public static bool IsChinaTelecomPhone(this string data) => IsNotEmptyAndMatch(data, CHINA_TELECOM_PATTERN);

        /// <summary>
        /// 是否是身份证号
        /// </summary>
        public static bool IsIdCard(this string data)
        {
            if (data == null) return false;
            if (data.Length == 15) return IsNotEmptyAndMatch(data, IDCARD_15_PATTERN);
            else return IsNotEmptyAndMatch(data, IDCARD_18_PATTERN);
        }

        /// <summary>
        /// 是否是日期
        /// </summary>
        public static bool IsDateTime(this string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return false;
            DateTime dt;
            if (DateTime.TryParse(data, out dt)) return true;
            else return false;
        }

        /// <summary>
        /// 是否是GUID
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsGuid(this string data) => IsNotEmptyAndMatch(data, GUID_PATTERN);

        /// <summary>
        /// 是否是URL地址
        /// </summary>
        public static bool IsUrl(this string data) => IsNotEmptyAndMatch(data, URL_PATTERN);

        /// <summary>
        /// 是否是IP地址
        /// </summary>
        public static bool IsIp(this string data) => IsNotEmptyAndMatch(data, IP_PATTERN);
        #endregion

    }
}
