using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateNewCharacter : MonoBehaviour {

    private BasePlayer newPlayer;
    private bool isMageClass;
    private bool isWarriorClass;
    private bool isThiefClass;
    private bool isArcherClass;
    private string playerName = "Enter Name";

    // Use this for initialization
    void Start () {
        newPlayer = new BasePlayer();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {

        playerName = GUILayout.TextArea(playerName,20);
        isMageClass = GUILayout.Toggle(isMageClass, "Mage Class");
        isWarriorClass = GUILayout.Toggle(isWarriorClass, "Warrior Class");
        isThiefClass = GUILayout.Toggle(isThiefClass, "Thief Class");
        isArcherClass = GUILayout.Toggle(isArcherClass, "Archer Class");
        if (GUILayout.Button("Create"))
        {
            newPlayer.PlayerName = playerName;

            if (isMageClass)
            {
                newPlayer.PlayerClass = new BaseMageClass();
            }
            else if (isWarriorClass)
            {
                newPlayer.PlayerClass = new BaseWarriorClass();
            }
            else if (isThiefClass)
            {
                newPlayer.PlayerClass = new BaseThiefClass();
            }
            else if (isArcherClass)
            {
                newPlayer.PlayerClass = new BaseArcherClass();
            }

           
            CreateNewPlayer(newPlayer);

            //init inventory
            newPlayer.PlayerInventory = new Inventory();
            newPlayer.PlayerInventory.Weapons = new List<BaseWeapon>();
            newPlayer.PlayerInventory.Equipment = new List<BaseEquipment>();
            newPlayer.PlayerInventory.Potions = new List<BasePotion>();
            newPlayer.PlayerInventory.printInventory();
            Debug.Log("INVENTORY INITIALIZED");

            //init quest log
            newPlayer.PlayerQuestLog = new QuestLog();
            newPlayer.PlayerQuestLog.CurrentQuests = new List<Quest>();
            newPlayer.PlayerQuestLog.FinishedQuests = new List<Quest>();
            


            StoreNewPlayerInfo();
            SaveInformation.SaveAllInformation();

            Debug.Log("player name : "+ newPlayer.PlayerName);
            Debug.Log("player class: " + newPlayer.PlayerClass.CharacterClassName);
            Debug.Log("player level: " + newPlayer.PlayerLevel);
            Debug.Log("player strength: " + newPlayer.Strength);
            Debug.Log("player agility: " + newPlayer.Agility);
            Debug.Log("player defense: " + newPlayer.Defense);
            Debug.Log("player intellect: " + newPlayer.Intellect);
            Debug.Log("player health: " + newPlayer.Health);
            Debug.Log("player gold: " + newPlayer.Gold);
            Debug.Log("player mana: " + newPlayer.Mana);

        }
        if (GUILayout.Button("Load"))
        {
            //Application.LoadLevel("loadTest");
            SceneManager.LoadScene("Store");
        }
        if (GUILayout.Button("TEST"))
        {
            GameInformation.PlayerInventory.printInventory();
        }
    }


    private void StoreNewPlayerInfo()
    {
        GameInformation.PlayerName = newPlayer.PlayerName;
        GameInformation.PlayerLevel = newPlayer.PlayerLevel;
        GameInformation.Strength = newPlayer.Strength;
        GameInformation.Agility = newPlayer.Agility;
        GameInformation.Intellect = newPlayer.Intellect;
        GameInformation.Defense = newPlayer.Defense;
        GameInformation.Health = newPlayer.Health;
        GameInformation.Mana = newPlayer.Mana;
        GameInformation.Gold = newPlayer.Gold;
        GameInformation.PlayerInventory = newPlayer.PlayerInventory;
        GameInformation.CurrentXP = newPlayer.CurrentXP;
        GameInformation.RequiredXP = newPlayer.RequiredXP;
        GameInformation.PlayerQuestLog = newPlayer.PlayerQuestLog;

       
    }

    public static void CreateNewPlayer(BasePlayer newPlayer1)
    {
        
        newPlayer1.PlayerLevel = 1;
        newPlayer1.CurrentXP = 0;
        newPlayer1.RequiredXP = newPlayer1.PlayerLevel * newPlayer1.PlayerLevel + 7 * newPlayer1.PlayerLevel + 10;
        newPlayer1.Health = 50;
        newPlayer1.Mana = 50;
        newPlayer1.Gold = 0;
        newPlayer1.Strength = newPlayer1.PlayerClass.Strength;
        newPlayer1.Agility = newPlayer1.PlayerClass.Agility;
        newPlayer1.Intellect = newPlayer1.PlayerClass.Intellect;
        newPlayer1.Defense = newPlayer1.PlayerClass.Defense;
    }

    
}
