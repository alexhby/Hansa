using UnityEngine;
using System.Collections;
[System.Serializable]

public class BaseStatItem : BaseItem {

    public enum ItemRaritys
    {
        Rusty,
        Common,
        Great,
        Flawless,
        Legendary
    }
    public ItemRaritys ItemRarity { get; set; }
    public int Agility { get; set; }
    public int Defense { get; set; }
    public int Strength { get; set; }
    public int Intellect { get; set; }


}
