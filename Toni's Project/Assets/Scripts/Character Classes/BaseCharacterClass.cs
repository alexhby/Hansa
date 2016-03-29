using UnityEngine;
using System.Collections;
[System.Serializable]

public class BaseCharacterClass {

    public string CharacterClassName { get; set; }
    public string CharacterClassDescription { get; set; }

    public int Agility { get; set; }
    public int Defense { get; set; }
    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Health { get; set; }
    public int Mana { get; set; }

    public int weapon { get; set; }     //the weapon the class uses
    

    

}
