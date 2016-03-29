using UnityEngine;
using System.Collections;

public class CreateNewEquipment : MonoBehaviour {

    private BaseEquipment newEquipment;
    private string[] itemNames = new string[5] { "Rusty","Common", "Great", "Flawless", "Legendary" };
    private string[] itemDes = new string[2] { "a new cool item", "a random item" };
    
    // Use this for initialization
    void Start()
    {
        CreateEquipment();
        Debug.Log(newEquipment.ItemName);
        Debug.Log(newEquipment.ItemDescription);
        Debug.Log(newEquipment.EquipmentType.ToString());
        Debug.Log(newEquipment.ItemID.ToString());
        Debug.Log(newEquipment.Strength.ToString());
    }

    private void CreateEquipment()
    {
        newEquipment = new BaseEquipment();
        newEquipment.ItemName = itemNames[Random.Range(0, 3)] + " Item";
        newEquipment.ItemID = Random.Range(0, 20);
        newEquipment.ItemDescription = itemDes[Random.Range(0, 1)];
        ChooseItemType();

        //stats
        newEquipment.Strength = Random.Range(1, 10);
        newEquipment.Intellect = Random.Range(1, 10);
        newEquipment.Agility = Random.Range(1, 10);
        newEquipment.Defense = Random.Range(1, 10);



    }

    private void ChooseItemType()
    {
        int randomTemp = Random.Range(1, 6);

        if (randomTemp == 1)
        {
            newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.HEAD;
        }
        else if (randomTemp == 2)
        {
            newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.CHEST;
        }
        else if (randomTemp == 3)
        {
            newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.ARMS;
        }
        else if (randomTemp == 4)
        {
            newEquipment.EquipmentType = BaseEquipment.EquipmentTypes.LEGS;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
