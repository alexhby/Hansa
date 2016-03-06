using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseItem {

    //private string itemName;
    //private string itemDescription;
    //private int itemID;
    public enum ItemTypes
    {
        EQUIPMENT,
        WEAPON,
        POTION,
        CHEST
    }

    public enum ItemRaritys
    {
        Rusty,
        Common,
        Great,
        Flawless,
        Legendary
    }

   // private ItemType itemType;
   public int Price { get; set; }
   public int SpellEffectID { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public int ItemID { get; set; }
    public ItemTypes ItemType { get; set; }
    public ItemRaritys ItemRarity { get; set; }

}
