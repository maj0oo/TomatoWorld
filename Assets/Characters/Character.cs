
using Assets.Characters.Quests;
using Assets.Models.Inventory;
using Assets.Models.Tomato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static Assets.Characters.Character;

namespace Assets.Characters
{
    class Character
    {
        public CharacterType type;
        public GameObject charObject;
        public GameObject answersPanel;
        public CharactersManager characterMngr;
        public QuestManager questMngr;

        private bool busy = false;

        public Character(GameObject gameObject, CharacterType type, GameObject answersPanel, CharactersManager characterMngr)
        {
            this.characterMngr = characterMngr;
            charObject = gameObject;
            this.answersPanel = answersPanel;
            this.type = type;
            if(type == CharacterType.boss)
            {
                questMngr = new QuestManager();
                questMngr.GenerateQuests(GlobalParams.questCount);
                DayManager.AddQuestManager(questMngr);
            }
        }
        public string GetInfo()
        {
            if (!busy)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PlayerMovement.busy = true;
                    busy = true;
                    characterMngr.panel = new Panel(this);
                    characterMngr.panel.OpenPanel(type);
                }
                return Consts.Translations.talk;
            }
            return "";
        }
        public int GetObjectId()
        {
            return charObject.GetInstanceID();
        }
        
        public class Panel
        {
            public List<TextItem> items = new List<TextItem>();
            public readonly Character character;
            public Panel(Character character)
            {
                this.character = character;
            }
            public void OpenPanel(CharacterType type)
            {
                character.answersPanel.SetActive(true);
                switch (type)
                {
                    case CharacterType.dealer:
                        {
                            AddText(Consts.DealerAnswers.buySeeds, TextItem.TextItemType.parent,TextItem.ChildItemsType.buySeeds);
                            AddText(Consts.DealerAnswers.buyTomatoes, TextItem.TextItemType.parent, TextItem.ChildItemsType.buyTomatos);
                            AddText(Consts.Translations.end, TextItem.TextItemType.exit);
                            break;
                        }
                    case CharacterType.boss:
                        {
                            AddText(Consts.BossAnswers.quests, TextItem.TextItemType.parent, TextItem.ChildItemsType.quests);
                            AddText(Consts.BossAnswers.sellTomatoes, TextItem.TextItemType.parent, TextItem.ChildItemsType.sellTomatoes);
                            AddText(Consts.BossAnswers.sellSeeds, TextItem.TextItemType.parent, TextItem.ChildItemsType.sellSeeds);
                            AddText(Consts.Translations.end, TextItem.TextItemType.exit);
                            break;
                        }
                }
            }
          
            public void ActiveNext()
            {
                int activeItemIndex = items.IndexOf(items.Where(i => i.active == true).FirstOrDefault());
                if(activeItemIndex == items.Count - 1)
                {
                    items[0].Active();
                    UnactiveExcept(items[0]);
                    return;
                }
                items[activeItemIndex + 1].Active();
                UnactiveExcept(items[activeItemIndex + 1]);
            }
            public void ActivePrev()
            {
                int activeItemIndex = items.IndexOf(items.Where(i => i.active == true).FirstOrDefault());
                if (activeItemIndex == 0)
                {
                    items[items.Count - 1].Active();
                    UnactiveExcept(items[items.Count -1]);
                    return;
                }
                items[activeItemIndex - 1].Active();
                UnactiveExcept(items[activeItemIndex -1]);
            }
            private void UnactiveExcept(TextItem item)
            {
                var itemToDeactive = items.Where(i => i.active == true && i != item).FirstOrDefault();
                if(itemToDeactive != null)
                {
                    itemToDeactive.Deactive();
                }
            }
            public void ChooseActive()
            {
                var activeItem = items.Where(i => i.active).FirstOrDefault();
                if(activeItem != null)
                {
                    activeItem.Choose();
                }
            }
            private void AddText(string name, TextItem.TextItemType type, TextItem.ChildItemsType? childsType = null)
            {
                var text = CreateTextObj(name, new Vector3(-180, -10 - items.Count * 20));
                
                TextItem item = new TextItem(type, this);
                item.textObj = text;
                if(childsType != null)
                {
                    item.items = GetChilds(childsType);
                }
                items.Add(item);
                if (items.Count == 1)
                {
                    text.color = Color.green;
                    item.active = true;
                }
                
            }
            public void EnableChilds(List<TextItem> childItems)
            {
                foreach (TextItem item in items)
                {
                    item.Dispose();
                }
                foreach (Transform child in character.answersPanel.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                items.Clear();
                foreach(TextItem childItem in childItems)
                {
                    var text = CreateTextObj(childItem.text, new Vector3(-180, -10 - items.Count * 20));
                    childItem.textObj = text;
                    items.Add(childItem);
                    if(items.Count == 1)
                    {
                        text.color = Color.green;
                        childItem.active = true;
                    }
                }
            }
            private Text CreateTextObj(string name, Vector3 position)
            {
                Vector3 textPos = new Vector3(-30, 10 - items.Count * 20);
                GameObject GO = new GameObject(name);
                GO.transform.parent = character.answersPanel.transform;
                GO.AddComponent<Text>();
                var text = GO.GetComponent<Text>();
                GO.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);
                text.text = name;
                text.alignment = TextAnchor.UpperLeft;
                var rectTransform = text.GetComponent<RectTransform>();
                rectTransform.localPosition = textPos;
                text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                GO.transform.SetParent(character.answersPanel.transform);
                return text;
            }
            private List<TextItem> GetChilds(TextItem.ChildItemsType? type)
            {
                List<TextItem> childItems = new List<TextItem>();
                switch (type)
                {
                    case TextItem.ChildItemsType.buySeeds:
                        {
                            childItems.Add(CreateTextItemObject(Consts.DealerAnswers.buyMalinowySeeds.Replace("#price", "1"), TextItem.TextItemType.buySeed, TomatoType.malinowy, 1));
                            childItems.Add(CreateTextItemObject(Consts.DealerAnswers.buyKoktajlowySeeds.Replace("#price", "10"), TextItem.TextItemType.buySeed, TomatoType.koktajlowy, 10));
                            childItems.Add(CreateTextItemObject(Consts.DealerAnswers.buyDaktylowySeeds.Replace("#price", "50"), TextItem.TextItemType.buySeed, TomatoType.daktylowy, 50));
                            childItems.Add(CreateTextItemObject(Consts.DealerAnswers.buyPodluznySeeds.Replace("#price", "100"), TextItem.TextItemType.buySeed, TomatoType.podluzny, 100));
                            childItems.Add(CreateTextItemObject(Consts.Translations.end, TextItem.TextItemType.exit));
                            break;
                        }
                    case TextItem.ChildItemsType.buyTomatos:
                        {
                            childItems.Add(CreateTextItemObject(Consts.DealerAnswers.buyMalinowyTomato.Replace("#price", "10"), TextItem.TextItemType.buyTomato, TomatoType.malinowy, 10));
                            childItems.Add(CreateTextItemObject(Consts.DealerAnswers.buyKoktajlowyTomato.Replace("#price", "100"), TextItem.TextItemType.buyTomato, TomatoType.koktajlowy, 100));
                            childItems.Add(CreateTextItemObject(Consts.DealerAnswers.buyDaktylowyTomato.Replace("#price", "500"), TextItem.TextItemType.buyTomato, TomatoType.daktylowy, 500));
                            childItems.Add(CreateTextItemObject(Consts.DealerAnswers.buyPodluznyTomato.Replace("#price", "1000"), TextItem.TextItemType.buyTomato, TomatoType.podluzny, 1000));
                            childItems.Add(CreateTextItemObject(Consts.Translations.end, TextItem.TextItemType.exit));
                            break;
                        }
                    case TextItem.ChildItemsType.quests:
                        {
                            foreach(QuestManager.Quest q in character.questMngr.quests)
                            {
                                childItems.Add(CreateTextItemObject(q.questName, TextItem.TextItemType.quest, q.tomatoType, q.priceTotal, q));
                            }
                            childItems.Add(CreateTextItemObject(Consts.Translations.end, TextItem.TextItemType.exit));
                            break;
                        }
                    case TextItem.ChildItemsType.sellSeeds:
                        {
                            childItems.Add(CreateTextItemObject(Consts.BossAnswers.sellMalinowySeed.Replace("#price", "1"), TextItem.TextItemType.sellSeed, TomatoType.malinowy, 1));
                            childItems.Add(CreateTextItemObject(Consts.BossAnswers.sellKoktajlowySeed.Replace("#price", "10"), TextItem.TextItemType.sellSeed, TomatoType.koktajlowy, 10));
                            childItems.Add(CreateTextItemObject(Consts.BossAnswers.sellDaktylowySeed.Replace("#price", "50"), TextItem.TextItemType.sellSeed, TomatoType.daktylowy, 50));
                            childItems.Add(CreateTextItemObject(Consts.BossAnswers.sellPodluznySeed.Replace("#price", "100"), TextItem.TextItemType.sellSeed, TomatoType.podluzny, 100));
                            childItems.Add(CreateTextItemObject(Consts.Translations.end, TextItem.TextItemType.exit));
                            break;
                        }
                    case TextItem.ChildItemsType.sellTomatoes:
                        {
                            childItems.Add(CreateTextItemObject(Consts.BossAnswers.sellMalinowyTomato.Replace("#price", "5"), TextItem.TextItemType.sellTomato, TomatoType.malinowy, 5));
                            childItems.Add(CreateTextItemObject(Consts.BossAnswers.sellKoktajlowyTomato.Replace("#price", "15"), TextItem.TextItemType.sellTomato, TomatoType.koktajlowy, 15));
                            childItems.Add(CreateTextItemObject(Consts.BossAnswers.sellDaktylowyTomato.Replace("#price", "100"), TextItem.TextItemType.sellTomato, TomatoType.daktylowy, 100));
                            childItems.Add(CreateTextItemObject(Consts.BossAnswers.sellPodluznyTomato.Replace("#price", "500"), TextItem.TextItemType.sellTomato, TomatoType.podluzny, 500));
                            childItems.Add(CreateTextItemObject(Consts.Translations.end, TextItem.TextItemType.exit));
                            break;
                        }
                }
                return childItems;
            }
            private TextItem CreateTextItemObject(string text, TextItem.TextItemType type, TomatoType? tomatoType = null, int price = 0, QuestManager.Quest quest = null)
            {
                TextItem item = new TextItem(type, this, tomatoType, price);
                item.text = text;
                item.quest = quest;
                return item;
            }
            public void Exit()
            {
                foreach (TextItem item in items)
                {
                    item.Dispose();
                }
                foreach (Transform child in character.answersPanel.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                character.busy = false;
                PlayerMovement.busy = false;
                items.Clear();
                character.answersPanel.SetActive(false);
            }
        }
        public class TextItem : IDisposable, ICloneable
        {
            public bool active = false;
            public Text textObj;
            public string text = String.Empty;
            public List<TextItem> items = new List<TextItem>();
            TextItemType type;
            private readonly Panel panel;


            public int price = 0;
            public TomatoType? tomatoType;

            public QuestManager.Quest quest = null;

            public TextItem(TextItemType type, Panel panel, TomatoType? tomatoType = null, int price = 0)
            {
                this.type = type;
                this.panel = panel;
                this.price = price;
                this.tomatoType = tomatoType;
            }
            public List<TextItem> Choose()
            {
                switch (type)
                {
                    case TextItemType.exit:
                        {
                            panel.Exit();
                            break;
                        }
                    case TextItemType.parent:
                        {
                            panel.EnableChilds(items);
                            break;
                        }
                    case TextItemType.buySeed:
                        {
                            bool bougth = InventoryManager.SubBalance(price);
                            if (bougth && tomatoType != null)
                            {
                                InventoryManager.AddSeed(new Seed((TomatoType)tomatoType));
                            }
                            else
                            {
                                InventoryManager.DisplayInfo(Consts.Translations.noEnaugthBalance);
                            }
                            break;
                        }
                    case TextItemType.buyTomato:
                        {
                            bool bougth = InventoryManager.SubBalance(price);
                            if (bougth && tomatoType != null)
                            {
                                InventoryManager.AddTomato(new Tomato((TomatoType)tomatoType, false));
                            }
                            else
                            {
                                InventoryManager.DisplayInfo(Consts.Translations.noEnaugthBalance);
                            }
                            break;
                        }
                    case TextItemType.quest:
                        {
                            bool check = InventoryManager.GetTomatoCount(quest.tomatoType) >= quest.tomatoCount;
                            if (!check)
                            {
                                InventoryManager.DisplayInfo(Consts.Translations.noEnaugthTomatoes);
                                break;
                            }
                            bool questRemoved = panel.character.questMngr.RemoveQuest(quest);
                            if (!questRemoved)
                            {
                                InventoryManager.DisplayInfo(Consts.Translations.questIsNotAble);
                                break;
                            }
                            InventoryManager.RemoveTomatoes(quest.tomatoCount, quest.tomatoType);
                            InventoryManager.AddBalance(quest.priceTotal);
                            
                            List<TextItem> newItems = new List<TextItem>();
                            foreach(TextItem item in panel.items)
                            {
                                if(item != this)
                                {
                                    newItems.Add((TextItem)item.Clone());
                                }
                            }
                            panel.EnableChilds(newItems);
                            break;
                        }
                    case TextItemType.sellSeed:
                        {
                            bool check = InventoryManager.GetSeedCount((TomatoType)tomatoType) >= 1;
                            if (!check)
                            {
                                InventoryManager.DisplayInfo(Consts.Translations.noEnaugthSeeds);
                                break;
                            }
                            bool removedFromInventory = InventoryManager.RemoveSeeds(1, (TomatoType)tomatoType);
                            if (removedFromInventory)
                            {
                                InventoryManager.AddBalance(price);
                            }
                            break;
                        }
                    case TextItemType.sellTomato:
                        {
                            bool check = InventoryManager.GetTomatoCount((TomatoType)tomatoType) >= 1;
                            if (!check)
                            {
                                InventoryManager.DisplayInfo(Consts.Translations.noEnaugthTomatoes);
                                break;
                            }
                            bool removedFromInventory = InventoryManager.RemoveTomatoes(1, (TomatoType)tomatoType);
                            if (removedFromInventory)
                            {
                                InventoryManager.AddBalance(price);
                            }
                            break;
                        }
                }
                return items;
            }
            public void Active()
            {
                active = true;
                textObj.color = Color.green;
            }
            public void Deactive()
            {
                active = false;
                textObj.color = Color.white;
            }
            public void Dispose()
            {
                
                if (items != null)
                {
                    foreach (TextItem i in items)
                    {
                        i.Dispose();
                    }
                }
                items = null;
                GameObject.Destroy(textObj);
                
            }
            public object Clone()
            {
                TextItem item = new TextItem(type, panel, tomatoType, price);
                item.text = text;
                item.quest = quest;
                return item;
            }
            public enum TextItemType
            {
                parent = 0,
                exit = 1,
                buySeed = 2,
                buyTomato = 3,
                quest = 4,
                sellTomato = 5,
                sellSeed = 6
            }
            public enum ChildItemsType
            {
                buySeeds = 0,
                buyTomatos = 1,
                quests = 2,
                sellTomatoes = 3,
                sellSeeds = 4
            }
        }
    }
}
