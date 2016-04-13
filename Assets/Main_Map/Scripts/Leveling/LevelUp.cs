using UnityEngine;
using System.Collections;

//Base logic for levelling up
public class LevelUp {

	public bool LevelUpCharacter(BaseCharacter character)
    {
        //Check if current xp > req
        if(character.CurrentXP >= character.RequiredXP)
        {
            character.PlayerLevel++;
            character.CurrentXP -= character.RequiredXP;

                    //Give player stat points
        character.AvailableStatPoints++;

        character.Health += 20;

        SaveInformation.SaveAllCharacterInformation();


            return true;
        }

        //Increase Health Mana!
         

        //ability based on level? 
        //money?
        //determine next amt of req exp
        //DetermineRequiredXP(character);
        return false;

    }

    private void DetermineRequiredXP(BaseCharacter character)
    {
        int temp = 100;

        character.RequiredXP = temp;
        IncreaseExperience.CheckLevelUp(character);

    }
}
