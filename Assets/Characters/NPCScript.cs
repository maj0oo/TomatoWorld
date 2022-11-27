using Assets;
using Assets.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    // Start is called before the first frame update
    CharactersManager charactersMngr;
    void Start()
    {
        charactersMngr = new CharactersManager();
        var answersPanel = GameObject.Find(Consts.Tags.AnswersPanel);
        answersPanel.SetActive(false);
        var dealers = GameObject.FindGameObjectsWithTag(Consts.Tags.Dealer);
        var bosses = GameObject.FindGameObjectsWithTag(Consts.Tags.Boss);
        for(int i = 0; i < dealers.Length; i++)
        {
            Character character = new Character(dealers[i], CharacterType.dealer, answersPanel, charactersMngr);
            CharactersManager.characters.Add(character);   
        }
        for(int i = 0; i < bosses.Length; i++)
        {
            Character character = new Character(bosses[i], CharacterType.boss, answersPanel, charactersMngr);
            CharactersManager.characters.Add(character);
        }
    }

    // Update is called once per frame
    void Update()
    {
        charactersMngr.RefreshPanel();
    }
}
