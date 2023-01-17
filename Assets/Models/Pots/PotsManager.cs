using Assembly_CSharp;
using Assets.Models.Inventory;
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
        private static Color defaultColor;
        public static bool isSettingNewPot => newPotObject != null;
        public static void BeginPotPlace()
        {
            if (!InventoryManager.HaveEnaughtPots)
            {
                InventoryManager.DisplayInfo(Consts.Translations.notEnaughtPots);
                return;
            }
            IPot pot = pots.FirstOrDefault();
            if(pot == null)
            {
                return;
            }
            newPotObject = Instantiate(pot.GetGameObject(), new Vector3(0,0,0), Quaternion.identity);
            defaultColor = pot.GetGameObject().transform.GetChild(0).GetComponent<Renderer>().material.color;
            newPotObject.GetComponent<BoxCollider>().isTrigger = true;
            newPotObject.AddComponent<Rigidbody>().isKinematic = true;
            newPotObject.AddComponent<OnEnter>();
        }
        public static void TrySetNewPot()
        {
            var onEnter = newPotObject.GetComponent<OnEnter>();
            if (!onEnter.CheckCanPlace())
            {
                InventoryManager.DisplayInfo(Consts.Translations.potColliding);
                return;
            }
            var newPotObj = Instantiate(newPotObject, newPotObject.transform.position, Quaternion.identity);
            newPotObj.GetComponent<BoxCollider>().isTrigger = false;
            var potChilld = newPotObj.transform.GetChild(0);
            Destroy(newPotObj.GetComponent<OnEnter>());
            Renderer renderer = potChilld.GetComponent<Renderer>();
            renderer.material.color = defaultColor;
            Pot pot = new Pot(newPotObj);
            pots.Add(pot);
            Destroy(newPotObject);
            InventoryManager.ChosedOption = InventoryType.malinowy;
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
