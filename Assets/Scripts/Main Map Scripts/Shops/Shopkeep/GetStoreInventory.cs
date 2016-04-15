using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;
using System.Collections.Generic;




//Function: Gets and Displays the store inventory -- allows for buying and selling of all types of items
public class GetStoreInventory : MonoBehaviour {
    public GameObject shopPanel;
    public GameObject ShopContent;
    public GameObject PlayerContent;
    public GameObject storeOwner;
    public GameObject title;
    public GameObject shopBack;
    public GameObject buyButton;
    public GameObject sellButton;
    public GameObject shopKeepPanel;
   
    private int height;
    private int index = 0;
    private int switcher = 0;

    //TEMPORARY!!!!
    

    private BaseWeapon newWeapon;

   //Leaves to shop and returns to the store -- (quests and shop)
    public void ExitShop()
    {
        shopPanel.SetActive(false);
        storeOwner.SetActive(true);
        shopBack.SetActive(false);
        shopKeepPanel.SetActive(true);  
    }

    //Displays all the items in the store inventory
    public void DisplayStoreInventory()
    {
        height = -20;
        index = 0;
        shopKeepPanel.SetActive(false);
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in ShopContent.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        Text text;
        switcher = 0;
        text = title.GetComponent<Text>();
        text.text = "Shop Inventory";
        shopPanel.SetActive(true);
        shopBack.SetActive(true);
        sellButton.SetActive(true);
        buyButton.SetActive(false);
        ResizeView();
        Debug.Log("hi \n How are you?");
        storeOwner.SetActive(false);
        GameObject.Find("PlayerGold").GetComponent<Text>().text = "Gold: " + GameInformation.Gold;

        WorldInformation.shopInv.Weapons.ForEach(ShowWeaponInStore);
        WorldInformation.shopInv.Equipment.ForEach(ShowEquipmentInStore);
        WorldInformation.shopInv.Potions.ForEach(ShowPotionInStore);
        ResizeView();
    }

    //Displays all items in the players inventory
    public void DisplayPlayerInventory()
    {
        height = -20;
        
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in ShopContent.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        GameInformation.PlayerInventory.printInventory();
        sellButton.SetActive(false);
        buyButton.SetActive(true);
        Text text;
        switcher = 1;
       
        index = 0;
        text = title.GetComponent<Text>();
        text.text = "Your Inventory";
        GameObject.Find("PlayerGold").GetComponent<Text>().text = "Gold: " + GameInformation.Gold;
        GameInformation.PlayerInventory.Weapons.ForEach(ShowWeaponInStore);
        GameInformation.PlayerInventory.Equipment.ForEach(ShowEquipmentInStore);
        GameInformation.PlayerInventory.Potions.ForEach(ShowPotionInStore);
        ResizeView();


    }

    private void ResizeView()
    {
        RectTransform ViewRect = (RectTransform)ShopContent.transform;
        ViewRect.sizeDelta = new Vector2(0, height*(-1));
    }

    //Shows a weapon in the store -- places all relevant values in the prefab
    public void ShowWeaponInStore(BaseWeapon weapon)
    {
        GameObject newB = (GameObject)Instantiate(Resources.Load("WeaponButton"));
        newB.transform.SetParent(ShopContent.transform);
        SetListener(newB.GetComponent<Button>());
        index++;
        RectTransform ButtonRect = (RectTransform)newB.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);
        height -= 110;
        GameObject Name = newB.transform.GetChild(0).gameObject;
        Text text;
        text = Name.GetComponent<Text>();
        text.text = weapon.ItemName;

        Name = newB.transform.GetChild(1).gameObject;
        text = Name.GetComponent<Text>();
        if(switcher==0)
            text.text = "Price: " + weapon.Price;
        else
            text.text = "Sell: " + weapon.Price;

