using UnityEngine;
using System.Collections;

public class BasePotion : BaseStatItem {
    
    public enum PotionTypes
    {
        HEALTH,
        MANA,
        STRENGTH,
        INTELLECT,
        AGILITY,
        DEFENSE,
        SPEED
    }
   
    public PotionTypes PotionType { get; set; }
}
