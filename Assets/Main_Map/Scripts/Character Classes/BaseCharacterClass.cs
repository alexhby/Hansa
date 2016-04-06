using UnityEngine;
using System.Collections;
[System.Serializable]

public class BaseCharacterClass {
    //private string characterClassName;
    //private string characterClassDescription;

    public enum CharacterClasses
    {
        Squire,
        Apprentice,
        Thief,
        Archer,

        WhiteMage,
        BlackMage,
        Sniper,
        Knight,
        Ninja,
        Paladin,
        Darksword,
        Berserker,
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
	public Abilities[] ablities;
}
