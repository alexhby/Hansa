using UnityEngine;
using System.Collections;

public class LevelUp {

	public void LevelUpCharacter()
    {
        //Check if current xp > req
        if(GameInformation.CurrentXP >= GameInformation.RequiredXP)
        {
            GameInformation.PlayerLevel++;
            GameInformation.CurrentXP -= GameInformation.RequiredXP;
        }
        //Give player stat points

        //Increase Health Mana!
         

        //ability based on level? 
        //money?
        //determine next amt of req exp
        DetermineRequiredXP();

    }

    private void DetermineRequiredXP()
    {
        int temp = GameInformation.PlayerLevel * GameInformation.PlayerLevel +7*GameInformation.PlayerLevel+ 10;

        GameInformation.RequiredXP = temp;
        IncreaseExperience.CheckLevelUp();

    }
}
