using UnityEngine;

using System.Collections;


//CREATES NEW QUEST -- INITIALIZES ALL PROPERTIES -- Randomizes based on level
public class CreateNewQuest  {

    private Quest newQuest;

    //returns randomly generated quest
    public Quest returnQuest()
    {
        CreateQuest();
        //Debug.Log("Name: " + newQuest.QuestName);//+ "    ----  Alliance: " + newQuest.QuestAlliance.KingName);
        return newQuest;
    }

    //returns a control quest
    public Quest returnControl()
    {
        controlEvent();
        //Debug.Log("Name: " + newQuest.QuestName + "    ----  Alliance: " + newQuest.QuestAlliance.KingName);
        return newQuest;
    }

    //Fills out special types for a control event
    private void controlEvent()
    {
        newQuest = new Quest();
        newQuest.QuestType = Quest.QuestTypes.Control;

        //Gets a random integer
        int randTemp = Random.Range(0, WorldInformation.Kingdoms.Count);

        //finds a kingdom that the integer corresponds to in the globally stored kingdoms list
        //This will be the attacking kingdom
        newQuest.QuestAlliance = WorldInformation.Kingdoms[randTemp];

        //finds an area that IS NOT a city and isn't owned by this kingdom
        newQuest.QuestLocation = WorldInformation.Areas.Find(x => x.AreaType != Area.AreaTypes.City && x.OwnedBy != newQuest.QuestAlliance);
        newQuest.QuestEnemy = newQuest.QuestLocation.OwnedBy;
        
        //set name and descriptions
        newQuest.QuestName = "Control: " + newQuest.QuestLocation.AreaName +"!";
        newQuest.QuestDescription = newQuest.QuestLocation.AreaName + " has fallen into a state of disarray and " + newQuest.QuestAlliance.KingName + " is making a move!";

        newQuest.RecommendedLevel = GameInformation.PlayerCharacter.PlayerLevel;

        //high gold reward (esp for lower levels)
        newQuest.GoldReward = newQuest.RecommendedLevel * newQuest.RecommendedLevel + 300;

    }

    //Creates a new randomly generated quest
    private void CreateQuest()
    {
        newQuest = new Quest();

        //Location -- where on the map the quest will be located
        DetermineLocation();

        //Type  
        DetermineType();

        //Alliance       
        newQuest.QuestAlliance = WorldInformation.Kingdoms.Find(x =>x.KingdomID == newQuest.QuestLocation.OwnedBy.KingdomID);

        //Enemy
        //No enemy for normal quests

        //Name
        DetermineName();



        //Description
        DetermineDescription();

        //Recommended Lvl
        DetermineRecommendedLevel();

        //Gold reward
        DetermineGold();
        //weapon, equipment, or potion reward
        DetermineReward();

    }

    private void DetermineReward()
    {
        int randTemp = Random.Range(1, 4);
        CreateNewWeapon cw = new CreateNewWeapon();
        CreateNewPotion cp = new CreateNewPotion();
        CreateNewEquipment ce = new CreateNewEquipment();

        if (newQuest.QuestType == Quest.QuestTypes.CaravanProtect)
        {
            newQuest.GoldReward = 21 * newQuest.RecommendedLevel;
            if (randTemp == 4)
                newQuest.PotionReward = cp.returnPotion();
        }
        else if (newQuest.QuestType == Quest.QuestTypes.Delivery)
        {
            newQuest.GoldReward = 16 * newQuest.RecommendedLevel;
            if (randTemp < 3)
                newQuest.PotionReward = cp.returnPotion();
            else if (randTemp == 3)
                newQuest.EquipmentReward = ce.returnEquipment();
            else
                newQuest.WeaponReward = cw.returnWeapon();
        }
        else if (newQuest.QuestType == Quest.QuestTypes.TreasureMapGetMap)
        {
            newQuest.WeaponReward = cw.returnWeapon();
            newQuest.EquipmentReward = ce.returnEquipment();

        }
    }

    private void DetermineGold()
    {
        if (newQuest.QuestType == Quest.QuestTypes.CaravanProtect)
        {
            newQuest.GoldReward = 21 * newQuest.RecommendedLevel ;
        }
        else if (newQuest.QuestType == Quest.QuestTypes.Delivery)
        {
            newQuest.GoldReward = 16 * newQuest.RecommendedLevel;
        }
        else if (newQuest.QuestType == Quest.QuestTypes.TreasureMapGetMap)
        {
            newQuest.GoldReward = 9 * newQuest.RecommendedLevel;
        }
    }

    private void DetermineRecommendedLevel()
    {
        int randtemp = Random.Range(1, 5);
        
        if (randtemp == 1)
            newQuest.RecommendedLevel = GameInformation.PlayerCharacter.PlayerLevel - 4;
        else if(randtemp == 2)
            newQuest.RecommendedLevel = GameInformation.PlayerCharacter.PlayerLevel - 2;
        else if (randtemp == 3)
            newQuest.RecommendedLevel = GameInformation.PlayerCharacter.PlayerLevel;
        else if (randtemp == 4)
            newQuest.RecommendedLevel = GameInformation.PlayerCharacter.PlayerLevel + 2;
        else if (randtemp == 5)
            newQuest.RecommendedLevel = GameInformation.PlayerCharacter.PlayerLevel + 4;

        if (newQuest.RecommendedLevel < 1)
            newQuest.RecommendedLevel = 1;

    }

    private void DetermineDescription()
    {
        if (newQuest.QuestType == Quest.QuestTypes.CaravanProtect)
        {
            newQuest.QuestDescription = newQuest.QuestAlliance.KingName+ " is offering a fair bit of coin to protect an incoming shipment of goods at "+newQuest.QuestLocation.AreaName+".";
        }
        else if (newQuest.QuestType == Quest.QuestTypes.Delivery)
        {
            newQuest.QuestDescription = newQuest.QuestAlliance.KingName +" is paying to deliver an important message to a friend in " +newQuest.QuestLocation.AreaName+".";
        }
        else if (newQuest.QuestType == Quest.QuestTypes.TreasureMapGetMap)
        {
           newQuest.QuestDescription = "It seems the adventurer stumbled upon a treasure map leading to a secret location in "+newQuest.QuestLocation.AreaName+ ". He agreed give me the secret location if we split the rewards.";
        }
    }

    private void DetermineLocation()
    {
        int randTemp = Random.Range(1, 34);
        Area questLocation = WorldInformation.Areas.Find(x => x.IconNumber == randTemp);
        newQuest.QuestLocation = questLocation;
    }

    private void DetermineName()
    {
        if(newQuest.QuestType == Quest.QuestTypes.CaravanProtect)
        {
            newQuest.QuestName = "Hired Protection!";
            newQuest.ReputationReward = 10;
        }
        if (newQuest.QuestType == Quest.QuestTypes.Delivery)
        {
            newQuest.QuestName = "Impotant Delivery!";
            newQuest.ReputationReward = 5;
        }
        if (newQuest.QuestType == Quest.QuestTypes.TreasureMapGetMap)
        {
            newQuest.QuestName = "Treasure!?";
            newQuest.ReputationReward = 0;
        }
    }

    private void DetermineType()
    {
        int randTemp = Random.Range(1, 3);
        if(randTemp == 1)
        {
            newQuest.QuestType = Quest.QuestTypes.CaravanProtect;
        }
        if (randTemp == 2)
        {
            newQuest.QuestType = Quest.QuestTypes.Delivery;
        }
        if (randTemp == 3)
        {
            newQuest.QuestType = Quest.QuestTypes.TreasureMapGetMap;
        }
    }

}
