using NFlex.Logging;
using NFlex.Logging.Log4Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.WechatManagement
{
    public class Log
    {
        private static Logger _logger=new NFlex.Logging.Log4Net.Logger();

        public static void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        public static void Error(string msg,Exception ex)
        {
            _logger.Error(msg,ex);
        }
    }
}