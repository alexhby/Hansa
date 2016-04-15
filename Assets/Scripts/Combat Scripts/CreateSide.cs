using UnityEngine;
using System.Collections;

public class CreateSide : MonoBehaviour {

	private static CreateNewCharacter charCreate = new CreateNewCharacter();
	private static CreateNewEquipment equipCreate = new CreateNewEquipment();
	private static CreateNewWeapon wepCreate = new CreateNewWeapon();
	private static BaseCharacter newSide;


	// create a new side character for combat demo
	// reuse the code from CreateEnemy
	public static BaseCharacter returnSide(int level, BaseCharacterClass.CharacterClasses pClass)
	{
		newSide = charCreate.ReturnNewEnemy(level);
		newSide.PlayerClass = pClass;
		if (newSide.PlayerClass == BaseCharacterClass.CharacterClasses.Apprentice || newSide.PlayerClass == BaseCharacterClass.CharacterClasses.Mage || newSide.PlayerClass == BaseCharacterClass.CharacterClasses.ArchMage)
			newSide.Weapon = wepCreate.ReturnLeveledWeapon(level, 3);
		else if (newSide.PlayerClass == BaseCharacterClass.CharacterClasses.Squire || newSide.PlayerClass == BaseCharacterClass.CharacterClasses.Knight || newSide.PlayerClass == BaseCharacterClass.CharacterClasses.Paladin)
			newSide.Weapon = wepCreate.ReturnLeveledWeapon(level, 2);
		else if (newSide.PlayerClass == BaseCharacterClass.CharacterClasses.Thief || newSide.PlayerClass == BaseCharacterClass.CharacterClasses.Ninja || newSide.PlayerClass == BaseCharacterClass.CharacterClasses.Assassin)
			newSide.Weapon = wepCreate.ReturnLeveledWeapon(level, 5);
		else if (newSide.PlayerClass == BaseCharacterClass.CharacterClasses.Archer || newSide.PlayerClass == BaseCharacterClass.CharacterClasses.Hunter || newSide.PlayerClass == BaseCharacterClass.CharacterClasses.Sniper)
			newSide.Weapon = wepCreate.ReturnLeveledWeapon(level, 4);

		newSide.Helmet = equipCreate.returnLeveledEquipment(level, 2);
		newSide.Armor = equipCreate.returnLeveledEquipment(level, 1);
		newSide.Gauntlets = equipCreate.returnLeveledEquipment(level, 3);
		newSide.Grieves = equipCreate.returnLeveledEquipment(level, 4);

		return newSide;

	}
}
