using UnityEngine;
using System.Collections;
[System.Serializable]

//Base item for equippable weapons and equipment
public class BaseStatItem : BaseItem {

    public enum ItemRaritys
    {
        Rusty,
        Common,
        Great,
        Flawless,
        Legendary
    }

    //These items will add to your stats
    public ItemRaritys ItemRarity { get; set; } //equippable item rarities
    public int Agility { get; set; }
    public int Defense { get; set; }
    public int Strength { get; set; }
    public int Intellect { get; set; }


}
