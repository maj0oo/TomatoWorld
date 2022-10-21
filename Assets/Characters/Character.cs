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
                    busy = true;
                    characterMngr.panel = new Panel(this.answersPanel);
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
            public GameObject answersPanel;
            public Panel(GameObject answersPanel)
            {
                this.answersPanel = answersPanel;
            }
            public void OpenPanel(CharacterType type)
            {
                answersPanel.SetActive(true);
                switch (type)
                {
                    case CharacterType.dealer:
                        {
                            AddText(Consts.DealerAnswers.buySeeds);
                            AddText(Consts.DealerAnswers.buyTomatoes);

                            break;
                        }
                }
            }
            private void AddText(string name)
            {
                Vector3 textPos = new Vector3(-180, -10 - items.Count * 20);
                GameObject buySeedGO = new GameObject(name);
                buySeedGO.transform.parent = answersPanel.transform;
                buySeedGO.AddComponent<Text>();
                var text = buySeedGO.GetComponent<Text>();
                text.text = name;
                text.alignment = TextAnchor.UpperLeft;
                if(items.Count == 0)
                {
                    text.color = Color.green;
                }
                var rectTransform = text.GetComponent<RectTransform>();
                rectTransform.localPosition = textPos;
                text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                buySeedGO.transform.SetParent(answersPanel.transform);

                TextItem item = new TextItem(text);
                items.Add(item);
            }
        }
        class TextItem
        {
            public bool active = false;
            public Text textObj;
            public TextItem(Text text)
            {
                textObj = text;
            }
        }
    }
}
