using UnityEngine;
using System.Collections;

public class LoadInformation {
    
    
    public static void LoadAllInformation()
    {
        
        
        GameInformation.Gold = PlayerPrefs.GetInt("GOLD");

       

        //player inv , all armor and inventory
        if (PlayerPrefs.GetString("PLAYERINVENTORY") != null)
        {
            GameInformation.PlayerInventory = (Inventory)PPSerialization.Load("PLAYERINVENTORY");
        }
        if (PlayerPrefs.GetString("PLAYERQUESTLOG") != null)
        {
            GameInformation.PlayerQuestLog = (QuestLog)PPSerialization.Load("PLAYERQUESTLOG");
        }
        
        if (PlayerPrefs.GetString("PLAYERCHARACTER") != null)
        {
            GameInformation.PlayerCharacter = (BaseCharacter)PPSerialization.Load("PLAYERCHARACTER");
        }

        
        if (PlayerPrefs.GetString("CHAR01") != null)
        {
            GameInformation.Char1 = (BaseCharacter)PPSerialization.Load("CHAR01");
        }
        if (PlayerPrefs.GetString("CHAR02") != null)
        {
            GameInformation.Char2 = (BaseCharacter)PPSerialization.Load("CHAR02");
        }
        if (PlayerPrefs.GetString("CHAR03") != null)
        {
            GameInformation.Char3 = (BaseCharacter)PPSerialization.Load("CHAR03");
        }
        if (PlayerPrefs.GetString("CHAR04") != null)
        {
            GameInformation.Char4 = (BaseCharacter)PPSerialization.Load("CHAR04");
        }
        if (PlayerPrefs.GetString("CHAR05") != null)
        {
            GameInformation.Char5 = (BaseCharacter)PPSerialization.Load("CHAR05");
        }
    }

    public static void LoadInventoryInformation()
    {
        GameInformation.Gold = PlayerPrefs.GetInt("GOLD");
        if (PlayerPrefs.GetString("PLAYERQUESTLOG") != null)
        {
            GameInformation.PlayerQuestLog = (QuestLog)PPSerialization.Load("PLAYERQUESTLOG");
        }
        if (PlayerPrefs.GetString("PLAYERINVENTORY") != null)
        {
            GameInformation.PlayerInventory = (Inventory)PPSerialization.Load("PLAYERINVENTORY");
        }
       
        if (PlayerPrefs.GetString("PLAYERCHARACTER") != null)
        {
            GameInformation.PlayerCharacter = (BaseCharacter)PPSerialization.Load("PLAYERCHARACTER");
        }
        if (PlayerPrefs.GetString("CHAR01") != null)
        {
            GameInformation.Char1 = (BaseCharacter)PPSerialization.Load("CHAR01");
        }
        if (PlayerPrefs.GetString("CHAR02") != null)
        {
            GameInformation.Char2 = (BaseCharacter)PPSerialization.Load("CHAR02");
        }
        if (PlayerPrefs.GetString("CHAR03") != null)
        {
            GameInformation.Char3 = (BaseCharacter)PPSerialization.Load("CHAR03");
        }
        if (PlayerPrefs.GetString("CHAR04") != null)
        {
            GameInformation.Char4 = (BaseCharacter)PPSerialization.Load("CHAR04");
        }
        if (PlayerPrefs.GetString("CHAR05") != null)
        {
            GameInformation.Char5 = (BaseCharacter)PPSerialization.Load("CHAR05");
        }
    }

}
