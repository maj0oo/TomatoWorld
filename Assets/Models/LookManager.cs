using Assets.Characters;
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
            var obj = PlayerLookingAtObj();
            if (obj != null && obj.tag == Consts.Tags.Pot)
            {
                activeObject = obj;
                Pot pot = PotsManager.pots.Where(p => p.GetObjectId() == activeObject.GetInstanceID()).FirstOrDefault();
                if(pot == null)
                {
                    return;
                }
                text.text = pot.GetInfo();
                text.gameObject.SetActive(true);

                return;
            }
            if(obj != null && obj.tag == Consts.Tags.Dealer)
            {
                activeObject = obj;
                Character character = CharactersManager.characters.Where(p => p.GetObjectId() == activeObject.GetInstanceID()).FirstOrDefault();
                if(character == null)
                {
                    return;
                }
                text.text = character.GetInfo();
                text.gameObject.SetActive(true);
                return;
            }
            activeObject = null;
            text.gameObject.SetActive(false);
        }
        private GameObject PlayerLookingAtObj()
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 5f);
            if (hit.collider != null)
            {
                return hit.collider.gameObject;
                //if (hit.collider.gameObject.tag == tag)
                //{
                //    return hit.collider.gameObject;
                //}
            }
            return null;
        }
    }
}
