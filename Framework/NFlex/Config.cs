using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NFlex
{
    /// <summary>
    /// 配置
    /// </summary>
    public static class Config
    {
        //自定义配置文件默认存放路径
        private static string _defaultConfigFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs");

        #region DefaultEncryKey(默认加密密钥)
        /// <summary>
        /// 获取默认加密密钥
        /// </summary>
        public static string DefaultEncryKey
        {
            get { return "~u!f@w#|$p%l^|&y*t(y)h_"; }
        }
        #endregion

        #region GetAppSettings(获取appSettings)

        /// <summary>
        /// 获取appSettings
        /// </summary>
        /// <param name="key">键名</param>
        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        #endregion

        #region GetConnectionString(获取连接字符串)

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="key">键名</param>        
        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ToString();
        }

        #endregion

        #region GetProviderName(获取数据提供程序名称)

        /// <summary>
        /// 获取数据提供程序名称
        /// </summary>
        /// <param name="key">键名</param>
        public static string GetProviderName(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ProviderName;
        }

        #endregion

        #region 保存对象为配置文件
        /// <summary>
        /// 保存对象为配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="fileName"></param>
        public static void SaveConfig(object config,string fileName)
        {
            string configPath = fileName;
            if(!Path.IsPathRooted(fileName))
            {
                configPath = Path.Combine(_defaultConfigFolder, fileName);
            }

            DirectoryInfo dir = new DirectoryInfo(Path.GetDirectoryName(configPath));
            if (!dir.Exists)
                dir.Create();
            XmlDocument document = new XmlDocument();
            XmlWriterSettings wSettings = new XmlWriterSettings();
            wSettings.Indent = true;
            wSettings.Encoding = Encoding.UTF8;
            wSettings.CloseOutput = true;
            wSettings.CheckCharacters = false;
            using (XmlWriter writer = XmlWriter.Create(configPath, wSettings))
            {
                XmlSerializer xs = new XmlSerializer(config.GetType());
                xs.Serialize(writer, config);
                writer.Flush();
                document.Save(writer);
            }
        }
        #endregion

        #region 读取配置文件为对象
        /// <summary>
        /// 读取配置文件为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T LoadConfig<T>(string fileName)
        {
            string configPath = fileName;
            if (!Path.IsPathRooted(fileName))
            {
                configPath = Path.Combine(_defaultConfigFolder, fileName);
            }

            T config = default(T);
            if(File.Exists(configPath))
            {
                XmlReaderSettings rSettings = new XmlReaderSettings();
                rSettings.CloseInput = true;
                rSettings.CheckCharacters = false;
                using (XmlReader reader = XmlReader.Create(configPath, rSettings))
                {
                    reader.ReadOuterXml();
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    config = (T)xs.Deserialize(reader);
                }
            }
            return config;
        }
        #endregion
    }
}
