using Assets.Characters.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Assets
{
    internal class DayManager
    {
        private GameObject _lightObject;
        private Light _light;
        private const int maxColorValue = 255;
        Helpers.Timer timer;
        private static List<QuestManager> questManagers = new List<QuestManager>();
        public TimeSpan time;
        public TimeOperationType operationType;
        public int colorValue;
        public DayManager(GameObject lightObj)
        {
            _lightObject = lightObj;
            _light = lightObj.GetComponent<Light>();
            timer = new Helpers.Timer(GlobalParams.dayLength);
            timer.timeElapsed += Timer_timeElapsed;
            timer.timeTick += Timer_timeTick;
            time = TimeSpan.Zero;
        }

        private void Timer_timeTick()
        {
            time = (100 - (timer.currentTime.TotalSeconds * 100 / timer.fullTime.TotalSeconds)) * TimeSpan.FromHours(24) / 100;
            if(time <= TimeSpan.FromHours(12))
            {
                operationType = TimeOperationType.increment;
            }
            else
            {
                operationType = TimeOperationType.decrement;
            }
            colorValue = CalculateColorValue();
            
            //_light.color = new Color(colorValue, colorValue, colorValue);
        }
        private int CalculateColorValue()
        {
            int counter = (int)(time * maxColorValue / TimeSpan.FromHours(12));
            if (operationType == TimeOperationType.increment)
            {
                return counter;
            }
            else
            {
                return maxColorValue - (counter - maxColorValue);
            }
        }
        private void Timer_timeElapsed()
        {
            if (GlobalParams.dayState == GlobalParams.DayState.day)
            {
                GlobalParams.dayState = GlobalParams.DayState.night;
            }
            else
            {

                GlobalParams.dayState = GlobalParams.DayState.day;
            }
            RegenerateQuests();
            //timer.Dispose();
            timer = new Helpers.Timer(GlobalParams.dayLength);
            timer.timeElapsed += Timer_timeElapsed;
            timer.timeTick += Timer_timeTick;
        }
        private void RegenerateQuests()
        {
            foreach(QuestManager mngr in questManagers)
            {
                mngr.RegenerateQuests();
            }
        }
        public static void AddQuestManager(QuestManager questMngr)
        {
            questManagers.Add(questMngr);
        }
        public enum TimeOperationType
        {
            increment = 0,
            decrement = 1
        }
    }
}
