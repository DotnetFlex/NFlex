using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NFlex.Test
{
    public class CacheTest
    {
        private readonly IDatabase _database;
        private readonly IDatabase _lockDatabase;
        private readonly IServer _server;
        private readonly ConnectionMultiplexer _multiplexer;
        private List<IDatabase> _dbs=new List<IDatabase>();

        public CacheTest()
        {
            _multiplexer = ConnectionMultiplexer.Connect("192.168.1.175:9006");
            //_server = _multiplexer.GetServer(connectionString);
            _lockDatabase = _multiplexer.GetDatabase(1);
            //_database = _multiplexer.GetDatabase(1);
            for (int i = 0; i < 5; i++)
            {
                _multiplexer = ConnectionMultiplexer.Connect("192.168.1.175:9006");
                _dbs.Add(_multiplexer.GetDatabase(1));
            }
        }

        [Fact]
        public void RedisLockTest()
        {
            //MinusStock(); return;
            var sw = new Stopwatch();
            sw.Start();
            int threadCount = 10;
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < threadCount; i++)
            {
                taskList.Add(Task.Run(new Action(MinusStock)));
            }
            Task.WaitAll(taskList.ToArray());
            var es = sw.Stop();
            Debug.WriteLine(es.ToString());
        }

        private void MinusStock()
        {
            string key = "LockTest";
            string lockKey = "LockKeys:" + key;

            var threadId = Thread.CurrentThread.ManagedThreadId.ToString().PadLeft(3, '0');
            for (int i = 0; i < 20; i++)
            {
                LockKey(key);
                try
                {
                    var _db = _dbs[Common.Random(0,_dbs.Count)];
                    var stock = _db.StringGet(key).To<int>();
                    if (stock == 0)
                    {
                        Debug.WriteLine(threadId + "-- 库存不足");
                        return;
                    }
                    //Thread.Sleep(5000);
                    stock--;
                    _db.StringSet(key, stock);
                    Debug.WriteLine(threadId + "-- 当前库存：" + stock);
                }
                finally
                {
                    ReleaseKey(key);
                }
            }
        }

        public bool LockKey(string key)
        {
            var token = Guid.NewGuid().ToString();
            string lockKey = "LockKeys:" + key;
            while (true)
            {
                var locked = _lockDatabase.LockTake(lockKey, token, TimeSpan.FromSeconds(10));
                if (locked) return true;
            }
        }

        public void ReleaseKey(string key)
        {
            string lockKey = "LockKeys:" + key;
            var token= _lockDatabase.StringGet(lockKey);
            _lockDatabase.LockRelease(lockKey, token);
        }
    }
}
