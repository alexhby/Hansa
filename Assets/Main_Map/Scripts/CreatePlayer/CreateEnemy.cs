﻿using UnityEngine;
using System.Collections;

public class CreateEnemy : MonoBehaviour {
    private static CreateNewCharacter charCreate = new CreateNewCharacter();
    private static CreateNewEquipment equipCreate = new CreateNewEquipment();
    private static CreateNewWeapon wepCreate = new CreateNewWeapon();
    private static BaseCharacter newEnemy;

    public static BaseCharacter returnEnemy(int level)
    {
        newEnemy = charCreate.ReturnNewEnemy(level);
        if (newEnemy.PlayerClass == BaseCharacterClass.CharacterClasses.Apprentice || newEnemy.PlayerClass == BaseCharacterClass.CharacterClasses.Mage || newEnemy.PlayerClass == BaseCharacterClass.CharacterClasses.ArchMage)
            newEnemy.Weapon = wepCreate.ReturnLeveledWeapon(level, 3);
        else if (newEnemy.PlayerClass == BaseCharacterClass.CharacterClasses.Squire || newEnemy.PlayerClass == BaseCharacterClass.CharacterClasses.Knight || newEnemy.PlayerClass == BaseCharacterClass.CharacterClasses.Paladin)
            newEnemy.Weapon = wepCreate.ReturnLeveledWeapon(level, 2);
        else if (newEnemy.PlayerClass == BaseCharacterClass.CharacterClasses.Thief || newEnemy.PlayerClass == BaseCharacterClass.CharacterClasses.Ninja || newEnemy.PlayerClass == BaseCharacterClass.CharacterClasses.Assassin)
            newEnemy.Weapon = wepCreate.ReturnLeveledWeapon(level, 5);
        else if (newEnemy.PlayerClass == BaseCharacterClass.CharacterClasses.Archer || newEnemy.PlayerClass == BaseCharacterClass.CharacterClasses.Hunter || newEnemy.PlayerClass == BaseCharacterClass.CharacterClasses.Sniper)
            newEnemy.Weapon = wepCreate.ReturnLeveledWeapon(level, 4);

        newEnemy.Helmet = equipCreate.returnLeveledEquipment(level, 2);
        newEnemy.Armor = equipCreate.returnLeveledEquipment(level, 1);
        newEnemy.Gauntlets = equipCreate.returnLeveledEquipment(level, 3);
        newEnemy.Grieves = equipCreate.returnLeveledEquipment(level, 4);

        return newEnemy;

    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}