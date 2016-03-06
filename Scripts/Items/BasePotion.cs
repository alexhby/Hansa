using UnityEngine;
using System.Collections;

public class BasePotion : BaseItem {
    
    public enum PotionTypes
    {
        Health,
        Mana,
        Strength,
        Intellect,
        Agility,
        Defense,
        Speed
    }

    public int Effectiveness { get; set; }
   
    public PotionTypes PotionType { get; set; }
}
