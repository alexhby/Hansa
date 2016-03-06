using UnityEngine;
using System.Collections;
[System.Serializable]

public class BaseCharacterClass {
    //private string characterClassName;
    //private string characterClassDescription;

    ////stats
    //private int strength;
    //private int intellect;
    //private int defense;
    //private int agility;


    //Getter functions
    public string CharacterClassName { get; set; }
    public string CharacterClassDescription { get; set; }
    public int Agility { get; set; }
    public int Defense { get; set; }
    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Health { get; set; }
    public int Mana { get; set; }

}
