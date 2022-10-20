using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models.Tomato
{
    public delegate void elapsed();
    class Timer
    {
        public bool _disposed = false;
        public event elapsed timeElapsed;
        DateTime start = DateTime.Now;
        DateTime current;
        DateTime end;
        private System.Threading.Timer timer;
        public int growPercentage = 0;
        public  Timer(TimeSpan timeSpan)
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
        }
        private int CalculatePercentage()
        {
            TimeSpan fullTime = end.Subtract(start);
            TimeSpan currentTime = end.Subtract(current);
            return 100 - Convert.ToInt32(currentTime.TotalSeconds * 100 / fullTime.TotalSeconds);
        }
    }
}
