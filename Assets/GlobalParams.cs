using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class GlobalParams
    {
        public static int questCount = 3;
        public static TimeSpan dayLength = TimeSpan.FromSeconds(20);
        public static DayState dayState;
        public static int PotPrice = 200;
        public enum DayState
        {
            night = 0,
            day = 1
        }
    }
}
