using Assets.Models.Pots;
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

        public static InventoryType ChosedOption = InventoryType.malinowy;
        private static ConcurrentDictionary<InventoryType, Text> tomatoesTexts = new ConcurrentDictionary<InventoryType, Text>();

        //INVENTORY LISTS 
        private static List<Tomato.Tomato> tomatoes = new List<Tomato.Tomato>();
        private static List<Seed> seeds = new List<Seed>();

        //POTS
        private static int potsCollected = 2;
        private static Text potCountText;
        public static bool HaveEnaughtPots => potsCollected > 0;

        //BALANCE
        private static int balance = 10;
        private static Text balanceText;

        public InventoryManager()
        {
            ChangeTomatoHighlight(ChosedOption);
        }
        public static void AddTomato(Tomato.Tomato tomato)
        {
            tomatoes.Add(tomato);
        }
        public static void AddSeed(Seed seed)
        {
            seeds.Add(seed);
        }
        public static void AddPot()
        {
            potsCollected++;
        }
        public static void RemovePot()
        {
            if(potsCollected > 0)
            {
                potsCollected--;
            }
        }
        public static void SetBalanceText(Text text)
        {
            balanceText = text;
            balanceText.text = balance.ToString();
        }
        public static void SetPotText(Text text)
        {
            potCountText = text;
            potCountText.text = potsCollected.ToString();
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
        public static void AddBalance(int toAdd)
        {
            balance += toAdd;
            balanceText.text = balance.ToString();
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
        public static bool RemoveSeeds(int count, TomatoType type)
        {
            try
            {
                var allSeedsWithType = seeds.Where(s => s.type == type).ToList();
                for (int i = 0; i < count; i++)
                {
                    seeds.Remove(allSeedsWithType[i]);
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.Log($"Could not remove seeds: {ex.Message}");
                return false;
            }
        }
        public static bool RemoveTomatoes(int count, TomatoType type)
        {
            try
            {
                var allTomatoesWithType = tomatoes.Where(t => t.type == type).ToList();
                for(int i = 0; i < count; i++)
                {
                    tomatoes.Remove(allTomatoesWithType[i]);
                }
                return true;
            }catch(Exception ex)
            {
                Debug.Log($"Could not remove tomatoes: {ex.Message}");
                return false;
            }
            
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
                if ((int)ChosedOption >= Enum.GetNames(typeof(InventoryType)).Length - 1)
                {
                    ChosedOption = 0;
                }
                else
                {
                    ChosedOption = ChosedOption + 1;
                }
            }
            if (scroll == scrollType.up)
            {
                if ((int)ChosedOption <= 0)
                {
                    ChosedOption = (InventoryType)Enum.GetNames(typeof(InventoryType)).Length - 1;
                }
                else
                {
                    ChosedOption = ChosedOption - 1;
                }
            }
            ChangeTomatoHighlight(ChosedOption);
            if(ChosedOption == InventoryType.pot)
            {
                PotsManager.BeginPotPlace();
            }
            else
            {
                PotsManager.CancelPotPlace();
            }
        }
        public static bool SetInventoryText(InventoryType type, Text textObj)
        {
            return tomatoesTexts.TryAdd(type, textObj);
        }
        private void ChangeTomatoHighlight(InventoryType option)
        {
            tomatoesTexts[option].color = Color.green;
            SetRestOfTextsWhite(option);
        }
        private void SetRestOfTextsWhite(InventoryType option)
        {
            foreach(var x in tomatoesTexts)
            {
                if(x.Key != option)
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
    public enum InventoryType
    {
        malinowy = 0,
        koktajlowy = 1,
        daktylowy = 2,
        podluzny = 3,
        pot = 4
    }
}
