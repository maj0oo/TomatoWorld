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
            var seed = InventoryManager.GetSeed(InventoryManager.ChosedTomato);
            if(seed == null)
            {
                InventoryManager.DisplayInfo(Consts.Translations.notEnaugthSeeds);
                return;
            }
            InventoryManager.RemoveSeed(seed);
            tomato = new Tomato.Tomato(InventoryManager.ChosedTomato);
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
                if (Input.GetKeyDown(KeyCode.E))
                {
                    InventoryManager.DisplayInfo(Consts.Translations.potIsBusy);
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
