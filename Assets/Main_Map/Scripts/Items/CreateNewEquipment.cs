using UnityEngine;
using System.Collections;

//CREATES NEW EQUIPMENT -- INITIALIZES ALL PROPERTIES -- Randomizes based on level
public class CreateNewEquipment  {

    private BaseEquipment newEquipment;
    //private string[] itemNames = new string[5] { "Rusty","Common", "Great", "Flawless", "Legendary" };
    private string[] itemDes = new string[2] { "a new cool item", "a random item" };
    private int itemRarity = 1;
    private int itemType = 0;
    private int price = 1;
    private string[] spellEffects = new string[6] { "Water", "Ice", "Wind", "Fire", "Lightning", "Darkness" };


    // Use this for initialization
    void Start()
    {
        //CreateEquipment();
        //Debug.Log(newEquipment.ItemName);
        
        //Debug.Log(newEquipment.EquipmentType.ToString());
        
        //Debug.Log(newEquipment.Strength.ToString());
    }

    public BaseEquipment returnEquipment()
    {
        CreateEquipment(GameInformation.PlayerCharacter.PlayerLevel,0);
        //Debug.Log(newWeapon.ItemName);
        return newEquipment;
    }

    public BaseEquipment returnLeveledEquipment(int level,int piece)
    {
        //piece -- 1 is armor, 2 is helmets, 3 is gauntlets, 4 is grieves
        CreateEquipment(level,piece);
        return newEquipment;
    }

    private void CreateEquipment(int level,int piece)
    {

        newEquipment = new BaseEquipment();

        //equipment rarity
        DetermineRarity(level);

        //stats
        DetermineStats();

        int rand = Random.Range(1, 10);
        //status effect (element)
        if (rand > 6)
        {
            newEquipment.SpellEffectID = Random.Range(1, 5);
            price = price + 30;
        }
        else
        {
            newEquipment.SpellEffectID = 0;
        }

        //Type
        ChooseItemType(piece);

        //Equipment Resistance
        DetermineResistance();

        DeterminePrice();

        //assign name
        DetermineName();





    }

    private void DeterminePrice()
    {
        price = price + 2*(newEquipment.Strength + newEquipment.Agility + newEquipment.Defense + newEquipment.Intellect);
        newEquipment.Price = 2 * newEquipment.Resistance + price;
    }

    private void DetermineName()
    {
        newEquipment.ItemName = newEquipment.ItemRarity.ToString() + " " + newEquipment.EquipmentType.ToString();
        if (newEquipment.SpellEffectID != 0)
        {
            newEquipment.ItemName = newEquipment.ItemName + " of " + spellEffects[newEquipment.SpellEffectID];
        }


    }

    private void DetermineResistance()
    {
        if (itemType == 1)
        {
            newEquipment.Resistance = itemRarity * Random.Range(20, 25);
        }
        else if (itemType == 2)
        {
            newEquipment.Resistance = itemRarity * Random.Range(11, 16);

        }
        else {
            newEquipment.Resistance = itemRarity * Random.Range(7, 12);
        }

    }

    private void DetermineStats()
    {
        if (itemRarity < 3)
        {
            newEquipment.Strength = Random.Range(1, 10);
            newEquipment.Intellect = Random.Range(1, 10);
            newEquipment.Agility = Random.Range(1, 10);
            newEquipment.Defense = Random.Range(1, 10);
        }
        else if (itemRarity < 5)
        {
            newEquipment.Strength = Random.Range(1, 20);
            newEquipment.Intellect = Random.Range(1, 20);
            newEquipment.Agility = Random.Range(1, 20);
            newEquipment.Defense = Random.Range(1, 20);
        }
        else
        {
            newEquipment.Strength = Random.Range(1, 30);
            newEquipment.Intellect = Random.Range(1, 30);
            newEquipment.Agility = Random.Range(1, 30);
            newEquipment.Defense = Random.Range(1, 30);
        }
    }

    private void DetermineRarity(int level)
    {
        int temp = Random.Range(1, 10);
        if (level / 30 > temp)
        {
            newEquipment.ItemRarity = BaseStatItem.ItemRaritys.Legendary;
            itemRarity = 5;
        }
        else if (level / 20 > temp)
        {
            newEquipment.ItemRarity = BaseStatItem.ItemRaritys.Flawless;
            itemRarity = 4;
        }
        else if (level / 10 > temp)
        {
            newEquipment.ItemRarity = BaseStatItem.ItemRaritys.Great;
            itemRarity = 3;
        }
        else if (level / 4 > temp)
        {
            newEquipment.ItemRarity = BaseStatItem.ItemRaritys.Common;
            itemRarity = 2;
        }
        else
        {
            newEquipment.ItemRarity = BaseStatItem.ItemRaritys.Rusty;
            itemRarity = 1;
        }
    }

    private void ChooseItemType(int piece)
    {
        int randomTemp;
        if (piece == 0) {
            randomTemp = Random.Range(1, 4);
        }
        else
        {
            randomTemp = piece;
        }

        itemType = randomTemp;

        if (randomTemp == 2)
        {
            newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.Helmet;
        }
        else if (randomTemp == 1)
        {
            newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.Armor;
        }
        else if (randomTemp == 3)
        {
            newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.Gauntlets;
        }
        else if (randomTemp == 4)
        {
            newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.Grieves;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
