using UnityEngine;
using System.Collections;

public class SaveInformation {

    public static void SaveAllInformation()
    {
        PlayerPrefs.SetInt("PLAYERLEVEL", GameInformation.PlayerLevel);
        PlayerPrefs.SetString("PLAYERNAME",GameInformation.PlayerName);
        PlayerPrefs.SetInt("STRENGTH", GameInformation.Strength);
        PlayerPrefs.SetInt("AGILITY", GameInformation.Agility);
        PlayerPrefs.SetInt("INTELLECT", GameInformation.Intellect);
        PlayerPrefs.SetInt("DEFENSE", GameInformation.Defense);
        PlayerPrefs.SetInt("HEALTH", GameInformation.Health);
        PlayerPrefs.SetInt("MANA", GameInformation.Mana);
        PlayerPrefs.SetInt("GOLD", GameInformation.Gold);
        PlayerPrefs.SetInt("CURRENTXP", GameInformation.CurrentXP);
        PlayerPrefs.SetInt("REQUIREDXP", GameInformation.RequiredXP);
        
        if(GameInformation.PlayerInventory != null)
        {
            PPSerialization.Save("PLAYERINVENTORY", GameInformation.PlayerInventory);
        }
        if (GameInformation.PlayerQuestLog != null)
        {
            PPSerialization.Save("PLAYERQUESTLOG", GameInformation.PlayerQuestLog);
        }
        if (GameInformation.Helmet != null)
            PPSerialization.Save("HELMET", GameInformation.Helmet);
        if (GameInformation.Armor != null)
            PPSerialization.Save("ARMOR", GameInformation.Armor);
        if (GameInformation.Gauntlets != null)
            PPSerialization.Save("GAUNTLETS", GameInformation.Gauntlets);
        if (GameInformation.Grieves != null)
            PPSerialization.Save("GRIEVES", GameInformation.Grieves);
        if (GameInformation.Weapon != null)
            PPSerialization.Save("WEAPON", GameInformation.Weapon);



        
        if (GameInformation.Char1 != null)
            PPSerialization.Save("CHAR1", GameInformation.Char1);
        if (GameInformation.Char2 != null)
            PPSerialization.Save("CHAR2", GameInformation.Char2);
        if (GameInformation.Char3 != null)
            PPSerialization.Save("CHAR3", GameInformation.Char3);
        if (GameInformation.Char4 != null)
            PPSerialization.Save("CHAR4", GameInformation.Char4);
        if (GameInformation.Char5 != null)
            PPSerialization.Save("CHAR5", GameInformation.Char5);

        Debug.Log("SAVED ALL INFO!!");
    }

    public static void SaveInventoryInformation()
    {
        PlayerPrefs.SetInt("GOLD", GameInformation.Gold);
        if (GameInformation.PlayerInventory != null)
        {
            PPSerialization.Save("PLAYERINVENTORY", GameInformation.PlayerInventory);
        }
        if (GameInformation.Helmet != null)
            PPSerialization.Save("HELMET", GameInformation.Helmet);
        if (GameInformation.Armor != null)
            PPSerialization.Save("ARMOR", GameInformation.Armor);
        if (GameInformation.Gauntlets != null)
            PPSerialization.Save("GAUNTLETS", GameInformation.Gauntlets);
        if (GameInformation.Grieves != null)
            PPSerialization.Save("GRIEVES", GameInformation.Grieves);
        if (GameInformation.Weapon != null)
            PPSerialization.Save("WEAPON", GameInformation.Weapon);
        if (GameInformation.Char1 != null)
            PPSerialization.Save("CHAR1", GameInformation.Char1);
        if (GameInformation.Char2 != null)
            PPSerialization.Save("CHAR2", GameInformation.Char2);
        if (GameInformation.Char3 != null)
            PPSerialization.Save("CHAR3", GameInformation.Char3);
        if (GameInformation.Char4 != null)
            PPSerialization.Save("CHAR4", GameInformation.Char4);
        if (GameInformation.Char5 != null)
            PPSerialization.Save("CHAR5", GameInformation.Char5);
    }

    

    public static void SaveAllCharacterInformation()
    {
        
        if (GameInformation.Char1 != null)
            PPSerialization.Save("CHAR1", GameInformation.Char1);
        if (GameInformation.Char2 != null)
            PPSerialization.Save("CHAR2", GameInformation.Char2);
        if (GameInformation.Char3 != null)
            PPSerialization.Save("CHAR3", GameInformation.Char3);
        if (GameInformation.Char4 != null)
            PPSerialization.Save("CHAR4", GameInformation.Char4);
        if (GameInformation.Char5 != null)
            PPSerialization.Save("CHAR5", GameInformation.Char5);
    }

    //create simpler static functions that save after certain state changes


	
}
