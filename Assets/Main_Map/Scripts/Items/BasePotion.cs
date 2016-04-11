using UnityEngine;
using System.Collections;
[System.Serializable]

//Base potion type
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

    public enum ItemRaritys
    {
        Weak,
        Common,
        Strong,
        Supreme,
        Legendary
        
    }

    public int Effectiveness { get; set; }
    public ItemRaritys ItemRarity { get; set; }
    public PotionTypes PotionType { get; set; }
}
