using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Helpers
{
    public delegate void elapsed();
    public delegate void tick();
    class Timer
    {
        public bool _disposed = false;
        public event elapsed timeElapsed;
        public event tick timeTick;
        DateTime start = DateTime.Now;
        DateTime current;
        DateTime end;
        private System.Threading.Timer timer;
        public int growPercentage = 0;
        public TimeSpan fullTime => end.Subtract(start);
        public TimeSpan currentTime => end.Subtract(current);
        public Timer(TimeSpan timeSpan)
        {
            end = start.Add(timeSpan);
            timer = new System.Threading.Timer(tick, null, 0, 1000);
        }
        public string GetTime()
        {
            return end.Subtract(current).ToString(@"hh\:mm\:ss");
        }
        private void tick(object state)
        {
            current = DateTime.Now;
            if(end.Subtract(current) <= TimeSpan.Zero)
            {
                timeElapsed.Invoke();
                timer.Dispose();
            }
            growPercentage = CalculatePercentage();
            timeTick.Invoke();
        }
        private int CalculatePercentage()
        {
            return 100 - Convert.ToInt32(currentTime.TotalSeconds * 100 / fullTime.TotalSeconds);
        }
    }
}
