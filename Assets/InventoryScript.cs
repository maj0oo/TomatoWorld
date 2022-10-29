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
        //SEEDS COUNTS
        public Text seedMalinowy;
        public Text seedKoktajlowy;
        public Text seedDaktylowy;
        public Text seedPodluzny;

        //TOMATOES COUNTS
        public Text tomatoMalinowy;
        public Text tomatoKoktajlowy;
        public Text tomatoDaktylowy;
        public Text tomatoPodluzny;

        //TOMATOES TEXTS
        public Text tomatoMalinowyText;
        public Text tomatoKoktajlowyText;
        public Text tomatoDaktylowyText;
        public Text tomatoPodluznyText;

        //BALANCE TEXT
        public Text balanceText;
        void Start()
        {
            InventoryManager.GenerateSeed(TomatoType.malinowy, 10);
            InventoryManager.SetBalanceText(balanceText);
            SetTomatoText(TomatoType.malinowy, tomatoMalinowyText);
            SetTomatoText(TomatoType.koktajlowy, tomatoKoktajlowyText);
            SetTomatoText(TomatoType.daktylowy, tomatoDaktylowyText);
            SetTomatoText(TomatoType.podluzny, tomatoPodluznyText);

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
        private void SetTomatoText(TomatoType type, Text text)
        {
            bool added = InventoryManager.SetTomatoText(type, text);
            if (!added)
            {
                Debug.Log($"Could not add {typeof(Text)} tomato [{type}] to inventoryManager");
            }
        }
    }
}
