using UnityEngine;
using System.Collections;
[System.Serializable]

public class BasePlayer {

    public string PlayerName { get; set; }
    public int PlayerLevel { get; set; }
    public BaseCharacterClass PlayerClass { get; set; }
    public int Defense { get; set; }    //physical damage resist
    public int Agility { get; set; }    // dodge/crits
    public int Intellect { get; set; }  //magic damage modifier
    public int Strength { get; set; }   //physical damage modifier
    public int CurrentXP { get; set; }  
    public int RequiredXP { get; set; }

    public int Health { get; set; }     //Health
    public int Mana { get; set; }       //Mana
    public int Gold { get; set; }       //gold

}
