using log4net;
using log4net.Config;
using System;
using System.IO;

namespace NFlex.Logging.Log4Net
{
    public class Logger : ILogger
    {
        private ILog log;
        public Logger()
        {
            string fileName = @"Configs\log4net.config";
            string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(configFile))
                XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
            log = LogManager.GetLogger("Logger");
        }

        public void Debug(string message, Exception ex = null)
        {
            log.Debug(message, ex);
        }

        public void Error(string message, Exception ex = null)
        {
            log.Error(message, ex);
        }

        public void Fatal(string message, Exception ex = null)
        {
            log.Fatal(message, ex);
        }

        public void Info(string message, Exception ex = null)
        {
            log.Info(message, ex);
        }

        public void Warning(string message, Exception ex = null)
        {
            log.Warn(message, ex);
        }
    }
}
