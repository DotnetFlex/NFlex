using Demo.Infrastructure.Caching;
using NFlex.Caching;
using NFlex.Core.Ioc;
using NFlex.Ioc;
using NFlex.Logging;
using NFlex.Logging.Log4Net;
using NFlex.Web.Mvc.Ioc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Demo.Web
{
    public static class IocConfig
    {
        public static void Initialize()
        {
            //核心对象注入
            IocContainer.Initialize(true);
            //缓存注入
            //IocManager.Register<ICacheManager<MemoryCacheProvider>, CacheManager<MemoryCacheProvider>>();
            //IocManager.Register<ICacheManager<RedisCacheProvider>, CacheManager<RedisCacheProvider>>();
        }
    }
}