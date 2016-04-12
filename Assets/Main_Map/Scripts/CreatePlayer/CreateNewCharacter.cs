using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//CREATES NEW CHARACTER -- INITIALIZES ALL PROPERTIES to their base levels
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
            
            // SET CLASSES
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

            //Sets the player character to this new character
            newPlayer.PlayerCharacter = newCharacter;

            //init Abilities
            

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

    
    private void StoreNewPlayerInfo()
    {
        GameInformation.PlayerCharacter = newCharacter;
        GameInformation.Gold = newPlayer.Gold;
        GameInformation.PlayerInventory = newPlayer.PlayerInventory;
        GameInformation.PlayerQuestLog = newPlayer.PlayerQuestLog;   
    }

    public static void InitNewCharacter(BaseCharacter newPlayer1)
    {

        //Sets all standard info!
        newPlayer1.PlayerLevel = 1;
        newPlayer1.CurrentXP = 0;
        newPlayer1.RequiredXP = newPlayer1.PlayerLevel * newPlayer1.PlayerLevel + 7 * newPlayer1.PlayerLevel + 10;
        newPlayer1.Health = 50;
        newPlayer1.Mana = 50;
        newPlayer1.CurrentHealth = 50;
        SetClassStatsCharacter(newPlayer1);
        newPlayer1.AvailableStatPoints = 0;
        newPlayer1.Abilities = new List<AbilityTommy>();
    }

    private void InitLeveledCharacter(int level)
    {
        //Sets all standard info!
        newCharacter = new BaseCharacter();
        newCharacter.PlayerLevel = level;
        int classDecison = Random.Range(1, 4);
        if(level < 12)
        {
            //tier 1 class
            if(classDecison == 1)
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Apprentice;
            }
            else if(classDecison == 2)
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Archer;
            }
            else if(classDecison == 3)
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Squire;
            }
            else
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Thief;
            }
        }
        else if (level < 20)
        {
            //tier 2 class
            if (classDecison == 1)
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Mage;
            }
            else if (classDecison == 2)
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Hunter;
            }
            else if (classDecison == 3)
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Knight;
            }
            else
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Ninja;
            }
        }
        else
        {
            //tier 3 class
            if (classDecison == 1)
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.ArchMage;
            }
            else if (classDecison == 2)
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Sniper;
            }
            else if (classDecison == 3)
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Paladin;
            }
            else
            {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Assassin;
            }
        }
        newCharacter.CurrentXP = 0;
        newCharacter.RequiredXP = newCharacter.PlayerLevel * newCharacter.PlayerLevel + 7 * newCharacter.PlayerLevel + 10;
        newCharacter.Health = 50 + level * 10;
        newCharacter.Mana = 50 + level * 10;
        newCharacter.CurrentHealth = newCharacter.Health;
        SetClassStatsCharacter(newCharacter);
        newCharacter.AvailableStatPoints = 0;
        newCharacter.Abilities = new List<AbilityTommy>();
    }

    public BaseCharacter ReturnNewEnemy(int level)
    {
        InitLeveledCharacter(level);
        newCharacter.PlayerName = "Enemy";
        return newCharacter;
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
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Hunter)
        {
            //Set Hunter stats
            newPlayer1.Strength = 6;
            newPlayer1.Agility = 7;
            newPlayer1.Intellect = 4;
            newPlayer1.Defense = 3;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Sniper)
        {
            //Set sniper stats
            newPlayer1.Strength = 8;
            newPlayer1.Agility = 10;
            newPlayer1.Intellect = 6;
            newPlayer1.Defense = 6;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Ninja)
        {
            //Set ninja stats
            newPlayer1.Strength = 4;
            newPlayer1.Agility = 11;
            newPlayer1.Intellect = 2;
            newPlayer1.Defense = 3;
        }
        else if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Mage)
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
