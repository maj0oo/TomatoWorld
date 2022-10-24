using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Characters
{
    class Character
    {
        public CharacterType type;
        public GameObject charObject;
        public GameObject answersPanel;
        private CharactersManager characterMngr;

        private bool busy = false;

        public Character(GameObject gameObject, CharacterType type, GameObject answersPanel, CharactersManager characterMngr)
        {
            this.characterMngr = characterMngr;
            charObject = gameObject;
            this.answersPanel = answersPanel;
            this.type = type;
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
            private List<TextItem> items = new List<TextItem>();
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
                            AddText(Consts.DealerAnswers.buySeeds, TextItem.TextItemType.parent);
                            AddText(Consts.DealerAnswers.buyTomatoes, TextItem.TextItemType.parent);
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
            private void AddText(string name, TextItem.TextItemType type)
            {
                Vector3 textPos = new Vector3(-180, -10 - items.Count * 20);
                GameObject buySeedGO = new GameObject(name);
                buySeedGO.transform.parent = character.answersPanel.transform;
                buySeedGO.AddComponent<Text>();
                var text = buySeedGO.GetComponent<Text>();
                text.text = name;
                text.alignment = TextAnchor.UpperLeft;
                TextItem item = new TextItem(text, type, this);
                items.Add(item);
                if (items.Count == 1)
                {
                    text.color = Color.green;
                    item.active = true;
                }
                var rectTransform = text.GetComponent<RectTransform>();
                rectTransform.localPosition = textPos;
                text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                buySeedGO.transform.SetParent(character.answersPanel.transform);
            }
            public void Exit()
            {
                foreach(TextItem item in items)
                {
                    item.Dispose();
                }
                character.busy = false;
                PlayerMovement.busy = false;
                items.Clear();
                character.answersPanel.SetActive(false);
            }
        }
        class TextItem : IDisposable
        {
            public bool active = false;
            public Text textObj;
            public List<TextItem> items = new List<TextItem>();
            TextItemType type;
            private readonly Panel panel;

            public TextItem(Text text, TextItemType type, Panel panel)
            {
                textObj = text;
                this.type = type;
                this.panel = panel;
            }
            public void Choose()
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

                            break;
                        }
                }
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
                foreach(TextItem i in items)
                {
                    i.Dispose();
                }
                items = null;
                Text.Destroy(textObj);
            }
            public enum TextItemType
            {
                parent = 0,
                exit = 1
            }
        }
    }
}
