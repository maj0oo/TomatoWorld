using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class LightScript : MonoBehaviour
    {
        public GameObject lightObj;
        private DayManager dayMngr;
        public Text timeText;
        void Start()
        {
            dayMngr = new DayManager(gameObject);
        }
        void Update()
        {
            Color32 color = new Color32((byte)dayMngr.colorValue, (byte)dayMngr.colorValue, (byte)dayMngr.colorValue, 255);
            lightObj.GetComponent<Light>().color = color;
            timeText.text = dayMngr.time.ToString(@"hh\:mm\:ss");
        }
    }
}
