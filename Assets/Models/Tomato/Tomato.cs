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
        public int growPercentage => timer.growPercentage;
        public bool isReady = false;
        private Timer timer;
        public int growTime => GetGrowTime(type);
        public bool collected = false;
        public TomatoType? type;
        public Tomato(TomatoType? type)
        {
            this.type = type;
            name = GetTomatoName(type);
            timer = new Timer(TimeSpan.FromSeconds(growTime));
            timer.timeElapsed += Timer_timeElapsed;
        }
        public string GetTime()
        {
            return timer.GetTime();
        }
        public string GetPercentage()
        {
            return $"{growPercentage} %";
        }
        private void Timer_timeElapsed()
        {
            isReady = true;
            Debug.Log($"Pomidor urusl");
        }
        private string GetTomatoName(TomatoType? type)
        {
            switch (type)
            {
                case TomatoType.malinowy:
                    {
                        return Consts.Translations.malinowy;
                    }
                case TomatoType.podluzny:
                    {
                        return Consts.Translations.podluzny;
                    }
                case TomatoType.daktylowy:
                    {
                        return Consts.Translations.daktylowy;
                    }
                case TomatoType.koktajlowy:
                    {
                        return Consts.Translations.koktajlowy;
                    }
                default:
                    {
                        return "";
                    }
            }
        }
        private int GetGrowTime(TomatoType? type)
        {
            switch (type)
            {
                case TomatoType.malinowy:
                    {
                        return 10;
                    }
                case TomatoType.podluzny:
                    {
                        return 60;
                    }
                case TomatoType.daktylowy:
                    {
                        return 120;
                    }
                case TomatoType.koktajlowy:
                    {
                        return 30;
                    }
                default:
                    {
                        return int.MaxValue;
                    }
            }
        }
        public void Dispose()
        {
            
        }
    }
    public enum TomatoType
    {
        malinowy = 0,
        koktajlowy = 1,
        daktylowy = 2,
        podluzny = 3
    }
}



