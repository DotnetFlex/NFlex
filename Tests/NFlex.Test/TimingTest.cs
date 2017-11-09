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
    public class TimingTest
    {
        [Fact]
        public void IntervalAsyncTest1()
        {
            Timing timer = new Timing( () =>
              {
                  Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
                  Thread.Sleep(2000);
              });
            timer.Start(1000, Timing.ExecuteMode.Async);
            Thread.Sleep(new TimeSpan(0, 0, 5));
            timer.Stop();
            Thread.Sleep(5000);
        }


        [Fact]
        public void IntervalAsyncTest2()
        {
            Timing timer = new Timing(() =>
            {
                Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
                Thread.Sleep(2000);
            });
            timer.Start(1000, Timing.ExecuteMode.Async, true);
            Thread.Sleep(new TimeSpan(0, 0, 5));
            timer.Stop();
            Thread.Sleep(5000);
        }


        [Fact]
        public void IntervalSyncTest1()
        {
            Timing timer = new Timing( () =>
            {
                Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
                Thread.Sleep(1000);
            });
            timer.Start(100, Timing.ExecuteMode.Sync);
            Thread.Sleep(new TimeSpan(0, 0, 5));
            timer.Stop();
        }

        [Fact]
        public void IntervalSyncTest2()
        {
            Timing timer = new Timing(() =>
            {
                Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
                Thread.Sleep(1000);
            });
            timer.Start(0, Timing.ExecuteMode.Sync, true);
            Thread.Sleep(new TimeSpan(0, 0, 5));
            timer.Stop();
        }

        [Fact]
        public void TimeListTest1()
        {
            Debug.WriteLine(DateTime.Now.ToString("当前时间：yyyy-MM-dd HH:mm:ss"));
            Timing timer = new Timing(() =>
            {
                Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            });

            timer.Start(Timing.CycelType.Never, DateTime.Now.AddSeconds(5),DateTime.Now.AddSeconds(12));
            Thread.Sleep(DateTime.Now.AddSeconds(20)-DateTime.Now);
            timer.Stop();
        }

        [Fact]
        public void TimeListForMinuteTest1()
        {
            Debug.WriteLine(DateTime.Now.ToString("当前时间：yyyy-MM-dd HH:mm:ss"));
            Timing timer = new Timing(() =>
            {
                Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            });

            timer.Start(Timing.CycelType.Minute, DateTime.Parse("2001-08-09 10:20:30"));
            Thread.Sleep(DateTime.Now.AddMinutes(5) - DateTime.Now);
            timer.Stop();
        }
    }
}
