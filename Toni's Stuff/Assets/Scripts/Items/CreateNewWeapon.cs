using UnityEngine;
using System.Collections;

public class CreateNewWeapon : MonoBehaviour {

    private BaseWeapon newWeapon;

    //can assign names here!
     private string[] itemNames = new string[5] { "Rusty","Common", "Great", "Flawless", "Legendary" };
    private string[] itemDes = new string[2] { "a new cool item", "a random item" };


    void Start()
    {
        CreateWeapon();
        Debug.Log(newWeapon.ItemName);
        Debug.Log(newWeapon.ItemDescription);
        Debug.Log(newWeapon.WeaponType.ToString());
        Debug.Log(newWeapon.ItemID.ToString());
        Debug.Log(newWeapon.Strength.ToString());
    }

    public void CreateWeapon()
    {
        newWeapon = new BaseWeapon();

       

        //create wep description
        newWeapon.ItemDescription = "Description here!";

        //weapon id
        newWeapon.ItemID = Random.Range(1, 20);

        //stats
        newWeapon.Strength = Random.Range(1, 10);
        newWeapon.Intellect = Random.Range(1, 10);
        newWeapon.Agility = Random.Range(1, 10);
        newWeapon.Defense = Random.Range(1, 10);

        //choose type of weapon
        ChooseWeaponType();

         //assign name
        newWeapon.ItemName = itemNames[Random.Range(0,4)] + " " + newWeapon.WeaponType.ToString();

        //SpellEffectID
        newWeapon.SpellEffectID = Random.Range(1, 5);
    }
	
    private void ChooseWeaponType()
    {
        //randomly pick a weapon type
        int randomTemp = Random.Range(1, 5);
        if(randomTemp == 1)
        {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.SWORD;
        }
        else if (randomTemp == 2)
        {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.SPEAR;
        }
        else if (randomTemp == 3)
        {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.TOMB;
        }
        if (randomTemp == 4)
        {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.BOW;
        }
        if (randomTemp == 5)
        {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.DAGGER;
        }
    }
}
