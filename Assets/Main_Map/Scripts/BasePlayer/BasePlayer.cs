using UnityEngine;
using System.Collections;
[System.Serializable]

//Represents the aspect that only the player has controll over
public class BasePlayer {

    public BaseCharacter PlayerCharacter { get; set; }
    public Inventory PlayerInventory { get; set; }
    public int Gold { get; set; }       //gold
    //QuestLog
    public QuestLog PlayerQuestLog { get; set; }
    
}
