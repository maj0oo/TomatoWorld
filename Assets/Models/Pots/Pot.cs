using Assets.Models.Inventory;
using System;
using System.Collections.Concurrent;
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
        public ConcurrentDictionary<PotsManager.State, GameObject> stateObjects = new ConcurrentDictionary<PotsManager.State, GameObject>();
        private PotsManager.State currentState;
        public bool tomatoIsPlanted => tomato != null;
        public Pot(GameObject obj)
        {
            potObject = obj;

            AddObjectStateToDictionary(PotsManager.State.pot, PotsManager.potObjectStates.pot);
            AddObjectStateToDictionary(PotsManager.State.potWithLeaf, PotsManager.potObjectStates.potWithLeaf);
            AddObjectStateToDictionary(PotsManager.State.potWithPlant, PotsManager.potObjectStates.potWithPlant);
            AddObjectStateToDictionary(PotsManager.State.potWithTomatos, PotsManager.potObjectStates.potWithTomatoes);

            currentState = PotsManager.State.potWithTomatos;
        }
        private void AddObjectStateToDictionary(PotsManager.State state, string objName)
        {
            bool added = stateObjects.TryAdd(state, potObject.transform.Find(objName).gameObject);
            if (!added)
                Debug.Log($"Could not add {objName} object");
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
                return $"Pomidor {tomato.name} \n {tomato.GetTime()} \n {tomato.GetPercentage()}";
            }
            info = Consts.Translations.plantTomato;
            if (Input.GetKeyDown(KeyCode.E))
            {

                PlantTomato();
            }
            return info;
        }
        private PotsManager.State GetCurrentState()
        {
            if (!tomatoIsPlanted)
            {
                return PotsManager.State.pot;
            }
            if (tomatoIsPlanted && tomato.growPercentage < 50)
            {
                return PotsManager.State.potWithLeaf;
            }
            if (tomatoIsPlanted && tomato.growPercentage < 100)
            {
                return PotsManager.State.potWithPlant;
            }
            if (tomatoIsPlanted && tomato.growPercentage == 100)
            {
                return PotsManager.State.potWithTomatos;
            }
            return PotsManager.State.pot;
        }
        public void UpdatePotState()
        {
            var state = GetCurrentState();
            if(currentState == state)
            {
                return;
            }
            switch (state)
            {
                case PotsManager.State.pot:
                    {
                        stateObjects[PotsManager.State.pot].SetActive(true);
                        stateObjects[PotsManager.State.potWithLeaf].SetActive(false);
                        stateObjects[PotsManager.State.potWithPlant].SetActive(false);
                        stateObjects[PotsManager.State.potWithTomatos].SetActive(false);
                        break;
                    }
                case PotsManager.State.potWithLeaf:
                    {
                        stateObjects[PotsManager.State.pot].SetActive(false);
                        stateObjects[PotsManager.State.potWithLeaf].SetActive(true);
                        stateObjects[PotsManager.State.potWithPlant].SetActive(false);
                        stateObjects[PotsManager.State.potWithTomatos].SetActive(false);
                        break;
                    }
                case PotsManager.State.potWithPlant:
                    {
                        stateObjects[PotsManager.State.pot].SetActive(false);
                        stateObjects[PotsManager.State.potWithLeaf].SetActive(false);
                        stateObjects[PotsManager.State.potWithPlant].SetActive(true);
                        stateObjects[PotsManager.State.potWithTomatos].SetActive(false);
                        break;
                    }
                case PotsManager.State.potWithTomatos:
                    {
                        stateObjects[PotsManager.State.pot].SetActive(false);
                        stateObjects[PotsManager.State.potWithLeaf].SetActive(false);
                        stateObjects[PotsManager.State.potWithPlant].SetActive(false);
                        stateObjects[PotsManager.State.potWithTomatos].SetActive(true);
                        break;
                    }
            }

            currentState = state;
        }
        public int GetObjectId()
        {
            return potObject.GetInstanceID();
        }
    }
}
