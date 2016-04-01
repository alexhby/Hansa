using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateNewCharacter : MonoBehaviour {

    private BaseCharacter newCharacter;
    private BasePlayer newPlayer;
    private bool isApprenticeClass;
    private bool isSquireClass;
    private bool isThiefClass;
    private bool isArcherClass;
    private string playerName = "Enter Name";

    // Use this for initialization
    void Start () {
        newCharacter = new BaseCharacter();
        newPlayer = new BasePlayer();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {

        playerName = GUILayout.TextArea(playerName,20);
        isApprenticeClass = GUILayout.Toggle(isApprenticeClass, "Apprentice Class");
        isSquireClass = GUILayout.Toggle(isSquireClass, "Squire Class");
        isThiefClass = GUILayout.Toggle(isThiefClass, "Thief Class");
        isArcherClass = GUILayout.Toggle(isArcherClass, "Archer Class");
        if (GUILayout.Button("Create"))
        {
            newCharacter.PlayerName = playerName;

            if (isApprenticeClass)
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Apprentice;
            }
            else if (isSquireClass)
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Squire;
            }
            else if (isThiefClass)
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Thief;
            }
            else if (isArcherClass)
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Archer;
            }

           
            InitNewCharacter(newCharacter);

            newPlayer.PlayerCharacter = newCharacter;

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
            Debug.Log("Current Quests Initialized");
            newPlayer.PlayerQuestLog.FinishedQuests = new List<Quest>();
            Debug.Log("Finished Quests Initialized");
            newPlayer.PlayerQuestLog.printAllQuests();
            
            
            


            StoreNewPlayerInfo();
            initShopsAndQuests();
            SaveInformation.SaveAllInformation();

            Debug.Log("player name : "+ newCharacter.PlayerName);
            Debug.Log("player class: " + newCharacter.PlayerClass.ToString());
            Debug.Log("player level: " + newCharacter.PlayerLevel);
            Debug.Log("player strength: " + newCharacter.Strength);
            Debug.Log("player agility: " + newCharacter.Agility);
            Debug.Log("player defense: " + newCharacter.Defense);
            Debug.Log("player intellect: " + newCharacter.Intellect);
            Debug.Log("player health: " + newCharacter.Health);
            Debug.Log("player gold: " + newPlayer.Gold);
            Debug.Log("player mana: " + newCharacter.Mana);

        }
        if (GUILayout.Button("Load"))
        {
            //Application.LoadLevel("loadTest");
            SceneManager.LoadScene("Store");
        }
        if (GUILayout.Button("TEST"))
        {
            GameInformation.PlayerQuestLog.printAllQuests();
            GameInformation.PlayerInventory.printInventory();
        }
    }

    private void initShopsAndQuests()
    {
        WorldInformation.AvailableQuests = new List<Quest>();
        WorldInformation.LoadNewQuests();
        WorldInformation.RenewShopInv();

    }
    private void StoreNewPlayerInfo()
    {
        GameInformation.PlayerCharacter = newCharacter;
        GameInformation.Gold = newPlayer.Gold;
        GameInformation.PlayerInventory = newPlayer.PlayerInventory;
        GameInformation.PlayerQuestLog = newPlayer.PlayerQuestLog;   
    }

    public static void InitNewCharacter(BaseCharacter newPlayer1)
    {

        newPlayer1.PlayerLevel = 1;
        newPlayer1.CurrentXP = 0;
        newPlayer1.RequiredXP = newPlayer1.PlayerLevel * newPlayer1.PlayerLevel + 7 * newPlayer1.PlayerLevel + 10;
        newPlayer1.Health = 50;
        newPlayer1.Mana = 50;
        SetClassStatsCharacter(newPlayer1);
        newPlayer1.AvailableStatPoints = 0;
    }

    

    public static void SetClassStatsCharacter(BaseCharacter newPlayer1)
    {
        if(newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Apprentice)
        {
            //Set apprentice stats
            newPlayer1.Strength = 1;
            newPlayer1.Agility = 2;
            newPlayer1.Intellect = 4;
            newPlayer1.Defense = 3;

        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Squire)
        {
            //Set squire stats
            newPlayer1.Strength = 4;
            newPlayer1.Agility = 2;
            newPlayer1.Intellect = 1;
            newPlayer1.Defense = 3;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Thief)
        {
            //Set thief stats
            newPlayer1.Strength = 3;
            newPlayer1.Agility = 4;
            newPlayer1.Intellect = 2;
            newPlayer1.Defense = 1;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Archer)
        {
            //Set archer stats
            newPlayer1.Strength = 3;
            newPlayer1.Agility = 3;
            newPlayer1.Intellect = 2;
            newPlayer1.Defense = 2;

        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Knight)
        {
            //Set knight stats
            newPlayer1.Strength = 8;
            newPlayer1.Agility = 4;
            newPlayer1.Intellect = 2;
            newPlayer1.Defense = 6;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Berserker)
        {
            //Set berserker stats
            newPlayer1.Strength = 13;
            newPlayer1.Agility = 11;
            newPlayer1.Intellect = 2;
            newPlayer1.Defense = 4;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Sniper)
        {
            //Set sniper stats
            newPlayer1.Strength = 5;
            newPlayer1.Agility = 6;
            newPlayer1.Intellect = 4;
            newPlayer1.Defense = 2;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Ninja)
        {
            //Set ninja stats
            newPlayer1.Strength = 6;
            newPlayer1.Agility = 9;
            newPlayer1.Intellect = 2;
            newPlayer1.Defense = 3;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.BlackMage)
        {
            //Set black mage stats
            newPlayer1.Strength = 2;
            newPlayer1.Agility = 4;
            newPlayer1.Intellect = 10;
            newPlayer1.Defense = 4;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.WhiteMage)
        {
            //Set white mage stats
            newPlayer1.Strength = 2;
            newPlayer1.Agility = 4;
            newPlayer1.Intellect = 8;
            newPlayer1.Defense = 6;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Paladin)
        {
            //Set paladin stats
            newPlayer1.Strength = 11;
            newPlayer1.Agility = 6;
            newPlayer1.Intellect = 3;
            newPlayer1.Defense = 10;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Darksword)
        {
            //Set Darksword stats
            newPlayer1.Strength = 12;
            newPlayer1.Agility = 5;
            newPlayer1.Intellect = 3;
            newPlayer1.Defense = 10;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.ArchMage)
        {
            //Set archmage stats
            newPlayer1.Strength = 3;
            newPlayer1.Agility = 6;
            newPlayer1.Intellect = 14;
            newPlayer1.Defense = 7;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Assassin)
        {
            //Set assassin stats
            newPlayer1.Strength = 9;
            newPlayer1.Agility = 14;
            newPlayer1.Intellect = 3;
            newPlayer1.Defense = 4;
        }
    }

    

}
