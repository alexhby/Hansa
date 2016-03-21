using UnityEngine;
using System.Collections;
[System.Serializable]

public class BasePlayer : BaseCharacter {

    public Inventory PlayerInventory { get; set; }
    public int Gold { get; set; }       //gold
    //QuestLog
    public QuestLog PlayerQuestLog { get; set; }
    
}
