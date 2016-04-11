using UnityEngine;
using System.Collections;

public class CreateNewWeapon{

    private BaseWeapon newWeapon;

    //can assign names here!
    private int itemRarity = 1;
    private int itemType = 0;
    private int price = 1;
    private string[] spellEffects = new string[6] { "Water", "Ice", "Wind", "Fire", "Lightning", "Darkness" };
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

    public BaseWeapon returnWeapon()
    {
        CreateWeapon();
        //Debug.Log(newWeapon.ItemName);
        return newWeapon;
    }

    

    //Creates a new weapon object 
    private void CreateWeapon()
    {
        newWeapon = new BaseWeapon();

       

        //create wep description
        newWeapon.ItemDescription = "Description here!";

        //weapon id
        newWeapon.ItemID = Random.Range(1, 20);

        //weapon rarity
        DetermineRarity();


        //stats
        DetermineStats();
        

        //choose type of weapon
        ChooseWeaponType();
        int rand = Random.Range(1, 10);
        //status effect (element)
        if ((rand > 8 && itemType == 2) || (itemType != 2 && rand > 7) )
        {
            newWeapon.SpellEffectID = Random.Range(1, 5);
            price = price + 50;
        }
        else
        {
            newWeapon.SpellEffectID = 0;
        }

        //weapon damage
        DetermineDamage();

        //weapon skill

        //weapon price
        DeterminePrice();

        //assign name
        DetermineName();

       
    }

    private void DeterminePrice()
    {
        price = price + 2 * (newWeapon.Strength + newWeapon.Agility + newWeapon.Defense + newWeapon.Intellect);
        newWeapon.Price = 2 * newWeapon.Damage + price;
    }

    private void DetermineName()
    {
        newWeapon.ItemName = newWeapon.ItemRarity.ToString() + " " + newWeapon.WeaponType.ToString();
        if (newWeapon.SpellEffectID != 0)
        {
            newWeapon.ItemName = newWeapon.ItemName + " of " + spellEffects[newWeapon.SpellEffectID];
        }

        
    }

    private void DetermineStats()
    {
        if (itemRarity < 3)
        {
            newWeapon.Strength = Random.Range(1, 10);
            newWeapon.Intellect = Random.Range(1, 10);
            newWeapon.Agility = Random.Range(1, 10);
            newWeapon.Defense = Random.Range(1, 10);
        }
        else if (itemRarity < 5)
        {
            newWeapon.Strength = Random.Range(1, 20);
            newWeapon.Intellect = Random.Range(1, 20);
            newWeapon.Agility = Random.Range(1, 20);
            newWeapon.Defense = Random.Range(1, 20);
        }
        else
        {
            newWeapon.Strength = Random.Range(1, 30);
            newWeapon.Intellect = Random.Range(1, 30);
            newWeapon.Agility = Random.Range(1, 30);
            newWeapon.Defense = Random.Range(1, 30);
        }
    }

    private void DetermineRarity()
    {
        int temp = Random.Range(1, 10);
        if(GameInformation.PlayerCharacter.PlayerLevel/30 > temp)
        {
            newWeapon.ItemRarity = BaseWeapon.ItemRaritys.Legendary;
            itemRarity = 5;
        }
        else if(GameInformation.PlayerCharacter.PlayerLevel/20 > temp)
        {
            newWeapon.ItemRarity = BaseWeapon.ItemRaritys.Flawless;
            itemRarity = 4;
        }
        else if(GameInformation.PlayerCharacter.PlayerLevel/10 > temp)
        {
            newWeapon.ItemRarity = BaseWeapon.ItemRaritys.Great;
            itemRarity = 3;
        }
        else if(GameInformation.PlayerCharacter.PlayerLevel/4 > temp)
        {
            newWeapon.ItemRarity = BaseWeapon.ItemRaritys.Common;
            itemRarity = 2;
        }
        else
        {
            newWeapon.ItemRarity = BaseWeapon.ItemRaritys.Rusty;
            itemRarity = 1;
        }
    }

    private void DetermineDamage()
    {
        if (itemType == 1 || itemType == 2)
            newWeapon.Damage = itemRarity * Random.Range(10, 15);
        else if (itemType == 3)
            newWeapon.Damage = itemRarity * Random.Range(13, 18);
        else
            newWeapon.Damage = itemRarity * Random.Range(7, 12);
        
   }
	
    private void ChooseWeaponType()
    {
        //randomly pick a weapon type
        int randomTemp = Random.Range(1, 5);
        itemType = randomTemp;
        if(randomTemp == 1)
        {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.Sword;
        }
        else if (randomTemp == 2)
        {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.Spear;
        }
        else if (randomTemp == 3)
        {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.Tomb;
        }
        if (randomTemp == 4)
        {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.Bow;
        }
        if (randomTemp == 5)
        {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.Dagger;
        }
    }
}
