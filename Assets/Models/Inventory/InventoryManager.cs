using Assets.Models.Tomato;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Models.Inventory
{
    internal class InventoryManager
    {
        public static Text info;
        public static string textToShow;

        public static TomatoType? ChosedTomato = null;
        private static List<Tomato.Tomato> tomatoes = new List<Tomato.Tomato>();
        public static void AddTomato(Tomato.Tomato tomato)
        {
            tomatoes.Add(tomato);
        }
        public void CheckForTextUpdates()
        {
            if (!String.IsNullOrEmpty(textToShow))
            {
                TextTimeout();
            }
        }
        public static IEnumerator TextTimeout()
        {
            info.text = textToShow;
            yield return new WaitForSeconds(5);
            Debug.Log()
            info.gameObject.SetActive(false);
            textToShow = "";
        }
    }
}
