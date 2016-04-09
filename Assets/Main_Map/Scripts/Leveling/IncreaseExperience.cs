using UnityEngine;
using System.Collections;

//Awarding experience
public static class IncreaseExperience {

    private static int xpToGive;
    private static LevelUp levelUpScript = new LevelUp();
    

    public static void AddExperience(BaseCharacter character)
    {
        xpToGive = character.PlayerLevel * 100;
        character.CurrentXP += xpToGive;
        CheckLevelUp(character);
    }

    public static void CheckLevelUp(BaseCharacter character)
    {
        if (character.CurrentXP >= character.RequiredXP)
        {
            
            Debug.Log("You've leveled up!");
            //create level up script
            levelUpScript.LevelUpCharacter(character);
        }
    }

  
	
}
