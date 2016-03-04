using UnityEngine;
using System.Collections;

public class TestLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LoadInformation.LoadAllInformation();
        Debug.Log("player name : "+ GameInformation.PlayerName);
        //Debug.Log("player class: " + newPlayer.PlayerClass.CharacterClassName);
        Debug.Log("player level: " + GameInformation.PlayerLevel);
        Debug.Log("player strength: " + GameInformation.Strength);
        Debug.Log("player agility: " + GameInformation.Agility);
        Debug.Log("player defense: " + GameInformation.Defense);
        Debug.Log("player intellect: " + GameInformation.Intellect);
        Debug.Log("player health: " + GameInformation.Health);
        Debug.Log("player mana: " + GameInformation.Mana);
        Debug.Log("player gold: " + GameInformation.Gold);
        Debug.Log("OK NOW FOR YOUR SIDE PERSON......");
        if (GameInformation.Char1 != null)
        {
            Debug.Log("side1 level: " + GameInformation.Char1.PlayerLevel);
            Debug.Log("side1 Strength: " + GameInformation.Char1.Strength);
            Debug.Log("side1 Agility: " + GameInformation.Char1.Agility);
            Debug.Log("side1 Defense: " + GameInformation.Char1.Defense);
            Debug.Log("side1 Intellect: " + GameInformation.Char1.Intellect);
            Debug.Log("side1 Health: " + GameInformation.Char1.Health);
            Debug.Log("side1 Mana: " + GameInformation.Char1.Mana);
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
