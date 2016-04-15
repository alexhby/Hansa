using UnityEngine;
using System.Collections;

[System.Serializable]
//Base type for all items
public class BaseItem {

    
    public enum ItemTypes
    {
        EQUIPMENT,
        WEAPON,
        POTION
    }

    

   
   public int Price { get; set; }           //The gold value of the item
   public int SpellEffectID { get; set; }   //Elemental effect of the item
    public string ItemName { get; set; }    
    public string ItemDescription { get; set; }
    public int ItemID { get; set; }
    public ItemTypes ItemType { get; set; }
    

}
