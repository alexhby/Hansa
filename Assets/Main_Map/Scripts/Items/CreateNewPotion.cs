using UnityEngine;
using System.Collections;

//CREATES NEW POTION -- INITIALIZES ALL PROPERTIES -- Randomizes based on level
public class CreateNewPotion  {

    private BasePotion newPotion;
   // private string[] itemNames = new string[5] { "Weak","Common", "Strong", "Greater", "Legendary" };
    private string[] itemDes = new string[2] { "a new cool item", "a random item" };
    private int itemRarity = 1;
    private int itemType = 0;

    // Use this for initialization
    void Start () {
       
      
       

    }

    public BasePotion returnPotion()
    {
        CreatePotion();
        //Debug.Log(newWeapon.ItemName);
        return newPotion;
    }


    private void CreatePotion()
    {
        newPotion = new BasePotion();
        
        newPotion.ItemDescription = "This is a potion";
        newPotion.ItemID = Random.Range(1, 20);

        // rarity
        DetermineRarity();

        ChoosePotionType();

        //Potion Effectiveness
        DetermineEffectiveness();


        newPotion.ItemName = newPotion.ItemRarity.ToString() +" Potion of "+newPotion.PotionType.ToString();
        
    }



    private void DetermineEffectiveness()
    {
        if (itemType < 3)
        {
            newPotion.Effectiveness = itemRarity * Random.Range(20, 25);
            newPotion.Price = newPotion.Effectiveness * 2;
        }
        else if (itemType < 7)
        {
            newPotion.Effectiveness = itemRarity * Random.Range(11, 16);
            newPotion.Price = newPotion.Effectiveness * 4;
        }
        else {
            newPotion.Effectiveness = itemRarity * Random.Range(3, 8);
            newPotion.Price = newPotion.Effectiveness * 10;
        }
    }
    private void DetermineRarity()
    {
        int temp = Random.Range(1, 10);
        if (GameInformation.PlayerCharacter.PlayerLevel / 30 > temp)
        {
            newPotion.ItemRarity = BasePotion.ItemRaritys.Legendary;
            itemRarity = 5;
        }
        else if (GameInformation.PlayerCharacter.PlayerLevel / 20 > temp)
        {
            newPotion.ItemRarity = BasePotion.ItemRaritys.Supreme;
            itemRarity = 4;
        }
        else if (GameInformation.PlayerCharacter.PlayerLevel / 10 > temp)
        {
            newPotion.ItemRarity = BasePotion.ItemRaritys.Strong;
            itemRarity = 3;
        }
        else if (GameInformation.PlayerCharacter.PlayerLevel / 4 > temp)
        {
            newPotion.ItemRarity = BasePotion.ItemRaritys.Common;
            itemRarity = 2;
        }
        else
        {
            newPotion.ItemRarity = BasePotion.ItemRaritys.Weak;
            itemRarity = 1;
        }
    }

    private void ChoosePotionType()
    {
        int randomTemp = Random.Range(1, 7);
        itemType = randomTemp;
 
        if (randomTemp == 1)
        {
            newPotion.PotionType = BasePotion.PotionTypes.Health;
        }
        if (randomTemp == 2)
        {
            newPotion.PotionType = BasePotion.PotionTypes.Mana;
        }
        else if (randomTemp == 3)
        {
            newPotion.PotionType = BasePotion.PotionTypes.Strength;
        }
        else if (randomTemp == 4)
        {
            newPotion.PotionType = BasePotion.PotionTypes.Intellect;
        }
        else if (randomTemp == 5)
        {
            newPotion.PotionType = BasePotion.PotionTypes.Agility;
        }
        else if (randomTemp == 6)
        {
            newPotion.PotionType = BasePotion.PotionTypes.Defense;
        }
        else if (randomTemp == 7)
        {
            newPotion.PotionType = BasePotion.PotionTypes.Speed;
        }
    }
	
}
