using UnityEngine;
using System.Collections;
[System.Serializable]

public class BaseMageClass : BaseCharacterClass {

    public BaseMageClass()
    {
        CharacterClassName = "Apprentice";
        CharacterClassDescription = "Apprentices haven't really gotten involved with magic so for now they are just nerds.";
        
        Agility = 2;
        Strength = 1;
        Defense = 3;
        Intellect = 4;
    }
}
