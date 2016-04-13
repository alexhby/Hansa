using UnityEngine;
using System.Collections;

public class CombatSceneManager : MonoBehaviour {

	private LevelUp lu = new LevelUp ();
	int experienceGain = 40;

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}


	// put this in combat scene win/lost trigger
	public void backToMainScene(string sceneName){
		// if lost, minus gold

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

		// add the quest to FinishedQuests
		GameInformation.PlayerQuestLog.FinishedQuests.Add (WorldInformation.CurrentQuest);


		//XP, check for levelup
		if (GameInformation.PlayerCharacter != null) {
			bool isLevelUp;
			GameInformation.PlayerCharacter.CurrentXP += experienceGain;
			isLevelUp = lu.LevelUpCharacter (GameInformation.PlayerCharacter);
		}

		if (GameInformation.Char1 != null) {
			GameInformation.Char1.CurrentXP += experienceGain;
			lu.LevelUpCharacter (GameInformation.Char1);
		}
		if (GameInformation.Char2 != null) {
			GameInformation.Char2.CurrentXP += experienceGain;
			lu.LevelUpCharacter (GameInformation.Char2);
		}

		if (GameInformation.Char3 != null) {
			GameInformation.Char3.CurrentXP += experienceGain;
			lu.LevelUpCharacter (GameInformation.Char3);
		}

		if (GameInformation.Char4 != null) {
			GameInformation.Char4.CurrentXP += experienceGain;
			lu.LevelUpCharacter (GameInformation.Char4);
		}

		if (GameInformation.Char5 != null) {
			GameInformation.Char5.CurrentXP += experienceGain;
			lu.LevelUpCharacter (GameInformation.Char5);
		}


	}



}
