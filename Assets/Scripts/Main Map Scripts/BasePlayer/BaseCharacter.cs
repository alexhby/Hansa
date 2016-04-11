using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class BaseCharacter {

    public string PlayerName { get; set; }
    public int PlayerLevel { get; set; }
    public BaseCharacterClass.CharacterClasses PlayerClass { get; set; }
    public int Defense { get; set; }    //physical damage resist
    public int Agility { get; set; }    // dodge/crits
    public int Intellect { get; set; }  //magic damage modifier
    public int Strength { get; set; }   //physical damage modifier
    public int CurrentXP { get; set; }  
    public int RequiredXP { get; set; }
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
    public int Mana { get; set; }       //Mana
    

}
