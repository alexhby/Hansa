using UnityEngine;
using System.Collections;

public class BaseWeapon : BaseStatItem { //BaseWeapon <- BaseStatItem <- BaseItem

    public enum WeaponTypes
    {
        SWORD,
        SPEAR,
        TOMB,
        BOW,
        DAGGER
    }

    private WeaponTypes weaponType;


    public WeaponTypes WeaponType { get; set; }
  


}
