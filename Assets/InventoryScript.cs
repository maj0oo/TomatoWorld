using Assets.Models.Inventory;
using Assets.Models.Tomato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class InventoryScript : MonoBehaviour
    {
        //SEEDS
        public Text seedMalinowy;
        public Text seedKoktajlowy;
        public Text seedDaktylowy;
        public Text seedPodluzny;

        //TOMATOES
        public Text tomatoMalinowy;
        public Text tomatoKoktajlowy;
        public Text tomatoDaktylowy;
        public Text tomatoPodluzny;
        void Start()
        {
            InventoryManager.GenerateSeed(TomatoType.malinowy, 10);
        }

        void Update()
        {
            seedMalinowy.text = InventoryManager.GetSeedCount(TomatoType.malinowy).ToString();
            seedKoktajlowy.text = InventoryManager.GetSeedCount(TomatoType.koktajlowy).ToString();
            seedDaktylowy.text = InventoryManager.GetSeedCount(TomatoType.daktylowy).ToString();
            seedPodluzny.text = InventoryManager.GetSeedCount(TomatoType.podluzny).ToString();

            tomatoMalinowy.text = InventoryManager.GetTomatoCount(TomatoType.malinowy).ToString();
            tomatoKoktajlowy.text = InventoryManager.GetTomatoCount(TomatoType.koktajlowy).ToString();
            tomatoDaktylowy.text = InventoryManager.GetTomatoCount(TomatoType.daktylowy).ToString();
            tomatoPodluzny.text = InventoryManager.GetTomatoCount(TomatoType.podluzny).ToString();
        }
    }
}
