using NFlex.Ioc;
using System;

namespace NFlex.Logging
{
    public interface ILogger:ISingletonDependency
    {
        void Fatal(string message, Exception ex = null);
        void Error(string message, Exception ex = null);
        void Warning(string message,Exception ex=null);
        void Info(string message, Exception ex = null);
        void Debug(string message, Exception ex = null);
    }
}
