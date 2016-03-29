using UnityEngine;
using System.Collections;

public static class IncreaseExperience {

    private static int xpToGive;
    private static LevelUp levelUpScript = new LevelUp();
    

    public static void AddExperience()
    {
        xpToGive = GameInformation.PlayerLevel * 100;
        GameInformation.CurrentXP += xpToGive;
        CheckLevelUp();
    }

    public static void CheckLevelUp()
    {
        if (GameInformation.CurrentXP >= GameInformation.RequiredXP)
        {
            
            Debug.Log("You've leveled up!");
            //create level up script
            levelUpScript.LevelUpCharacter();
        }
    }

  
	
}
