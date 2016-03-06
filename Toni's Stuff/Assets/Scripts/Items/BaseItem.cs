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

   // private ItemType itemType;
   public int SpellEffectID { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public int ItemID { get; set; }
    public ItemTypes ItemType { get; set; }

}
