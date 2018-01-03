using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Caching.Redis
{
    public class RedisCache : Cache
    {
        private readonly IDatabase _database;
        private readonly IServer _server;
        private readonly ConnectionMultiplexer _multiplexer;
        private int _databaseId;

        public RedisCache(string connectionString,int databaseId=-1):base()
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

        public override bool Contains(string key) => _database.KeyExists(key);

        /// <summary>
        /// 给指定的Key加独占锁
        /// </summary>
        /// <param name="key">要加锁的Key</param>
        public bool Lock(string key)
        {
            if (null == _database)
                return false;
            var token = Environment.MachineName;
            string lockKey = string.Format("{0}:{1}", _cacheLockKeys, key);
            while (true)
            {
                var locked = _database.LockTake(lockKey, token, TimeSpan.FromSeconds(10));
                if (locked) return true;
            }
        }

        /// <summary>
        /// 解锁指定的Key
        /// </summary>
        /// <param name="key">要解锁的Key</param>
        public void UnLock(string key)
        {
            string lockKey = string.Format("{0}:{1}", _cacheLockKeys, key);
            var token = Environment.MachineName;
            _database.LockRelease(lockKey, token);
        }
    }
}
