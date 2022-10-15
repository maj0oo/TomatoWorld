using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        if (!controller)
        {
            Debug.LogError($"PlayerController is not set");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(controller.transform.position, transform.position);
        if(distance < 3)
        {
            Debug.Log($"Player is close to bloack: " + distance);
        }
    }
}
