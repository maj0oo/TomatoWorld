using Assets.Helpers;
using Assets.Models.Tomato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Characters.Quests
{
    internal class QuestManager
    {
        public List<Quest> quests = new List<Quest>();
        public void RegenerateQuests()
        {
            quests.Clear();
            GenerateQuests(GlobalParams.questCount);
        }
        public void GenerateQuests(int questsNumber)
        {
            for(int i = 0; i < questsNumber; i++)
            {
                quests.Add(GenerateQuest());
            }
        }
        public bool RemoveQuest(Quest quest)
        {
            return quests.Remove(quest);
        }
        public Quest GenerateQuest()
        {
            Random random = new Random();
            TomatoType tomatoType = (TomatoType)random.Next(0, 3);
            int pricePerUnit = 0;
            switch (tomatoType)
            {
                case TomatoType.malinowy:
                    {
                        pricePerUnit = random.Next(5, 10);
                        break;
                    }
                case TomatoType.koktajlowy:
                    {
                        pricePerUnit = random.Next(10, 100);
                        break;
                    }
                case TomatoType.daktylowy:
                    {
                        pricePerUnit = random.Next(100,500);
                        break;
                    }
                case TomatoType.podluzny:
                    {
                        pricePerUnit = random.Next(500, 1000);
                        break;
                    }
            }
            int tomatoCount = random.Next(1, 10);
            Quest quest = new Quest();
            quest.pricePerUnit = pricePerUnit;
            quest.tomatoType = tomatoType;
            quest.type = QuestType.basic;
            quest.tomatoCount = tomatoCount;
            return quest;
        }
        public class Quest
        {
            public QuestType type { get; set; }
            public string questName 
            { 
                get 
                {
                    switch (tomatoType)
                    {
                        case TomatoType.malinowy:
                            {
                                return $"{Consts.BossAnswers.tomatoMalinowyQuest} {tomatoCount} {Consts.BossAnswers.tomatoFor} {priceTotal} $";
                            }
                        case TomatoType.koktajlowy:
                            {
                                return $"{Consts.BossAnswers.tomatoKoktajlowyQuest} {tomatoCount} {Consts.BossAnswers.tomatoFor} {priceTotal} $";
                            }
                        case TomatoType.daktylowy:
                            {
                                return $"{Consts.BossAnswers.tomatoDaktylowyQuest} {tomatoCount} {Consts.BossAnswers.tomatoFor} {priceTotal} $";
                            }
                        case TomatoType.podluzny:
                            {
                                return $"{Consts.BossAnswers.tomatoPodluznyQuest} {tomatoCount} {Consts.BossAnswers.tomatoFor} {priceTotal} $";
                            }
                        default: return "";
                    }
                }
            }
            public int tomatoCount { get; set; }
            public int pricePerUnit { get; set; }
            public TomatoType tomatoType { get; set; }
            public int priceTotal => pricePerUnit * tomatoCount;
        }
        public enum QuestType
        {
            basic = 0,
        }
    }
}
