using Assets.Models.Tomato;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Models.Inventory
{
    internal class InventoryManager
    {
        public static Text info;
        private readonly static List<TextMessage> messages = new List<TextMessage>();

        public static TomatoType ChosedTomato = TomatoType.malinowy;
        private static ConcurrentDictionary<TomatoType, Text> tomatoesTexts = new ConcurrentDictionary<TomatoType, Text>();

        //INVENTORY LISTS 
        private readonly static List<Tomato.Tomato> tomatoes = new List<Tomato.Tomato>();
        private readonly static List<Seed> seeds = new List<Seed>();

        //BALANCE
        private static int balance = 10;
        private static Text balanceText;

        public InventoryManager()
        {
            ChangeTomatoHighlight(ChosedTomato);
        }
        public static void AddTomato(Tomato.Tomato tomato)
        {
            tomatoes.Add(tomato);
        }
        public static void AddSeed(Seed seed)
        {
            seeds.Add(seed);
        }
        public static void SetBalanceText(Text text)
        {
            balanceText = text;
            balanceText.text = balance.ToString();
        }
        public static bool SubBalance(int price)
        {
            if(balance - price >= 0)
            {
                balance = balance - price;
                balanceText.text = balance.ToString();
                return true;
            }
            return false;
        }
        public static void GenerateSeed(TomatoType type, int count)
        {
            for(int i = 0; i < count; i++)
            {
                Seed seed = new Seed(type);
                seeds.Add(seed);
            }
        }
        public static Seed GetSeed(TomatoType type)
        {
            return seeds.Where(s => s.type == type).FirstOrDefault();
        }
        public static bool RemoveSeed(Seed seed)
        {
            return seeds.Remove(seed);
        }
        public static int GetSeedCount(TomatoType type)
        {
            return seeds.Where(s => s.type == type).Count();
        }
        public static int GetTomatoCount(TomatoType type)
        {
            return tomatoes.Where(s => s.type == type).Count();
        }
        public static void DisplayInfo(string text)
        {
            TextMessage msg = new TextMessage(text);
            messages.Add(msg);
            if (!info.gameObject.activeInHierarchy)
            {
                info.gameObject.SetActive(true);
            }
        }
        public void UpdateTextInfo()
        {
            string messageText = "";

            foreach(TextMessage tm in messages)
            {
                tm.timer += Time.deltaTime;
                if(tm.timer > tm.timerOffset)
                {
                    tm.toRemove = true;
                    continue;
                }
                messageText += tm.text + "\n";
            }
            messages.RemoveAll(msg => msg.toRemove);
            if (String.IsNullOrEmpty(messageText))
            {
                info.gameObject.SetActive(false);
            }
            else
            {
                info.text = messageText;
            }
        }
        public void SwitchTomatoType(scrollType scroll)
        {
            if(scroll == scrollType.down)
            {
                if ((int)ChosedTomato >= Enum.GetNames(typeof(TomatoType)).Length - 1)
                {
                    ChosedTomato = 0;
                }
                else
                {
                    ChosedTomato = ChosedTomato + 1;
                }
            }
            if (scroll == scrollType.up)
            {
                if ((int)ChosedTomato <= 0)
                {
                    ChosedTomato = (TomatoType)Enum.GetNames(typeof(TomatoType)).Length - 1;
                }
                else
                {
                    ChosedTomato = ChosedTomato - 1;
                }
            }
            ChangeTomatoHighlight(ChosedTomato);
        }
        public static bool SetTomatoText(TomatoType type, Text textObj)
        {
            return tomatoesTexts.TryAdd(type, textObj);
        }
        private void ChangeTomatoHighlight(TomatoType tomato)
        {
            switch (tomato)
            {
                case TomatoType.malinowy:
                    {
                        tomatoesTexts[TomatoType.malinowy].color = Color.green;
                        break;
                    }
                case TomatoType.koktajlowy:
                    {
                        tomatoesTexts[TomatoType.koktajlowy].color = Color.green;
                        break;
                    }
                case TomatoType.daktylowy:
                    {
                        tomatoesTexts[TomatoType.daktylowy].color = Color.green;
                        break;
                    }
                case TomatoType.podluzny:
                    {
                        tomatoesTexts[TomatoType.podluzny].color = Color.green;
                        break;
                    }
            }
            SetRestOfTextsWhite(tomato);
        }
        private void SetRestOfTextsWhite(TomatoType tomato)
        {
            foreach(var x in tomatoesTexts)
            {
                if(x.Key != tomato)
                {
                    x.Value.color = Color.white;
                }
            }
        }
        public static float GetBalance()
        {
            return balance;
        }
    }
    public class TextMessage
    {
        public float timer = 0f;
        public float timerOffset = 2f;
        public string text { get; set; }
        public bool toRemove = false;

        public TextMessage(string text)
        {
            this.text = text;
        }
    }
    public enum scrollType
    {
        up = 0,
        down = 1
    }
}
