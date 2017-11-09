using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Caching.Redis
{
    public class RedisCache : CacheBase
    {
        private readonly IDatabase _database;
        private readonly IServer _server;
        private readonly ConnectionMultiplexer _multiplexer;
        private int _databaseId;

        public RedisCache(string name,string connectionString,int databaseId=-1):base(name)
        {
            _databaseId = databaseId;
            _multiplexer = ConnectionMultiplexer.Connect(connectionString);
            _server= _multiplexer.GetServer(connectionString);
            _database = _multiplexer.GetDatabase(databaseId);
        }

        public override List<string> GetKeys(Func<string, bool> predicate = null)
        {
            var keys = _server.Keys(_databaseId).Select(t => t.ToString());
            if (predicate == null) return keys.ToList();
            else return keys.Where(predicate).ToList();
        }

        protected override void SetCache(string key, object value, TimeSpan? expireTime = default(TimeSpan?))
        {
            _database.StringSet(
                key,
                value.ToJson(),
                expireTime
                );
        }

        protected override T GetCache<T>(string key)
        {
            var obj = _database.StringGet(key);
            return obj.HasValue ? obj.ToString().JsonTo<T>() : default(T);
        }

        protected override void RemoveCache(string key) => _database.KeyDelete(key);

        public override void Dispose() => _multiplexer.Dispose();

        public override bool ContainsKey(string key) => _database.KeyExists(key);

        
    }
}
