using System;
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
        public bool tomatoIsPlanted => tomato != null;
        public Pot(GameObject obj)
        {
            potObject = obj;
        }
        public void PlantTomato()
        {
            tomato = new Tomato.Tomato("Pomidor malinowy");
            Tomato.TomatoManager.tomatoes.Add(tomato);
        }
        public int GetObjectId()
        {
            return potObject.GetInstanceID();
        }
    }
}
