using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Models.Pots
{
    class PotsManager: MonoBehaviour
    {
        public static List<Pot> pots = new List<Pot>();
        private static GameObject newPotObject;

        public static void BeginPotPlace()
        {
            IPot pot = pots.FirstOrDefault();
            if(pot == null)
            {
                return;
            }
            newPotObject = Instantiate(pot.GetGameObject(), new Vector3(0,0,0), Quaternion.identity).gameObject;
        }
        public static void ChangeNewPotPlace(Vector3 place)
        {
            if(newPotObject == null)
            {
                return;
            }
            Debug.Log(place);
            Destroy(newPotObject);
            newPotObject = Instantiate(newPotObject, new Vector3(place.x, 0, place.z), Quaternion.identity);
        }
        public static void CancelPotPlace()
        {
            GameObject.Destroy(newPotObject);
        }
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
