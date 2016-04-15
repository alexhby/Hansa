using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NewCharacterMenu : MonoBehaviour
{
    private BaseCharacter newCharacter;
    private BasePlayer newPlayer;
    public GameObject playernameField;
    public GameObject classDD;
    public GameObject newCharacterMenuPanel;
    public GameObject mainMenuPanel;

    // Use this for initialization
    void Start()
    {
        newCharacter = new BaseCharacter();
        newPlayer = new BasePlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void InitNewCharacter(BaseCharacter newPlayer1, string playername)
    {

        newPlayer1.PlayerLevel = 1;
        newPlayer1.CurrentXP = 0;
        newPlayer1.RequiredXP = newPlayer1.PlayerLevel * newPlayer1.PlayerLevel + 7 * newPlayer1.PlayerLevel + 10;
        newPlayer1.Health = 50;
        newPlayer1.Mana = 50;
        newPlayer1.PlayerName = playername;
        newPlayer1.CurrentHealth = 50;
        SetClassStatsCharacter(newPlayer1);
    }
    public void newCharacterButtonClicked()
    {
        
        
       

        int classChoice = classDD.GetComponent<Dropdown>().value; // get value from dropdown
        
        string playername = playernameField.GetComponent<InputField>().text; // get value from charnamefield
        if(playername.Length > 0)
        {
            
            switch (classChoice)
            {
                case 0: // squire
                    newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Squire;
                    break;
                case 1: // apprentice
                    newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Apprentice;
                    break;
                case 2: // thief
                    newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Thief;
                    break;
                case 3: // archer
                    newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Archer;
                    break;
            }
            InitNewCharacter(newCharacter, playername);
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

            // go to main menu
            newCharacterMenuPanel.SetActive(false);
            mainMenuPanel.SetActive(true);

        } else
        {
            // empty character name - error windows is shown
            
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

    public static void SetClassStatsCharacter(BaseCharacter newPlayer1)
    {
        if (newPlayer1.PlayerClass == BaseCharacterClass.CharacterClasses.Apprentice)
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