using UnityEngine;
using System.Collections;
[System.Serializable]
public class BaseArcherClass : BaseCharacterClass {

    public BaseArcherClass()
    {
        CharacterClassName = "Archer";
        CharacterClassDescription = "Elegant and refined, archers are just too good for swords";

        Agility = 4;
        Strength = 2;
        Defense = 2;
        Intellect = 2;
    }
}
