using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex
{
    public class Stopwatch
    {
        private System.Diagnostics.Stopwatch _stopwatch;
        public Stopwatch()
        {
            _stopwatch = new System.Diagnostics.Stopwatch();
        }
        
        public void Start()
        {
            _stopwatch.Start();
        }

        public TimeSpan Stop()
        {
            _stopwatch.Stop();
            return _stopwatch.Elapsed;
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }

        public TimeSpan Restart()
        {
            _stopwatch.Stop();
            var ts = _stopwatch.Elapsed;
            _stopwatch.Restart();
            return ts;
        }
    }
}
