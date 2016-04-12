using UnityEngine;
using System.Collections;
[System.Serializable]

//The character class class
public class BaseCharacterClass {
    //private string characterClassName;
    //private string characterClassDescription;

    public enum CharacterClasses
    {
        Squire,
        Apprentice,
        Thief,
        Archer,
        Mage,
        Hunter,
        Knight,
        Ninja,
        Paladin,
        Sniper,
        ArchMage,
        Assassin
    }
    ////stats
    //private int strength;
    //private int intellect;
    //private int defense;
    //private int agility;


    //Getter functions
    public CharacterClasses CharacterClassName { get; set; }
    public string CharacterClassDescription { get; set; }
    public int Health { get; set; }
    public int Mana { get; set; }

}
