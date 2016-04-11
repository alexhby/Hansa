using UnityEngine;
using System.Collections;

public class SaveInformation {

    public static void SaveAllInformation()
    {
        PlayerPrefs.SetInt("GOLD", GameInformation.Gold);
        if (GameInformation.PlayerInventory != null)
        {
            PPSerialization.Save("PLAYERINVENTORY", GameInformation.PlayerInventory);
        }
        if (GameInformation.PlayerQuestLog != null)
        {
            PPSerialization.Save("PLAYERQUESTLOG", GameInformation.PlayerQuestLog);
        }
        if (GameInformation.PlayerCharacter != null)
            PPSerialization.Save("PLAYERCHARACTER", GameInformation.PlayerCharacter);
       
        if (GameInformation.Char1 != null)
            PPSerialization.Save("CHAR01", GameInformation.Char1);
        if (GameInformation.Char2 != null)
            PPSerialization.Save("CHAR02", GameInformation.Char2);
        if (GameInformation.Char3 != null)
            PPSerialization.Save("CHAR03", GameInformation.Char3);
        if (GameInformation.Char4 != null)
            PPSerialization.Save("CHAR04", GameInformation.Char4);
        if (GameInformation.Char5 != null)
            PPSerialization.Save("CHAR05", GameInformation.Char5);

        Debug.Log("SAVED ALL INFO!!");
    }

    public static void SaveInventoryInformation()
    {
        if (GameInformation.PlayerQuestLog != null)
        {
            PPSerialization.Save("PLAYERQUESTLOG", GameInformation.PlayerQuestLog);
        }
        PlayerPrefs.SetInt("GOLD", GameInformation.Gold);
        if (GameInformation.PlayerInventory != null)
        {
            PPSerialization.Save("PLAYERINVENTORY", GameInformation.PlayerInventory);
        }
        
        if (GameInformation.PlayerCharacter != null)
            PPSerialization.Save("PLAYERCHARACTER", GameInformation.PlayerCharacter);
        if (GameInformation.Char1 != null)
            PPSerialization.Save("CHAR01", GameInformation.Char1);
        if (GameInformation.Char2 != null)
            PPSerialization.Save("CHAR02", GameInformation.Char2);
        if (GameInformation.Char3 != null)
            PPSerialization.Save("CHAR03", GameInformation.Char3);
        if (GameInformation.Char4 != null)
            PPSerialization.Save("CHAR04", GameInformation.Char4);
        if (GameInformation.Char5 != null)
            PPSerialization.Save("CHAR05", GameInformation.Char5);
    }

    

    public static void SaveAllCharacterInformation()
    {
        if (GameInformation.PlayerCharacter != null)
            PPSerialization.Save("PLAYERCHARACTER", GameInformation.PlayerCharacter);

        if (GameInformation.Char1 != null)
            PPSerialization.Save("CHAR01", GameInformation.Char1);
        if (GameInformation.Char2 != null)
            PPSerialization.Save("CHAR02", GameInformation.Char2);
        if (GameInformation.Char3 != null)
            PPSerialization.Save("CHAR03", GameInformation.Char3);
        if (GameInformation.Char4 != null)
            PPSerialization.Save("CHAR04", GameInformation.Char4);
        if (GameInformation.Char5 != null)
            PPSerialization.Save("CHAR05", GameInformation.Char5);
    }

    //create simpler static functions that save after certain state changes


	
}
