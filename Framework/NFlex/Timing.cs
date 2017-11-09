using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NFlex
{
    /// <summary>
    /// 定时器
    /// </summary>
    public class Timing
    {
        private ExecuteMode _executeMode;
        private Timer _timer;
        private bool _immediately = false;
        private Action _action;

        /// <summary>
        /// 构造一个定时器
        /// </summary>
        /// <param name="action">定时器要完成的任务委托</param>
        public Timing(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// 开始定时器
        /// </summary>
        /// <param name="interval">间隔时间</param>
        /// <param name="mode">运行模式<see cref="ExecuteMode"/></param>
        /// <param name="immediately">是否要立即执行一次任务内容</param>
        public void Start(int interval, ExecuteMode mode, bool immediately = false)
        {
            _immediately = immediately;
            _executeMode = mode;
            StartInterval(interval);
        }

        /// <summary>
        /// 开始定时器
        /// </summary>
        /// <param name="timeSpan">时间间隔</param>
        /// <param name="mode">运行模式<see cref="ExecuteMode"/></param>
        /// <param name="immediately">是否要立即执行一次任务内容</param>
        public void Start(TimeSpan timeSpan, ExecuteMode mode, bool immediately = false)
        {
            Start(Convert.ToInt32(timeSpan.TotalMilliseconds), mode, immediately);
        }

        /// <summary>
        /// 开始定时器
        /// </summary>
        /// <param name="cycelType">循环模式<see cref="CycelType"/></param>
        /// <param name="times">时间列表</param>
        public void Start(CycelType cycelType, params DateTime[] times)
        {
            StartTimeList(cycelType, times.ToList());
        }

        private void StartInterval(int interval)
        {
            _timer = new Timer((obj) => {
                if (_executeMode == ExecuteMode.Sync)
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);

                _action();

                if (_executeMode == ExecuteMode.Sync)
                    try { _timer.Dispose(); } catch { }
            }, null, _immediately ? 0 : interval, interval);
        }

        private void StartTimeList(CycelType cycelType, List<DateTime> times)
        {
            if (times == null || times.Count == 0) return;

            InitTimeList(cycelType, times);
            //将日期根据循环模式做处理
            var ts = GetNextTime(cycelType, times);
            if (ts == TimeSpan.MinValue) return;

            _timer = new Timer((obj) =>
            {
                var timeSpan = GetNextTime(cycelType, times);
                if (timeSpan != TimeSpan.MinValue)
                {
                    _timer.Change(timeSpan, timeSpan);
                }
                _action();

                if (timeSpan == TimeSpan.MinValue)
                    try { _timer.Dispose(); } catch { }
            }, null, ts, ts);
        }

        private TimeSpan GetNextTime(CycelType cycelType, List<DateTime> times)
        {
            var next = times.OrderBy(t => t)
                .Where(t => t > DateTime.Now)
                .Select(t => new { Index = times.IndexOf(t), Time = t })
                .FirstOrDefault();

            DateTime nextTime = next.Time;
            if (nextTime == DateTime.MinValue) return TimeSpan.MinValue;
            var ts = (nextTime - DateTime.Now);
            if (ts.TotalMilliseconds <= 0)
            {
                return TimeSpan.MinValue;
            }
            else
            {
                times[next.Index] = HandleTime(cycelType, nextTime);
                return ts;
            }
        }

        private DateTime HandleTime(CycelType cycel, DateTime dt)
        {
            switch (cycel)
            {
                case CycelType.Never:
                    return dt;
                case CycelType.Minute:
                    return dt.AddMinutes(1);
                case CycelType.Hour:
                    return dt.AddHours(1);
                case CycelType.Day:
                    return dt.AddDays(1);
                case CycelType.Week:
                    return dt.AddDays(7);
                case CycelType.Month:
                    return dt.AddMonths(1);
                case CycelType.Year:
                    return dt.AddYears(1);
                default:
                    return dt;
            }
        }

        private void InitTimeList(CycelType cycel, List<DateTime> times)
        {
            for (int i = 0; i < times.Count; i++)
            {
                switch (cycel)
                {
                    case CycelType.Never:
                        break;
                    case CycelType.Minute:
                        times[i] = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:") + times[i].ToString("ss.fff"));
                        break;
                    case CycelType.Hour:
                        times[i] = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:") + times[i].ToString("mm:ss.fff"));
                        break;
                    case CycelType.Day:
                        times[i] = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + times[i].ToString("HH:mm:ss.fff"));
                        break;
                    case CycelType.Week:
                        times[i] = DateTime.Parse(DateTime.Now.ToString("yyyy-MM- ") + times[i].ToString("dd HH:mm:ss.fff"));
                        break;
                    case CycelType.Month:
                        times[i] = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-") + times[i].ToString("dd HH:mm:ss.fff"));
                        break;
                    case CycelType.Year:
                        times[i] = DateTime.Parse(DateTime.Now.ToString("yyyy-") + times[i].ToString("MM-dd HH:mm:ss.fff"));
                        break;
                    default:
                        break;
                }
            }

            while (cycel != CycelType.Never && times.Any(t => t < DateTime.Now))
            {
                var leftList = times.Where(t => t < DateTime.Now).ToList();
                for (int i = 0; i < leftList.Count; i++)
                {
                    times[i] = HandleTime(cycel, times[i]);
                }
            }
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        public void Stop()
        {
            try { _timer.Dispose(); } catch { }
        }

        /// <summary>
        /// 创建一个定时器对象并立即开始
        /// </summary>
        /// <param name="action">定时器要完成的任务委托</param>
        /// <param name="interval">间隔时间</param>
        /// <param name="mode">运行模式<see cref="ExecuteMode"/></param>
        /// <param name="immediately">是否要立即执行一次任务内容</param>
        public static Timing StartNewTiming(Action action, int interval, ExecuteMode mode, bool immediately = false)
        {
            var timing = new Timing(action);
            timing.Start(interval, mode, immediately);
            return timing;
        }

        /// <summary>
        /// 创建一个定时器对象并立即开始
        /// </summary>
        /// <param name="action">定时器要完成的任务委托</param>
        /// <param name="interval">间隔时间</param>
        /// <param name="mode">运行模式<see cref="ExecuteMode"/></param>
        /// <param name="immediately">是否要立即执行一次任务内容</param>
        public static Timing StartNewTiming(Action action, TimeSpan timeSpan, ExecuteMode mode, bool immediately = false)
        {
            var timing = new Timing(action);
            timing.Start(timeSpan, mode, immediately);
            return timing;
        }

        /// <summary>
        /// 创建一个定时器对象并立即开始
        /// </summary>
        /// <param name="action">定时器要完成的任务委托</param>
        /// <param name="cycelType">循环模式<see cref="CycelType"/></param>
        /// <param name="times">时间列表</param>
        public static Timing StartNewTiming(Action action, CycelType cycelType, params DateTime[] times)
        {
            var timing = new Timing(action);
            timing.Start(cycelType, times);
            return timing;
        }

        #region 内部类

        /// <summary>
        /// 循环模式
        /// </summary>
        public enum CycelType
        {
            /// <summary>
            /// 永不
            /// </summary>
            Never,

            /// <summary>
            /// 每分钟
            /// </summary>
            Minute,

            /// <summary>
            /// 每小时
            /// </summary>
            Hour,

            /// <summary>
            /// 每天
            /// </summary>
            Day,

            /// <summary>
            /// 每周
            /// </summary>
            Week,

            /// <summary>
            /// 每月
            /// </summary>
            Month,

            /// <summary>
            /// 每年
            /// </summary>
            Year
        }

        /// <summary>
        /// 运行模式
        /// </summary>
        public enum ExecuteMode
        {
            /// <summary>
            /// 同步
            /// </summary>
            Sync,

            /// <summary>
            /// 异步
            /// </summary>
            Async
        }
        #endregion
    }
}
