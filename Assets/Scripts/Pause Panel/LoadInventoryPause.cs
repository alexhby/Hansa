using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LoadInventoryPause : MonoBehaviour {

    public GameObject InvContent;
    public GameObject InvDetailPanel;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void showAllps()
    {
        // Destroy all contents first
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in InvContent.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));


        int height = -10;
        foreach(BaseWeapon w in GameInformation.PlayerInventory.Weapons)
        {
            ShowWeapon(w, height);

            height -= 100;
        }
    }
    public void showAllEquipments()
    {
        // Destroy all contents first
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in InvContent.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        int height = -10;
        foreach (BaseEquipment e in GameInformation.PlayerInventory.Equipment)
        {
            ShowEquipment(e, height);

            height -= 100;
        }
    }
    public void showAllPotions()
    {
        // Destroy all contents first
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in InvContent.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        int height = -10;
        foreach (BasePotion p in GameInformation.PlayerInventory.Potions)
        {
            ShowPotion(p, height);

            height -= 100;
        }
    }

    public void ShowWeapon(BaseWeapon weapon, int height)
    {
        GameObject newB = (GameObject)Instantiate(Resources.Load("InventoryButton"));
        newB.transform.SetParent(InvContent.transform);
        SetListenerWeapon(newB.GetComponent<Button>(), weapon);
        RectTransform ButtonRect = (RectTransform)newB.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);
        GameObject Name = newB.transform.GetChild(0).gameObject; // name
        Text text;
        text = Name.GetComponent<Text>();
        text.text = weapon.ItemName + "\n" + weapon.ItemRarity;

        Name = newB.transform.GetChild(1).gameObject; // price
        text = Name.GetComponent<Text>();
        text.text = "Price: $ " + weapon.Price;

        Name = newB.transform.GetChild(2).gameObject; // stats
        text = Name.GetComponent<Text>();
        text.text = "Damage: " + weapon.Damage + "\nStrength: " + weapon.Strength + "\nIntellect: " + weapon.Intellect + "\nAgility : " + weapon.Agility + " \nDefense: " + weapon.Defense;

    }
    public void ShowEquipment(BaseEquipment equip, int height)
    {
        GameObject newB = (GameObject)Instantiate(Resources.Load("InventoryButton"));
        newB.transform.SetParent(InvContent.transform);
        SetListenerEquipment(newB.GetComponent<Button>(), equip);
        RectTransform ButtonRect = (RectTransform)newB.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);
        GameObject Name = newB.transform.GetChild(0).gameObject; // name
        Text text;
        text = Name.GetComponent<Text>();
        text.text = equip.ItemName;

        Name = newB.transform.GetChild(1).gameObject; // price
        text = Name.GetComponent<Text>();
        text.text = "Price: " + equip.Price;
        
        Name = newB.transform.GetChild(2).gameObject; // stats
        text = Name.GetComponent<Text>();
        text.text = "Resistance: " + equip.Resistance + "\nStrength: " + equip.Strength + "\nIntellect: " + equip.Intellect + "\nAgility : " + equip.Agility + " \nDefense: " + equip.Defense;

    }
    public void ShowPotion(BasePotion potion, int height)
    {
        GameObject newB = (GameObject)Instantiate(Resources.Load("InventoryButton"));
        newB.transform.SetParent(InvContent.transform);
        SetListenerPotion(newB.GetComponent<Button>(), potion);
        RectTransform ButtonRect = (RectTransform)newB.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);
        GameObject Name = newB.transform.GetChild(0).gameObject; // name
        Text text;
        text = Name.GetComponent<Text>();
        text.text = potion.ItemName;

        Name = newB.transform.GetChild(1).gameObject; // price
        text = Name.GetComponent<Text>();
        text.text = "Price: $ " + potion.Price;

        Name = newB.transform.GetChild(2).gameObject; // stat
        text = Name.GetComponent<Text>();
        text.text = "Effectiveness: " + potion.Effectiveness;

    }

    private void SetListenerPotion(Button B, BasePotion p)
    {

        B.onClick.AddListener(delegate { showInventoryDetailPotion(p); });

    }

    private void SetListenerEquipment(Button B, BaseEquipment p)
    {

        B.onClick.AddListener(delegate { showInventoryDetailEquipment(p); });

    }

    private void SetListenerWeapon(Button B, BaseWeapon p)
    {

        B.onClick.AddListener(delegate { showInventoryDetailWeapon(p); });

    }

    private void showInventoryDetailPotion(BasePotion p)
    {
        Image i = InvDetailPanel.transform.GetChild(0).gameObject.GetComponent<Image>();
        switch(p.PotionType)
        {
            case BasePotion.PotionTypes.Health:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/RedPotion"), i.sprite.rect, i.sprite.pivot);
                break;
            case BasePotion.PotionTypes.Mana:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/BluePotion"), i.sprite.rect, i.sprite.pivot);
                break;
            case BasePotion.PotionTypes.Speed:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/GreenPotion"), i.sprite.rect, i.sprite.pivot);
                break;
            case BasePotion.PotionTypes.Defense:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/PurplePotion2"), i.sprite.rect, i.sprite.pivot);
                break;
            case BasePotion.PotionTypes.Strength:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/BrownPotion"), i.sprite.rect, i.sprite.pivot);
                break;
            case BasePotion.PotionTypes.Agility:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/YellowPotion"), i.sprite.rect, i.sprite.pivot);
                break;
            case BasePotion.PotionTypes.Intellect:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/PurplePotion"), i.sprite.rect, i.sprite.pivot);
                break;
        }

        InvDetailPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = p.ItemName + "\n" + p.ItemRarity;
        InvDetailPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Price: $ " + p.Price;
        InvDetailPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = p.ItemDescription;
        InvDetailPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Effectiveness: " + p.Effectiveness;
        InvDetailPanel.transform.GetChild(6).gameObject.SetActive(false);
        InvDetailPanel.SetActive(true);
    }

    private void showInventoryDetailEquipment(BaseEquipment p)
    {
        Image i = InvDetailPanel.transform.GetChild(0).gameObject.GetComponent<Image>();
        switch (p.EquipmentType)
        {
            case BaseEquipment.EquipmentTypes.Armor:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/Armor"), i.sprite.rect, i.sprite.pivot);
                break;
            case BaseEquipment.EquipmentTypes.Gauntlets:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/Gauntlets"), i.sprite.rect, i.sprite.pivot);
                break;
            case BaseEquipment.EquipmentTypes.Grieves:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/Grieves"), i.sprite.rect, i.sprite.pivot);
                break;
            case BaseEquipment.EquipmentTypes.Helmet:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/Helmet"), i.sprite.rect, i.sprite.pivot);
                break;
            
        }


        InvDetailPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = p.ItemName + "\n" + p.ItemRarity;
        InvDetailPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Price: $ " + p.Price;
        InvDetailPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = p.ItemDescription;
        InvDetailPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Resistance: " + p.Resistance + "\nStrength: " + p.Strength + "\nIntellect: " + p.Intellect + "\nAgility : " + p.Agility + " \nDefense: " + p.Defense;
        GameObject B = InvDetailPanel.transform.GetChild(6).gameObject;
        B.SetActive(true);
        B.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Equip";
        InvDetailPanel.SetActive(true);
    }

    private void showInventoryDetailWeapon(BaseWeapon p)
    {
        Image i = InvDetailPanel.transform.GetChild(0).gameObject.GetComponent<Image>();

        switch (p.WeaponType)
        {
            case BaseWeapon.WeaponTypes.Sword:
                switch (p.ItemRarity)
                {
                    case BaseStatItem.ItemRaritys.Legendary:
                        i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/Diamond-Sword-Icon"), i.sprite.rect, i.sprite.pivot);
                        break;
                    case BaseStatItem.ItemRaritys.Flawless:
                        i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/Gold-Sword-Icon"), i.sprite.rect, i.sprite.pivot);
                        break;
                    case BaseStatItem.ItemRaritys.Great:
                        i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/Iron-Sword-Icon"), i.sprite.rect, i.sprite.pivot);
                        break;
                    case BaseStatItem.ItemRaritys.Common:
                        i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/Stone-Sword-Icon"), i.sprite.rect, i.sprite.pivot);
                        break;
                    case BaseStatItem.ItemRaritys.Rusty:
                        i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/Wooden-Sword-Icon"), i.sprite.rect, i.sprite.pivot);
                        break;
                }
                break;
            case BaseWeapon.WeaponTypes.Spear:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/Spear"), i.sprite.rect, i.sprite.pivot);
                break;
            case BaseWeapon.WeaponTypes.Bow:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/Bow"), i.sprite.rect, i.sprite.pivot);
                break;
            case BaseWeapon.WeaponTypes.Dagger:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/Dagger"), i.sprite.rect, i.sprite.pivot);
                break;
            case BaseWeapon.WeaponTypes.Tomb:
                i.overrideSprite = Sprite.Create(Resources.Load<Texture2D>("/Images/Book"), i.sprite.rect, i.sprite.pivot);
                break;
        }
        
        InvDetailPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = p.ItemName + "\n" + p.ItemRarity;
        InvDetailPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Price: $ " + p.Price;
        InvDetailPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = p.ItemDescription;
        InvDetailPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Damage: " + p.Damage + "\nStrength: " + p.Strength + "\nIntellect: " + p.Intellect + "\nAgility : " + p.Agility + " \nDefense: " + p.Defense;

        GameObject B = InvDetailPanel.transform.GetChild(6).gameObject;
        B.SetActive(true);
        B.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Equip";
        InvDetailPanel.SetActive(true);
    }

    private void equipWeapon(BaseWeapon w)
    {
        
    }
}
