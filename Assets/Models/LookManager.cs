using Assets.Characters;
using Assets.Models.Pots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

namespace Assets.Models
{
    class LookManager
    {
        private Text text;
        private GameObject activeObject;
        private Camera cam;
        private GameObject _doors;
        private bool doorsOpened = false;

        public LookManager(Text text, Camera cam, GameObject doors)
        {
            this.text = text;
            this.cam = cam;
            _doors = doors;
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
            if(obj != null && obj.tag == Consts.Tags.Boss)
            {
                activeObject = obj;
                Character boss = CharactersManager.characters.Where(p => p.GetObjectId() == activeObject.GetInstanceID()).FirstOrDefault();
                if(boss == null)
                {
                    return;
                }
                text.text = boss.GetInfo();
                text.gameObject.SetActive(true);
                return;
            }
            if(obj != null && obj.tag == Consts.Tags.Doors)
            {
                activeObject = obj;
                if(_doors != null)
                {
                    text.text = doorsOpened? Consts.Translations.closeDoors : Consts.Translations.openDoors;
                    text.gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        OpenOrCloseDoors();
                    }
                }
                return;
            }
            activeObject = null;
            text.gameObject.SetActive(false);
        }
        private void OpenOrCloseDoors()
        {
            if (doorsOpened)
            {
                _doors.transform.Translate(new Vector3(-2.5f, 0, -2.5f));
                _doors.transform.Rotate(new Vector3(0, -90, 0));
                //Quaternion rotation = new Quaternion(0f, 180f, 0f, 0f);
                ////rotation.y = rotation.y - 90;
                //_doors.transform.rotation = rotation;
                //_doors.transform.Translate(new Vector3(0.55f, 0, -0.593f));
                //Vector3 pos = _doors.transform.position;
                //pos.z = pos.z - 0.593f;
                //pos.x = pos.x + 0.55f;
                //_doors.transform.position = pos;
                doorsOpened = false;
            }
            else
            {
                _doors.transform.Rotate(new Vector3(0, 90, 0));
                //Quaternion rotation = new Quaternion(0f,270f,0f,0f);
                ////rotation.y = rotation.y + 90f;
                //_doors.transform.rotation = rotation;
                _doors.transform.Translate(new Vector3(2.5f, 0, 2.5f));
                //Vector3 pos = _doors.transform.position;
                //pos.z = pos.z + 0.593f;
                //pos.x = pos.x - 0.55f;
                //_doors.transform.position = pos;
                doorsOpened = true;
            }
        }
        private GameObject PlayerLookingAtObj()
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 5f);
            if (hit.collider != null)
            {
                return hit.collider.gameObject;
            }
            return null;
        }
        public void ChangeNewPotPlace()
        {
            PotsManager.ChangeNewPotPlace(cam.transform.position, cam.transform.forward);
        }
    }
}
