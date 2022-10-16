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
        private readonly static List<TextMessage> messages = new List<TextMessage>();

        public static TomatoType ChosedTomato = TomatoType.malinowy;

        //INVENTORY LISTS 
        private readonly static List<Tomato.Tomato> tomatoes = new List<Tomato.Tomato>();
        private readonly static List<Seed> seeds = new List<Seed>();

        public static void AddTomato(Tomato.Tomato tomato)
        {
            tomatoes.Add(tomato);
        }
        public static void AddSeed(Seed seed)
        {
            seeds.Add(seed);
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
}
