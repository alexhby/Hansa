using UnityEngine;
using System.Collections;

// For combat scene
public class EndCombat : MonoBehaviour {

	private static LevelUp lu = new LevelUp ();
	public static int experienceGain = 40;
	public static int goldPenalty = 500; // minus this if lost

	public static void Lose(){
		//TODO GUI:
		//You Lose! 
		//Lose 500 gold.

		// if lose, minus gold
		GameInformation.Gold -= goldPenalty;
		if (GameInformation.Gold < 0) {
			GameInformation.Gold = 0;
		}


	}

	public static void Win(){

		//TODO GUI:
		//You Win!
		//You have finished the current quest.name and gained all the rewards.
		//Every character gains 40 XP.

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
		promote (GameInformation.PlayerCharacter);
		promote (GameInformation.Char1);
		promote (GameInformation.Char2);
		promote (GameInformation.Char3);
		promote (GameInformation.Char4);
		promote (GameInformation.Char5);

	}


	//Helper func: Gain XP, check for levelup
	private static void promote(BaseCharacter character){
		if (character != null) {
			bool isLevelUp;
			character.CurrentXP += experienceGain;
			isLevelUp = lu.LevelUpCharacter (character);
			if (isLevelUp) {

				// save old stats for GUI display
				int oldD = character.Defense;
				int oldA = character.Agility;
				int oldI = character.Intellect;
				int oldS = character.Strength;

				// increase stats when level up
				int tier = (int)character.PlayerClass / 4 + 1;

				//basic increase
				character.Defense += tier;
				character.Agility += tier;
				character.Intellect += tier;
				character.Strength += tier;
				character.Health += tier * 20;
				if (character.Health > 100) {
					character.Health = 100;
				}

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


				//TODO GUI:
				//"Level up!"
				//character.PlayerName
				//"Defense: " + oldD + "-->" + character.Defense
				//"Agility: " + oldA + "-->" + character.Agility
				//"Intellect: " + oldI + "-->" + character.Intellect
				//"Strength: " + oldS + "-->" + character.Strength
			}
		}
	}



}
