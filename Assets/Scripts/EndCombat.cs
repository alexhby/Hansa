using UnityEngine;
using System.Collections;

// For combat scene
public class EndCombat : MonoBehaviour {

	private static LevelUp lu = new LevelUp ();
	public static int experienceGain = 40;
	public static int goldPenalty = 500; // minus this if lost
	public static string MainMapScene = "test";

	public static void Lose(){
		// if lose, minus gold
		GameInformation.Gold -= goldPenalty;

		Application.LoadLevel (MainMapScene);
	}

	public static void Win(){

		// if win, get the rewards
		GameInformation.Gold += WorldInformation.CurrentQuest.GoldReward;

		if (WorldInformation.CurrentQuest.WeaponReward != null) {
			GameInformation.PlayerInventory.Weapons.Add (WorldInformation.CurrentQuest.WeaponReward);
		}
		if (WorldInformation.CurrentQuest.EquipmentReward != null) {
			GameInformation.PlayerInventory.Equipment.Add (WorldInformation.CurrentQuest.EquipmentReward);
		}
		if (WorldInformation.CurrentQuest.PotionReward != null) {
			GameInformation.PlayerInventory.Potions.Add (WorldInformation.CurrentQuest.PotionReward);
		}

		// move the quest to FinishedQuests
		GameInformation.PlayerQuestLog.FinishedQuests.Add (WorldInformation.CurrentQuest);
		GameInformation.PlayerQuestLog.CurrentQuests.Remove (WorldInformation.CurrentQuest);
		WorldInformation.CurrentQuest = null;

		//Gain XP, check for levelup
		if (GameInformation.PlayerCharacter != null) {
			bool isLevelUp;
			GameInformation.PlayerCharacter.CurrentXP += experienceGain;
			isLevelUp = lu.LevelUpCharacter (GameInformation.PlayerCharacter);
			if (isLevelUp) {
				increaseStats (GameInformation.PlayerCharacter);
			}
		}

		if (GameInformation.Char1 != null) {
			bool isLevelUp;
			GameInformation.Char1.CurrentXP += experienceGain;
			isLevelUp = lu.LevelUpCharacter (GameInformation.Char1);
			if (isLevelUp) {
				increaseStats (GameInformation.Char1);
			}
		}
		if (GameInformation.Char2 != null) {
			bool isLevelUp;
			GameInformation.Char2.CurrentXP += experienceGain;
			isLevelUp = lu.LevelUpCharacter (GameInformation.Char2);
			if (isLevelUp) {
				increaseStats (GameInformation.Char2);
			}
		}

		if (GameInformation.Char3 != null) {
			bool isLevelUp;
			GameInformation.Char3.CurrentXP += experienceGain;
			isLevelUp = lu.LevelUpCharacter (GameInformation.Char3);
			if (isLevelUp) {
				increaseStats (GameInformation.Char3);
			}
		}

		if (GameInformation.Char4 != null) {
			bool isLevelUp;
			GameInformation.Char4.CurrentXP += experienceGain;
			isLevelUp = lu.LevelUpCharacter (GameInformation.Char4);
			if (isLevelUp) {
				increaseStats (GameInformation.Char4);
			}
		}

		if (GameInformation.Char5 != null) {
			bool isLevelUp;
			GameInformation.Char5.CurrentXP += experienceGain;
			isLevelUp = lu.LevelUpCharacter (GameInformation.Char5);
			if (isLevelUp) {
				increaseStats (GameInformation.Char5);
			}
		}


	}

	// increase stats when level up
	private static void increaseStats(BaseCharacter character){

		int tier = (int)character.PlayerClass / 4;

		//basic increase
		character.Defense += tier;
		character.Agility += tier;
		character.Intellect += tier;
		character.Strength += tier;

		//extra increase based on class
		if((int)character.PlayerClass % 4 == 0) {
			//squire
			character.Defense += tier;
			character.Strength += tier;
		}
		else if ((int)character.PlayerClass % 4 == 1) {
			//Apprentice
			character.Agility += tier;
			character.Intellect += tier;
		}
		else if ((int)character.PlayerClass % 4 == 2) {
			//Thief
			character.Agility += tier;
			character.Strength += tier;

		}
		else {
			//Archer
			character.Defense += tier;
			character.Intellect += tier;
		}

	}



}
