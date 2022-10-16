using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Models.Tomato
{
    public class TomatoManager
    {
        public static List<Tomato> tomatoes = new List<Tomato>();

    }
    public class Tomato : IDisposable
    {
        public string name;
        public int growPercentage = 0;
        private Timer timer;
        
        public Tomato(string name)
        {
            this.name = name;
            timer = new Timer(TimeSpan.FromSeconds(10));
            timer.timeElapsed += Timer_timeElapsed;
        }
        public string GetTime()
        {
            return timer.GetTime();
        }
        private void Timer_timeElapsed()
        {
            Debug.Log("Pomidor urusl");
        }
        public void Dispose()
        {

        }
    }
}



