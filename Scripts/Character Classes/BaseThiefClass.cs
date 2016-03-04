using UnityEngine;
using System.Collections;
[System.Serializable]
public class BaseThiefClass : BaseCharacterClass {

    public BaseThiefClass()
    {
        CharacterClassName = "Thief";
        CharacterClassDescription = "Nimble with their hands and sharp with their tongue, don't ever leave your items unattended around these people.";
        Agility = 4;
        Strength = 3;
        Defense = 1;
        Intellect = 2;
    }
}
