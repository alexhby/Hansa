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

        if (PlayerPrefs.GetString("EQUIPMENT1") != null)
        {
            GameInformation.EquipmentOne = (BaseEquipment) PPSerialization.Load("EQUIPMENT1");
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
