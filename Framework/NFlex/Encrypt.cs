using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NFlex
{
    /// <summary>
    /// 加解密辅助
    /// </summary>
    public static class Encrypt
    {
        private const string _defaultIv = "!NFlexDefaultIv@";

        #region MD5 32
        /// <summary>
        /// MD5 32位加密
        /// </summary>
        public static string Md5(string source, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var encryptBytes = Md5(encoding.GetBytes(source));
            return BitConverter.ToString(encryptBytes).Replace("-", "");
        }

        /// <summary>
        /// MD5 32位加密
        /// </summary>
        public static byte[] Md5(byte[] source)
        {
            MD5 provider = MD5.Create();
            return provider.ComputeHash(source);
        }
        #endregion

        #region MD5 16
        /// <summary>
        /// MD5 16位加密
        /// </summary>
        public static string Md5By16(string source, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            MD5 provider = MD5.Create();
            var encryptBytes = Md5By16(encoding.GetBytes(source));
            return BitConverter.ToString(encryptBytes).Replace("-", "");
        }

        /// <summary>
        /// MD5 16位加密
        /// </summary>
        public static byte[] Md5By16(byte[] source)
        {
            MD5 provider = MD5.Create();
            return provider.ComputeHash(source, 4, 8);
        }
        #endregion

        #region SHA1
        /// <summary>
        /// SHA1 加密
        /// </summary>
        public static string Sha1(string source, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var encryptBytes = Sha1(encoding.GetBytes(source));
            return BitConverter.ToString(encryptBytes).Replace("-", "");
        }

        /// <summary>
        /// SHA1 加密
        /// </summary>
        public static byte[] Sha1(byte[] source)
        {
            SHA1 provider = new SHA1CryptoServiceProvider();
            return provider.ComputeHash(source);
        }
        #endregion

        #region SHA256
        /// <summary>
        /// SHA256 加密
        /// </summary>
        public static string Sha256(string source, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var encryptBytes = Sha256(encoding.GetBytes(source));
            return BitConverter.ToString(encryptBytes).Replace("-", "");
        }

        /// <summary>
        /// SHA256 加密
        /// </summary>
        public static byte[] Sha256(byte[] source)
        {
            SHA256 provider = new SHA256CryptoServiceProvider();
            return provider.ComputeHash(source);
        }
        #endregion

        #region SHA512
        /// <summary>
        /// SHA512 加密
        /// </summary>
        public static string Sha512(string source, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var encryptBytes = Sha512(encoding.GetBytes(source));
            return BitConverter.ToString(encryptBytes).Replace("-", "");
        }

        /// <summary>
        /// SHA512 加密
        /// </summary>
        public static byte[] Sha512(byte[] source)
        {
            SHA512 provider = new SHA512CryptoServiceProvider();
            return provider.ComputeHash(source);
        }
        #endregion

        #region HMACSHA1
        /// <summary>
        /// HMACSHA1 加密
        /// </summary>
        public static string HmacSha1(string source, string key, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var encryptBytes = HmacSha1(encoding.GetBytes(source), encoding.GetBytes(key));
            return BitConverter.ToString(encryptBytes).Replace("-", "");
        }

        /// <summary>
        /// HMACSHA1 加密
        /// </summary>
        public static byte[] HmacSha1(byte[] source, byte[] key)
        {
            HMACSHA1 provider = new HMACSHA1(key);
            return provider.ComputeHash(source);
        }
        #endregion

        #region HMACSHA256
        /// <summary>
        /// HMACSHA256 加密
        /// </summary>
        public static string HmacSha256(string source, string key, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var encryptBytes = HmacSha256(encoding.GetBytes(source), encoding.GetBytes(key));
            return BitConverter.ToString(encryptBytes).Replace("-", "");
        }

        /// <summary>
        /// HMACSHA256 加密
        /// </summary>
        public static byte[] HmacSha256(byte[] source, byte[] key)
        {
            HMACSHA256 provider = new HMACSHA256(key);
            return provider.ComputeHash(source);
        }
        #endregion

        #region HMACSHA384
        /// <summary>
        /// HMACSHA384 加密
        /// </summary>
        public static string HmacSha384(string source, string key, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var encryptBytes = HmacSha384(encoding.GetBytes(source), encoding.GetBytes(key));
            return BitConverter.ToString(encryptBytes).Replace("-", "");
        }

        /// <summary>
        /// HMACSHA384 加密
        /// </summary>
        public static byte[] HmacSha384(byte[] source, byte[] key)
        {
            HMACSHA384 provider = new HMACSHA384(key);
            return provider.ComputeHash(source);
        }
        #endregion

        #region HMACSHA512
        /// <summary>
        /// HMACSHA512 加密
        /// </summary>
        public static string HmacSha512(string source, string key, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var encryptBytes = HmacSha512(encoding.GetBytes(source), encoding.GetBytes(key));
            return BitConverter.ToString(encryptBytes).Replace("-", "");
        }

        /// <summary>
        /// HMACSHA512 加密
        /// </summary>
        public static byte[] HmacSha512(byte[] source, byte[] key)
        {
            HMACSHA512 provider = new HMACSHA512(key);
            return provider.ComputeHash(source);
        }
        #endregion

        #region HMACMD5
        /// <summary>
        /// HMACMD5 加密
        /// </summary>
        public static string HmacMd5(string source, string key, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var encryptBytes = HmacMd5(encoding.GetBytes(source), encoding.GetBytes(key));
            return BitConverter.ToString(encryptBytes).Replace("-", "");
        }

        /// <summary>
        /// HMACMD5 加密
        /// </summary>
        public static byte[] HmacMd5(byte[] source, byte[] key)
        {
            HMACMD5 provider = new HMACMD5(key);
            return provider.ComputeHash(source);
        }
        #endregion

        #region HMACRIPEMD160
        /// <summary>
        /// HMACRIPEMD160 加密
        /// </summary>
        public static string HmacRipeMd160(string source, string key, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var encryptBytes = HmacRipeMd160(encoding.GetBytes(source), encoding.GetBytes(key));
            return BitConverter.ToString(encryptBytes).Replace("-", "");
        }

        /// <summary>
        /// HMACRIPEMD160 加密
        /// </summary>
        public static byte[] HmacRipeMd160(byte[] source, byte[] key)
        {
            HMACRIPEMD160 provider = new HMACRIPEMD160(key);
            return provider.ComputeHash(source);
        }
        #endregion


        #region RSA
        public static void CreateRsaKey(out string privateKey, out string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            privateKey = Convert.ToBase64String(rsa.ExportCspBlob(true));//.ToXmlString(true);
            publicKey = Convert.ToBase64String(rsa.ExportCspBlob(false));// rsa.ToXmlString(false);
        }

        /// <summary>
        /// RSA 加密
        /// </summary>
        public static string RsaEncrypt(string source, string publicKey, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var encryptBytes = RsaEncrypt(encoding.GetBytes(source), Convert.FromBase64String(publicKey));
            return Convert.ToBase64String(encryptBytes);
        }

        /// <summary>
        /// RSA 加密
        /// </summary>
        public static byte[] RsaEncrypt(byte[] source, byte[] publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(publicKey);
            return rsa.Encrypt(source, false);
        } 

        /// <summary>
        /// RSA 解密
        /// </summary>
        public static string RsaDecrypt(string source,string privateKey,Encoding encoding=null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var decryptBytes = RsaDecrypt(Convert.FromBase64String(source), Convert.FromBase64String(privateKey));
            return encoding.GetString(decryptBytes);
        }

        /// <summary>
        /// RSA 解密
        /// </summary>
        public static byte[] RsaDecrypt(byte[] source, byte[] privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(privateKey);
            return rsa.Decrypt(source, false);
        }
        #endregion


        #region AES
        /// <summary>
        /// AES 加密
        /// </summary>
        public static string AesEncrypt(string source, string key, string iv = _defaultIv, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var _key = key.Length < 32 ? encoding.GetBytes(key.PadRight(32)) : encoding.GetBytes(key.Substring(0, 32));
            var _iv = iv.Length < 16 ? encoding.GetBytes(iv.PadRight(16)) : encoding.GetBytes(iv.Substring(0, 16));
            var _inputBytes = encoding.GetBytes(source);
            byte[] encryptBytes = AesEncrypt(_inputBytes, _key, _iv);
            return Convert.ToBase64String(encryptBytes);
        }

        /// <summary>
        /// AES 加密
        /// </summary>
        public static byte[] AesEncrypt(byte[] source, byte[] key, byte[] iv)
        {
            Rijndael aes = Rijndael.Create();
            aes.Key = key;
            aes.IV = iv;
            byte[] encryptBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(source, 0, source.Length);
                    cs.FlushFinalBlock();
                    encryptBytes = ms.ToArray();
                }
            }
            aes.Clear();
            return encryptBytes;
        }

        /// <summary>
        /// AES 解密
        /// </summary>
        public static string AesDecrypt(string source, string key, string iv = _defaultIv, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var _key = key.Length < 32 ? encoding.GetBytes(key.PadRight(32)) : encoding.GetBytes(key.Substring(0, 32));
            var _iv = iv.Length < 16 ? encoding.GetBytes(iv.PadRight(16)) : encoding.GetBytes(iv.Substring(0, 16));
            var _inputBytes = Convert.FromBase64String(source);// encoding.GetBytes(source);
            byte[] decryptBytes = AesDecrypt(_inputBytes, _key, _iv);
            return encoding.GetString(decryptBytes);
        }

        /// <summary>
        /// AES 解密
        /// </summary>
        public static byte[] AesDecrypt(byte[] source, byte[] key, byte[] iv)
        {
            Rijndael aes = Rijndael.Create();
            aes.Key = key;
            aes.IV = iv;
            byte[] decryptBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(source, 0, source.Length);
                    cs.FlushFinalBlock();
                    decryptBytes = ms.ToArray();
                }
            }
            aes.Clear();
            return decryptBytes;
        }
        #endregion

        #region DES
        /// <summary>
        /// DES 加密
        /// </summary>
        public static string DesEncrypt(string source, string key, string iv = _defaultIv, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var _key = key.Length < 8 ? encoding.GetBytes(key.PadRight(8)) : encoding.GetBytes(key.Substring(0, 8));
            var _iv = iv.Length < 8 ? encoding.GetBytes(iv.PadRight(8)) : encoding.GetBytes(iv.Substring(0, 8));
            var _inputBytes = encoding.GetBytes(source);
            byte[] encryptBytes = DesEncrypt(_inputBytes, _key, _iv);
            return Convert.ToBase64String(encryptBytes);
        }

        /// <summary>
        /// DES 加密
        /// </summary>
        public static byte[] DesEncrypt(byte[] source, byte[] key, byte[] iv)
        {
            var des = new DESCryptoServiceProvider();
            des.Key = key;
            des.IV = iv;
            var encrypt = des.CreateEncryptor();
            return encrypt.TransformFinalBlock(source, 0, source.Length);
        }

        /// <summary>
        /// DES 解密
        /// </summary>
        public static string DesDecrypt(string source, string key, string iv = _defaultIv, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var _key = key.Length < 8 ? encoding.GetBytes(key.PadRight(8)) : encoding.GetBytes(key.Substring(0, 8));
            var _iv = iv.Length < 8 ? encoding.GetBytes(iv.PadRight(8)) : encoding.GetBytes(iv.Substring(0, 8));
            var _inputBytes = Convert.FromBase64String(source);
            byte[] decryptBytes = DesDecrypt(_inputBytes, _key, _iv);
            return encoding.GetString(decryptBytes);
        }

        /// <summary>
        /// DES 解密
        /// </summary>
        public static byte[] DesDecrypt(byte[] source, byte[] key, byte[] iv)
        {
            var des = new DESCryptoServiceProvider();
            des.Key = key;
            des.IV = iv;
            var decrypt = des.CreateDecryptor();
            return decrypt.TransformFinalBlock(source, 0, source.Length);
        }
        #endregion

        #region Base64
        /// <summary>
        /// Base64 加密
        /// </summary>
        public static string Base64Encrypt(string source, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return Convert.ToBase64String(encoding.GetBytes(source));
        }

        /// <summary>
        /// Base64 解密
        /// </summary>
        public static string Base64Decrypt(string source, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetString(Convert.FromBase64String(source));
        } 
        #endregion
    }
}
