using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assembly_CSharp
{
    internal class OnEnter : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            gameObject.transform.GetChild(0).GetComponent<Material>().color = Color.red;
        }
        private void OnTriggerExit(Collider other)
        {
            
        }
    }
}
