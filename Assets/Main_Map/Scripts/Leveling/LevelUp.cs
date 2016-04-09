using UnityEngine;
using System.Collections;

//Base logic for levelling up
public class LevelUp {

	public void LevelUpCharacter(BaseCharacter character)
    {
        //Check if current xp > req
        if(character.CurrentXP >= character.RequiredXP)
        {
            character.PlayerLevel++;
            character.CurrentXP -= character.RequiredXP;
        }
        //Give player stat points
        character.AvailableStatPoints++;
        SaveInformation.SaveAllCharacterInformation();

        //Increase Health Mana!
         

        //ability based on level? 
        //money?
        //determine next amt of req exp
        DetermineRequiredXP(character);

    }

    private void DetermineRequiredXP(BaseCharacter character)
    {
        int temp = character.PlayerLevel * character.PlayerLevel +7* character.PlayerLevel+ 10;

        character.RequiredXP = temp;
        IncreaseExperience.CheckLevelUp(character);

    }
}
