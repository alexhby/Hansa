using UnityEngine;
using System.Collections;

public class CreateNewPotion : MonoBehaviour {

    private BasePotion newPotion;
    private string[] itemNames = new string[5] { "Weak","Common", "Strong", "Greater", "Legendary" };
    private string[] itemDes = new string[2] { "a new cool item", "a random item" };
	// Use this for initialization
	void Start () {
        CreatePotion();
        Debug.Log(newPotion.ItemName);
        Debug.Log(newPotion.ItemDescription);
        Debug.Log(newPotion.PotionType.ToString());
        Debug.Log(newPotion.ItemID.ToString());
       

    }
	
    private void CreatePotion()
    {
        newPotion = new BasePotion();
        
        newPotion.ItemDescription = "This is a potion";
        newPotion.ItemID = Random.Range(1, 20);
        ChoosePotionType();
        newPotion.ItemName = "Potion of "+newPotion.PotionType.ToString();
        
    }

    private void ChoosePotionType()
    {
        int randomTemp = Random.Range(1, 7);
 
        if (randomTemp == 1)
        {
            newPotion.PotionType = BasePotion.PotionTypes.HEALTH;
        }
        if (randomTemp == 2)
        {
            newPotion.PotionType = BasePotion.PotionTypes.MANA;
        }
        else if (randomTemp == 3)
        {
            newPotion.PotionType = BasePotion.PotionTypes.STRENGTH;
        }
        else if (randomTemp == 4)
        {
            newPotion.PotionType = BasePotion.PotionTypes.INTELLECT;
        }
        else if (randomTemp == 5)
        {
            newPotion.PotionType = BasePotion.PotionTypes.AGILITY;
        }
        else if (randomTemp == 6)
        {
            newPotion.PotionType = BasePotion.PotionTypes.DEFENSE;
        }
        else if (randomTemp == 7)
        {
            newPotion.PotionType = BasePotion.PotionTypes.SPEED;
        }
    }
	
}
