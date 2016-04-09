using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;
using System.Collections.Generic;




//THE SHOP KEEP -- displays the available items in the store inventory -- allows players to buy and sell items
public class GetStoreInventory : MonoBehaviour {
    private string tester = "test";
    public static JsonData inv;
    private string url = "http://localhost/361/shop.php";
    private string[] spellEffects = new string[6] { "Water", "Ice", "Wind", "Fire", "Lightning", "Darkness" };
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

    void Start()
    {
        //GameInformation.PlayerInventory = new Inventory();
        //GameInformation.PlayerInventory.Weapons = new List<BaseWeapon>();
        //GameInformation.PlayerInventory.Equipment = new List<BaseEquipment>();
        //GameInformation.PlayerInventory.Potions = new List<BasePotion>();
        //LoadInformation.LoadInventoryInformation();
        
        height = -20;
        WWW www = new WWW(url);
        //StartCoroutine(goDoIt(www));
    }


    IEnumerator goDoIt(WWW www)
    {
        yield return www;
        inv = JsonMapper.ToObject(www.text);
        Debug.Log(inv[0]["NAME"]);
        //tester = www.text;
    }

    public void RenewShop()
    {
        CreateNewWeapon weaponCreator = new CreateNewWeapon();
        
        //weaponCreator.CreateWeapon();
        newWeapon = weaponCreator.returnWeapon();
        Debug.Log(newWeapon.ItemName);
        Debug.Log("Weapon damage: " + newWeapon.Damage);
        Debug.Log(newWeapon.Strength.ToString());

        Debug.Log("HEY THERE JUST GONNA GET BETWEEN THIS SHIIIIT");
        newWeapon = weaponCreator.returnWeapon();
        Debug.Log(newWeapon.ItemName);
        Debug.Log("Weapon damage: " + newWeapon.Damage);
        Debug.Log(newWeapon.Strength.ToString());

    }

    public void ExitShop()
    {
        //Leaves the shop and returns to the main store screen
        shopPanel.SetActive(false);
        storeOwner.SetActive(true);
        shopBack.SetActive(false);
        shopKeepPanel.SetActive(true);
       
    }

    public void DisplayStoreInventory()
    {
        //Displays the store inventory panel
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

    public void DisplayPlayerInventory()
    {
        //Displays the player sell inventory
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
        //Resizes the window based on how many items the player/shop has
        RectTransform ViewRect = (RectTransform)ShopContent.transform;
        ViewRect.sizeDelta = new Vector2(0, height*(-1));
    }

    public void ShowWeaponInStore(BaseWeapon weapon)
    {
        //Shows all the weapons in the store! GETS/DISPLAYS WEAPON DATA
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
    public void ShowEquipmentInStore(BaseEquipment equip)
    {
        //Shows all the equipment in the store! GETS/DISPLAYS EQUIPMENT DATA
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
    public void ShowPotionInStore(BasePotion potion)
    {
        //Shows all the potions in the store! GETS/DISPLAYS POTION DATA
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

    private void SetListener(Button B)
    {
        //Adds a listener onto a button 
        int i = index;
        B.onClick.AddListener(delegate { Buy(i); });
        
    }

    public void Buy(int ind)
    {
        //This function acts as both a buy and sell placing weapons from one set of items (store/player inventory) to the other
        Inventory TargetInv = new Inventory();
        Inventory OtherInv = new Inventory();
        string option = "";
        int price = 0;
        if (switcher == 0)
        {
            TargetInv = WorldInformation.shopInv;
            OtherInv = GameInformation.PlayerInventory;
            option = "bought";
            price = 1;
        }
        else {
            TargetInv = GameInformation.PlayerInventory;
            OtherInv = WorldInformation.shopInv;
            option = "sold";
            price = -1;
        }

        Debug.Log(ind);
        if (ind < TargetInv.Weapons.Count) {
            BaseWeapon BoughtItem = TargetInv.Weapons[ind];

            Debug.Log("You just "+option+": " + BoughtItem.ItemName);
            GameInformation.Gold = GameInformation.Gold - BoughtItem.Price * price;
            //GameObject.Find("PlayerGold").GetComponent<Text>().text = "Gold: " + GameInformation.Gold;
            //GameInformation.PlayerInventory.printInventory();
            OtherInv.Weapons.Add(BoughtItem);
            TargetInv.Weapons.Remove(BoughtItem);
        }
        else if(ind - TargetInv.Weapons.Count < TargetInv.Equipment.Count)
        {
            BaseEquipment BoughtItem = TargetInv.Equipment[ind - TargetInv.Weapons.Count];
            Debug.Log("You just " + option + ": " + BoughtItem.ItemName);
            GameInformation.Gold = GameInformation.Gold - BoughtItem.Price * price;
            //GameObject.Find("PlayerGold").GetComponent<Text>().text = "Gold: " + GameInformation.Gold;
            OtherInv.Equipment.Add(BoughtItem);

            TargetInv.Equipment.Remove(BoughtItem);
        }
        else
        {
            BasePotion BoughtItem = TargetInv.Potions[ind - TargetInv.Weapons.Count - TargetInv.Equipment.Count];
            Debug.Log("You just " + option + ": " + BoughtItem.ItemName);
            GameInformation.Gold = GameInformation.Gold - BoughtItem.Price*price;
            //GameObject.Find("PlayerGold").GetComponent<Text>().text = "Gold: " + GameInformation.Gold;
            OtherInv.Potions.Add(BoughtItem);
            TargetInv.Potions.Remove(BoughtItem);
        }

        
        

        Debug.Log("YOUR INVENTORY");
        GameInformation.PlayerInventory.printInventory();
        Debug.Log("SHOP INVENTORY");
        WorldInformation.shopInv.printInventory();
        if (switcher == 0)
            DisplayStoreInventory();
        else
            DisplayPlayerInventory();
            

    }

}