        Name = newB.transform.GetChild(2).gameObject;
        text = Name.GetComponent<Text>();
        text.text = "Damage: "+weapon.Damage+ "\nStrength: "+weapon.Strength+"\nIntellect: "+weapon.Intellect+"\nAgility : "+weapon.Agility+" \nDefense: "+weapon.Defense;

    }
    //Shows a equipment in the store -- places all relevant values in the prefab
    public void ShowEquipmentInStore(BaseEquipment equip)
    {
        GameObject newB = (GameObject)Instantiate(Resources.Load("WeaponButton"));
        newB.transform.SetParent(ShopContent.transform);
        SetListener(newB.GetComponent<Button>());
        index++;
        RectTransform ButtonRect = (RectTransform)newB.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);
        height -= 110;
        GameObject Name = newB.transform.GetChild(0).gameObject;
        Text text;
        text = Name.GetComponent<Text>();
        text.text = equip.ItemName;

        Name = newB.transform.GetChild(1).gameObject;
        text = Name.GetComponent<Text>();
        if(switcher ==0)
            text.text = "Price: " + equip.Price;
        else
            text.text = "Sell: " + equip.Price;

        Name = newB.transform.GetChild(2).gameObject;
        text = Name.GetComponent<Text>();
        text.text = "Resistance: " + equip.Resistance + "\nStrength: " + equip.Strength + "\nIntellect: " + equip.Intellect + "\nAgility : " + equip.Agility + " \nDefense: " + equip.Defense;

    }
    //Shows a potion in the store -- places all relevant values in the prefab
    public void ShowPotionInStore(BasePotion potion)
    {
        GameObject newB = (GameObject)Instantiate(Resources.Load("WeaponButton"));
        newB.transform.SetParent(ShopContent.transform);
        SetListener(newB.GetComponent<Button>());
        index++;
        RectTransform ButtonRect = (RectTransform)newB.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);
        height -= 110;
        GameObject Name = newB.transform.GetChild(0).gameObject;
        Text text;
        text = Name.GetComponent<Text>();
        text.text = potion.ItemName;

        Name = newB.transform.GetChild(1).gameObject;
        text = Name.GetComponent<Text>();
        if (switcher == 0)
            text.text = "Price: " + potion.Price;
        else
            text.text = "Sell: "+potion.Price;

        Name = newB.transform.GetChild(2).gameObject;
        text = Name.GetComponent<Text>();
        text.text = "Effectiveness: " + potion.Effectiveness;

    }

    //sets the button listeners to buy the item
    private void SetListener(Button B)
    {
        int i = index;
    
        B.onClick.AddListener(delegate { Buy(i); });
        
    }

    //functions as both a buy and sell depending on the target inventory and other inventory 
    //option = 0 -- buying     || option = 1 -- selling
    public void Buy(int ind)
    {
        Inventory TargetInv = new Inventory();
        Inventory OtherInv = new Inventory();
        //string option = "";
        int price = 0;
        if (switcher == 0)
        {
            TargetInv = WorldInformation.shopInv;
            OtherInv = GameInformation.PlayerInventory;
            //option = "bought";
            price = 1;
        }
        else {
            TargetInv = GameInformation.PlayerInventory;
            OtherInv = WorldInformation.shopInv;
            //option = "sold";
            price = -1;
        }

        //Debug.Log(ind);
        if (ind < TargetInv.Weapons.Count) {
            BaseWeapon BoughtItem = TargetInv.Weapons[ind];
            GameInformation.Gold = GameInformation.Gold - BoughtItem.Price * price;
            OtherInv.Weapons.Add(BoughtItem);
            TargetInv.Weapons.Remove(BoughtItem);
        }
        else if(ind - TargetInv.Weapons.Count < TargetInv.Equipment.Count)
        {
            BaseEquipment BoughtItem = TargetInv.Equipment[ind - TargetInv.Weapons.Count];
            GameInformation.Gold = GameInformation.Gold - BoughtItem.Price * price;
            OtherInv.Equipment.Add(BoughtItem);
            TargetInv.Equipment.Remove(BoughtItem);
        }
        else
        {
            BasePotion BoughtItem = TargetInv.Potions[ind - TargetInv.Weapons.Count - TargetInv.Equipment.Count];
            GameInformation.Gold = GameInformation.Gold - BoughtItem.Price*price;
            OtherInv.Potions.Add(BoughtItem);
            TargetInv.Potions.Remove(BoughtItem);
        }
        SaveInformation.SaveInventoryInformation();
        //shows the updated inventory after buying/selling the item
        if (switcher == 0)
            DisplayStoreInventory();
        else
            DisplayPlayerInventory();
            

    }

}
