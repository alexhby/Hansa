using UnityEngine;
using System.Collections;
[System.Serializable]

//Represents the Player Model in MVC -- contains only player specific structures like inventory and questlog 
public class BasePlayer {

    public BaseCharacter PlayerCharacter { get; set; }
    public Inventory PlayerInventory { get; set; }
    public int Gold { get; set; }       //gold
    public QuestLog PlayerQuestLog { get; set; }
    
}
