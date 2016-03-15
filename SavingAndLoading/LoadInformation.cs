using UnityEngine;
using System.Collections;

public class LoadInformation {
    
    
    public static void LoadAllInformation()
    {
        
        GameInformation.PlayerName = PlayerPrefs.GetString("PLAYERNAME");
        GameInformation.PlayerLevel = PlayerPrefs.GetInt("PLAYERLEVEL");
        GameInformation.Strength = PlayerPrefs.GetInt("STRENGTH");
        GameInformation.Intellect = PlayerPrefs.GetInt("INTELLECT");
        GameInformation.Agility = PlayerPrefs.GetInt("AGILITY");
        GameInformation.Defense = PlayerPrefs.GetInt("DEFENSE");
        GameInformation.Health = PlayerPrefs.GetInt("HEALTH");
        GameInformation.Mana = PlayerPrefs.GetInt("MANA");
        GameInformation.Gold = PlayerPrefs.GetInt("GOLD");

        GameInformation.CurrentXP = PlayerPrefs.GetInt("CURRENTXP");
        GameInformation.RequiredXP = PlayerPrefs.GetInt("REQUIREDXP");

        //player inv , all armor and inventory
        if (PlayerPrefs.GetString("PLAYERINVENTORY") != null)
        {
            GameInformation.PlayerInventory = (Inventory)PPSerialization.Load("PLAYERINVENTORY");
        }
        if (PlayerPrefs.GetString("PLAYERQUESTLOG") != null)
        {
            GameInformation.PlayerQuestLog = (QuestLog)PPSerialization.Load("PLAYERQUESTLOG");
        }
        if (PlayerPrefs.GetString("HELMET") != null)
        {
            GameInformation.Helmet = (BaseEquipment)PPSerialization.Load("HELMET");
        }
        if (PlayerPrefs.GetString("ARMOR") != null)
        {
            GameInformation.Armor = (BaseEquipment)PPSerialization.Load("ARMOR");
        }
        if (PlayerPrefs.GetString("GAUNTLETS") != null)
        {
            GameInformation.Gauntlets = (BaseEquipment)PPSerialization.Load("GAUNTLETS");
        }
        if (PlayerPrefs.GetString("GRIEVES") != null)
        {
            GameInformation.Grieves = (BaseEquipment)PPSerialization.Load("GRIEVES");
        }
        if (PlayerPrefs.GetString("WEAPON") != null)
        {
            GameInformation.Weapon = (BaseWeapon)PPSerialization.Load("WEAPON");
        }

        
        if (PlayerPrefs.GetString("CHAR1") != null)
        {
            GameInformation.Char1 = (BasePlayer)PPSerialization.Load("CHAR1");
        }
        if (PlayerPrefs.GetString("CHAR2") != null)
        {
            GameInformation.Char2 = (BasePlayer)PPSerialization.Load("CHAR2");
        }
        if (PlayerPrefs.GetString("CHAR3") != null)
        {
            GameInformation.Char3 = (BasePlayer)PPSerialization.Load("CHAR3");
        }
        if (PlayerPrefs.GetString("CHAR4") != null)
        {
            GameInformation.Char4 = (BasePlayer)PPSerialization.Load("CHAR4");
        }
        if (PlayerPrefs.GetString("CHAR5") != null)
        {
            GameInformation.Char5 = (BasePlayer)PPSerialization.Load("CHAR5");
        }
    }

    public static void LoadInventoryInformation()
    {
        GameInformation.Gold = PlayerPrefs.GetInt("GOLD");
        if (PlayerPrefs.GetString("PLAYERINVENTORY") != null)
        {
            GameInformation.PlayerInventory = (Inventory)PPSerialization.Load("PLAYERINVENTORY");
        }
        if (PlayerPrefs.GetString("HELMET") != null)
        {
            GameInformation.Helmet = (BaseEquipment)PPSerialization.Load("HELMET");
        }
        if (PlayerPrefs.GetString("ARMOR") != null)
        {
            GameInformation.Armor = (BaseEquipment)PPSerialization.Load("ARMOR");
        }
        if (PlayerPrefs.GetString("GAUNTLETS") != null)
        {
            GameInformation.Gauntlets = (BaseEquipment)PPSerialization.Load("GAUNTLETS");
        }
        if (PlayerPrefs.GetString("GRIEVES") != null)
        {
            GameInformation.Grieves = (BaseEquipment)PPSerialization.Load("GRIEVES");
        }
        if (PlayerPrefs.GetString("WEAPON") != null)
        {
            GameInformation.Weapon = (BaseWeapon)PPSerialization.Load("WEAPON");
        }
        if (PlayerPrefs.GetString("CHAR1") != null)
        {
            GameInformation.Char1 = (BasePlayer)PPSerialization.Load("CHAR1");
        }
        if (PlayerPrefs.GetString("CHAR2") != null)
        {
            GameInformation.Char2 = (BasePlayer)PPSerialization.Load("CHAR2");
        }
        if (PlayerPrefs.GetString("CHAR3") != null)
        {
            GameInformation.Char3 = (BasePlayer)PPSerialization.Load("CHAR3");
        }
        if (PlayerPrefs.GetString("CHAR4") != null)
        {
            GameInformation.Char4 = (BasePlayer)PPSerialization.Load("CHAR4");
        }
        if (PlayerPrefs.GetString("CHAR5") != null)
        {
            GameInformation.Char5 = (BasePlayer)PPSerialization.Load("CHAR5");
        }
    }
	
}
