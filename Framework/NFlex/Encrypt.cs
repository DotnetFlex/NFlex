using System;
using System.Security.Cryptography;
using System.Text;

namespace NFlex
{
    /// <summary>
    /// 加解密辅助
    /// </summary>
    public static class Encrypt
    {

        #region Sha1
        public static string Sha1(string data, Encoding encoding)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] dataToHash = encoding.GetBytes(data);
            byte[] dataHashed = sha1.ComputeHash(dataToHash);
            string hash = BitConverter.ToString(dataHashed).Replace("-", "");
            return hash.ToLower();
        }
        public static string Sha1(string data)
        {
            return Sha1(data, Encoding.UTF8);
        }
        #endregion

        #region HmacSha1
        public static string HmacSha1(string data,string key,Encoding encoding)
        {
            using (var algorithm = KeyedHashAlgorithm.Create("HMACSHA1"))
            {
                algorithm.Key = encoding.GetBytes(key.ToCharArray());
                var hashed = algorithm.ComputeHash(encoding.GetBytes(data.ToCharArray()));
                return Convert.ToBase64String(hashed);
            }
        }
        #endregion

        #region Md5加密

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        public static string Md5By16(string text)
        {
            return Md5By16(text, Encoding.UTF8);
        }

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">字符编码</param>
        public static string Md5By16(string text, Encoding encoding)
        {
            return Md5(text, encoding, 4, 8);
        }

        /// <summary>
        /// Md5加密
        /// </summary>
        private static string Md5(string text, Encoding encoding, int? startIndex, int? length)
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

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        public static string Md5(string text)
        {
            return Md5(text, Encoding.UTF8);
        }

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">字符编码</param>
        public static string Md5(string text, Encoding encoding)
        {
            return Md5(text, encoding, null, null);
        }

        #endregion

        #region Des加密

        /// <summary>
        /// Des加密
        /// </summary>
        /// <param name="value">原始值</param>
        public static string EncodeDes(object value)
        {
            return value == null ? string.Empty : EncodeDes(value.ToString(), Config.DefaultEncryKey);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="value">原始值</param>
        /// <param name="key">密钥,必须24位</param>
        public static string EncodeDes(object value, string key)
        {
            string text = value.To("");
            if (!ValidateDes(text, key))
                return string.Empty;
            var provider = CreateProvider(key);
            using (var transform = provider.CreateEncryptor())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                var result = transform.TransformFinalBlock(bytes, 0, bytes.Length);
                return Convert.ToBase64String(result);
            }
        }

        /// <summary>
        /// 验证参数
        /// </summary>
        private static bool ValidateDes(string value, string key)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
                return false;
            return key.Length == 24;
        }

        /// <summary>
        /// 创建加密服务提供程序
        /// </summary>
        private static TripleDESCryptoServiceProvider CreateProvider(string key)
        {
            return new TripleDESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes(key), Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="value">内容</param>
        public static string DecodeDes(object value)
        {
            return value == null ? string.Empty : DecodeDes(value.ToString(), Config.DefaultEncryKey);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="value">内容</param>
        /// <param name="key">密钥,必须24位</param>
        public static string DecodeDes(object value, string key)
        {
            string text = value.To("");
            if (!ValidateDes(text, key))
                return string.Empty;
            var provider = CreateProvider(key);
            using (var transform = provider.CreateDecryptor())
            {
                var bytes = Convert.FromBase64String(text);
                var result = transform.TransformFinalBlock(bytes, 0, bytes.Length);
                return Encoding.UTF8.GetString(result);
            }
        }

        #endregion
    }
}
