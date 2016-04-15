using UnityEngine;
using System.Collections;
[System.Serializable]

//Represents all of the available classes
public class BaseCharacterClass {
    //private string characterClassName;
    //private string characterClassDescription;

    public enum CharacterClasses
    {
        Squire,
        Apprentice,
        Thief,
        Archer,

		Knight,
        Mage,
		Ninja,
        Hunter,

        Paladin,
		ArchMage,
		Assassin,
        Sniper
        
    }
    


    //Getter functions
    public CharacterClasses CharacterClassName { get; set; }
    public string CharacterClassDescription { get; set; }


}
