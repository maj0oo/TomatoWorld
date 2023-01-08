using Assembly_CSharp;
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
        private const float distance = 5f;

        public static void BeginPotPlace()
        {
            IPot pot = pots.FirstOrDefault();
            if(pot == null)
            {
                return;
            }
            newPotObject = Instantiate(pot.GetGameObject(), new Vector3(0,0,0), Quaternion.identity);
            newPotObject.GetComponent<BoxCollider>().isTrigger = true;
            newPotObject.AddComponent<Rigidbody>().isKinematic = true;
            newPotObject.AddComponent<OnEnter>();
        }
        public static void ChangeNewPotPlace(Vector3 camPosition, Vector3 camForward)
        {
            if(newPotObject == null)
            {
                return;
            }
            newPotObject.transform.position = GetNewObjectPosition(camPosition, camForward);
            
        }
        private static Vector3 GetNewObjectPosition(Vector3 camPosition, Vector3 camForward)
        {
            var v = Vector3.Lerp(newPotObject.transform.position, camPosition + camForward * 5, Time.deltaTime * 20);
            v.y = 0f;
            if(Vector3.Distance(camPosition, v) > 4)
            {
                return v;
            }
            else
            {
                return newPotObject.transform.position;
            }
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
