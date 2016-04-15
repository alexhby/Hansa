using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

//Represents a character in the game world (can be friend or foe)
//The strategy pattern is used on these! 
public class BaseCharacter {

    public string PlayerName { get; set; } //Enemies will be set to "Enemy"
    public int PlayerLevel { get; set; }
    public BaseCharacterClass.CharacterClasses PlayerClass { get; set; }
    public int Defense { get; set; }    //physical damage resist
    public int Agility { get; set; }    // dodge/crits
    public int Intellect { get; set; }  //magic damage modifier
    public int Strength { get; set; }   //physical damage modifier
    public int CurrentXP { get; set; }  
    public int RequiredXP { get; set; } //Amount of XP required to increase in level
    public int AvailableStatPoints { get; set; }

    //Equipment
    public BaseEquipment Helmet { get; set; }
    public BaseEquipment Armor { get; set; }
    public BaseEquipment Gauntlets { get; set; }
    public BaseEquipment Grieves { get; set; }
    public BaseWeapon Weapon { get; set; }
    
    //skill list
    public List<Abilities> skills = new List<Abilities>();

    public int Health { get; set; }     //Health
    public int CurrentHealth {get; set; }
    public int Mana { get; set; }       //Mana
    

}
