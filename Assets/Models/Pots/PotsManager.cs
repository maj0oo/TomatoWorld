using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models.Pots
{
    class PotsManager
    {
        public static List<Pot> pots = new List<Pot>();

        public enum State
        {
            pot = 0,
            potWithLeaf = 1,
            potWithPlant = 2,
            potWithTomatos = 3
        }
        public class potObjectStates
        {
            public const string pot = "Pot";
            public const string potWithLeaf = "PotWithLeaf";
            public const string potWithPlant = "PotWithPlant";
            public const string potWithTomatoes = "PotWithTomatoes";
        }
    }
}
