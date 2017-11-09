using NFlex;
using NFlex.Caching;
using NFlex.Caching.Memory;
using NFlex.Caching.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Caching
{
    public class MemoryCacheProvider : ICacheProvider
    {
        public ICache Instance()
        {
            return new MemoryCache("Demo");
        }
    }

    public class RedisCacheProvider : ICacheProvider
    {
        public ICache Instance()
        {
            var config = Config.LoadConfig<RedisConfig>("Redis.config");
            return new RedisCache("Demo", config.Host);
        }
    }
}
