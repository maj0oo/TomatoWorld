using Assets.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models.Pots
{
    class Pot
    {
        public Tomato.Tomato tomato;
        private GameObject potObject;
        public bool tomatoIsPlanted => tomato != null;
        public Pot(GameObject obj)
        {
            potObject = obj;
        }
        public void PlantTomato()
        {
            if(InventoryManager.ChosedTomato == null)
            {
                InventoryManager.info.text = Consts.Translations.choseTomatoToSeed;
                InventoryManager.info.gameObject.SetActive(true);
                InventoryManager.TextTimeout();
                return;
            }
            tomato = new Tomato.Tomato(InventoryManager.ChosedTomato);
            Tomato.TomatoManager.tomatoes.Add(tomato);
        }
        public void CollectTomato()
        {
            InventoryManager.AddTomato(tomato);
            tomato.Dispose();
            tomato = null;
        }
        public string GetInfo()
        {
            string info = "";
            if (tomatoIsPlanted)
            {
                if (tomato.isReady)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        CollectTomato();
                    }
                    return Consts.Translations.getTomato;
                }
                return $"{tomato.name} \n {tomato.GetTime()} \n {tomato.GetPercentage()}";
            }
            info = Consts.Translations.plantTomato;
            if (Input.GetKeyDown(KeyCode.E))
            {

                PlantTomato();
            }
            return info;
        }
        public int GetObjectId()
        {
            return potObject.GetInstanceID();
        }
    }
}
