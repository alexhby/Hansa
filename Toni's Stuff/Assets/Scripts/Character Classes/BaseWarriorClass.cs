using UnityEngine;
using System.Collections;
[System.Serializable]

public class BaseWarriorClass : BaseCharacterClass {

    public BaseWarriorClass()
    {
        CharacterClassName = "Squire";
        CharacterClassDescription = "Basically the interns of real warriors";
        //(agility 2, strength 3, defense 3, intellect 2)
        Agility = 2;
        Strength = 3;
        Defense = 3;
        Intellect = 2;
    }

}
