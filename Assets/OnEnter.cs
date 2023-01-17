using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assembly_CSharp
{
    internal class OnEnter : MonoBehaviour
    {
        private bool CanPlace = true;
        private void OnTriggerEnter(Collider other)
        {
            var childTransform = gameObject.transform.GetChild(0);
            Renderer renderer = childTransform.GetComponent<Renderer>();
            renderer.material.color = Color.red;
            CanPlace = false;
        }
        private void OnTriggerExit(Collider other)
        {
            var childTransform = gameObject.transform.GetChild(0);
            Renderer renderer = childTransform.GetComponent<Renderer>();
            renderer.material.color = Color.green;
            CanPlace = true;
        }
        public bool CheckCanPlace()
        {
            return CanPlace;
        }
    }
}
