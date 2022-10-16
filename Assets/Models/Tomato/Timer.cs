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
        public event elapsed timeElapsed;
        DateTime start = DateTime.Now;
        DateTime end;
        private System.Threading.Timer timer; 
        public  Timer(TimeSpan timeSpan)
        {
            end = start.Add(timeSpan);
            timer = new System.Threading.Timer(tick, null, 0, 1000);
        }
        public string GetTime()
        {
            return end.Subtract(start).ToString(@"hh\:mm\:ss");
        }
        private void tick(object state)
        {
            start = DateTime.Now;
            if(end.Subtract(start) <= TimeSpan.Zero)
            {
                timeElapsed.Invoke();
                timer.Dispose();
            }
        }
    }
}
