using Assets;
using Assets.Models.Pots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var pots = GameObject.FindGameObjectsWithTag(Consts.Tags.Pot);
        for (int i = 0; i < pots.Length; i++)
        {
            Pot pot = new Pot(pots[i]);
            PotsManager.pots.Add(pot);
        }
    }
    void Update()
    {
        for (int i = 0; i < PotsManager.pots.Count; i++)
        {
            PotsManager.pots[i].UpdatePotState();
        }
    }
}
