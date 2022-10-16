using Assets.Models.Pots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Models
{
    class LookManager
    {
        private Text text;
        private GameObject activeObject;
        private Camera cam;
        public LookManager(Text text, Camera cam)
        {
            this.text = text;
            this.cam = cam;
        }
        public void CheckLook()
        {
            var pot = PlayerLookingAtPot();
            if (pot != null)
            {
                activeObject = pot;
                Pot obj = PotsManager.pots.Where(p => p.GetObjectId() == activeObject.GetInstanceID()).FirstOrDefault();
                if (obj.tomatoIsPlanted)
                {
                    text.text = obj.tomato.name + "\n " + obj.tomato.GetTime();
                    text.gameObject.SetActive(true);
                    return;
                }
                text.text = Consts.Translations.plantTomato;
                text.gameObject.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    
                    obj.PlantTomato();
                    Debug.Log(activeObject.GetInstanceID());
                }
                return;
            }
            text.gameObject.SetActive(false);
        }
        private GameObject PlayerLookingAtPot()
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 5f);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == Consts.Tags.Pot)
                {
                    return hit.collider.gameObject;
                }
            }
            return null;
        }
    }
}
