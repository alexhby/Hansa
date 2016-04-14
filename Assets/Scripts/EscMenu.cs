using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
using System;
/*
Class that controls tab buttons in pause menu
*/
public class EscMenu : MonoBehaviour
{

    public GameObject pauseMenuPanel;                   // reference to pause menu panel
    public GameObject areaname;
    public GameObject mainScroll;

    private bool isPaused;                              //Boolean to check if the game is paused or not

    private BlurOptimized blur;                         // reference to bluroptimized

    // reference to menu panels
    public GameObject charactersPanel;                  
    public GameObject inventoryPanel;
    public GameObject questPanel;
    public GameObject gamePanel;
    public GameObject charDetailPanel;
    public GameObject questDetailPanel;
    public GameObject inventoryDetailPanel;

    public GameObject goldUsername;
    public GameObject inventoryContent;
    public GameObject questContent;

    // character info buttons
    public GameObject characterInfoButton1;
    public GameObject characterInfoButton2;
    public GameObject characterInfoButton3;
    public GameObject characterInfoButton4;
    public GameObject characterInfoButton5;
    public GameObject characterInfoButton6;

    public BaseCharacter test = new BaseCharacter();
    // reference to main camera
    public GameObject mainCamera;

    //Awake is called before Start()
    void Awake()
    {
        //Get a component reference to bluroptimized attached to this object
        blur = mainCamera.GetComponent<BlurOptimized>();
    }

    void Start()
    {
        CreateNewCharacter.InitNewCharacter(test);
        GameInformation.PlayerCharacter = test;
        isPaused = false;
        
    }
    // Press Esc to pause/unpause
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                UnPause();
            } else {
                DoPause();
            }
                
        }
    }

    // function to control tab buttons' behaivour - pass parameter within OnClick() within each button
    // 1 = characters, 2 = inventory, 3 = quest, 4 = game
    public void tabButtonPressed(int panelToGo)
    {
        // first hide all panels
        charactersPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        questPanel.SetActive(false);
        gamePanel.SetActive(false);
        charDetailPanel.SetActive(false);
        questDetailPanel.SetActive(false);
        inventoryDetailPanel.SetActive(false);

        switch (panelToGo) // show panel according to panelToGo parameter
        {
            case 1:
                charactersPanel.SetActive(true);
                reloadCharInfo();
                break;
            case 2: inventoryPanel.SetActive(true); break;
            case 3: questPanel.SetActive(true); break;
            case 4: gamePanel.SetActive(true); break;
        }
        

    }

    public void reloadCharInfo()
    {
        // reload all character information

        BaseCharacter[] chars = new BaseCharacter[6];
        chars[0] = GameInformation.PlayerCharacter;
        chars[1] = GameInformation.Char1;
        chars[2] = GameInformation.Char2;
        chars[3] = GameInformation.Char3;
        chars[4] = GameInformation.Char4;
        chars[5] = GameInformation.Char5;

        GameObject[] charInfoButtons = new GameObject[6];
        charInfoButtons[0] = characterInfoButton1;
        charInfoButtons[1] = characterInfoButton2;
        charInfoButtons[2] = characterInfoButton3;
        charInfoButtons[3] = characterInfoButton4;
        charInfoButtons[4] = characterInfoButton5;
        charInfoButtons[5] = characterInfoButton6;

        for (int i = 0; i < 6; i++)
        {
            if (chars[i] != null) // if a character exists...
            {
                charInfoButtons[i].GetComponent<Button>().interactable = true; // enable button
                BaseCharacter bc = chars[i];
                charInfoButtons[i].GetComponent<Button>().onClick.AddListener(delegate { showCharDetailPanel(bc); });
                charInfoButtons[i].transform.GetChild(1).gameObject.SetActive(true);
                charInfoButtons[i].transform.GetChild(2).gameObject.SetActive(true);
                charInfoButtons[i].transform.GetChild(3).gameObject.SetActive(true);
                charInfoButtons[i].transform.GetChild(4).gameObject.SetActive(true);


                // Level of player character
                Text charlvl = charInfoButtons[i].transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>();
                charlvl.text = "Lv. " + chars[i].PlayerLevel.ToString();
                // Class
                Text charclass = charInfoButtons[i].transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>();
                charclass.text = chars[i].PlayerClass.ToString();
                // CharName
                Text charname = charInfoButtons[i].transform.GetChild(1).GetChild(2).gameObject.GetComponent<Text>();
                charname.text = chars[i].PlayerName;

                // StatTable
                // Strength
                Text charstr = charInfoButtons[i].transform.GetChild(1).GetChild(3).GetChild(4).gameObject.GetComponent<Text>();
                charstr.text = chars[i].Strength.ToString();
                // Defense
                Text chardef = charInfoButtons[i].transform.GetChild(1).GetChild(3).GetChild(5).gameObject.GetComponent<Text>();
                chardef.text = chars[i].Defense.ToString();
                // Agility
                Text charag = charInfoButtons[i].transform.GetChild(1).GetChild(3).GetChild(6).gameObject.GetComponent<Text>();
                charag.text = chars[i].Agility.ToString();
                // Intellect
                Text charint = charInfoButtons[i].transform.GetChild(1).GetChild(3).GetChild(7).gameObject.GetComponent<Text>();
                charint.text = chars[i].Intellect.ToString();
                // Health - MAX HEALTH = 100
                RectTransform charhp = charInfoButtons[i].transform.GetChild(2).GetChild(0).gameObject.GetComponent<RectTransform>();
                charInfoButtons[i].transform.GetChild(2).GetChild(1).gameObject.GetComponent<Text>().text = chars[i].CurrentHealth + "/" + chars[i].Health;
                charhp.sizeDelta = new Vector2(chars[i].CurrentHealth * 290/chars[i].Health, 15);

                // EXP
                RectTransform charxp = charInfoButtons[i].transform.GetChild(3).GetChild(0).gameObject.GetComponent<RectTransform>();
                charInfoButtons[i].transform.GetChild(3).GetChild(1).gameObject.GetComponent<Text>().text = chars[i].CurrentXP + "/" + chars[i].RequiredXP;
                charxp.sizeDelta = new Vector2(chars[i].CurrentXP * 290/ chars[i].RequiredXP, 15);

                // Mana
                charInfoButtons[i].transform.GetChild(4).GetChild(1).gameObject.GetComponent<Text>().text = chars[i].Mana + "";
            }
            else
            {
                charInfoButtons[i].GetComponent<Button>().interactable = false; // disable button
                charInfoButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                charInfoButtons[i].transform.GetChild(2).gameObject.SetActive(false);
                charInfoButtons[i].transform.GetChild(3).gameObject.SetActive(false);
                charInfoButtons[i].transform.GetChild(4).gameObject.SetActive(false);
            }
        }
    }

    public void reloadGoldUsername()
    {
        Text goldtext = goldUsername.GetComponent<Text>();
        goldtext.text = "$ " + GameInformation.Gold + "\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
    }
    public void reloadInventory()
    {
        GameObject ItemB = (GameObject)Instantiate(Resources.Load("InventoryButton"));
        ItemB.transform.SetParent(inventoryContent.transform);
    }

    
    private void showCharDetailPanel(BaseCharacter c)
    {
        charDetailPanel.SetActive(true);
        // Level of player character
        Text charlvl = charDetailPanel.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>();
        charlvl.text = "Lv. " + c.PlayerLevel.ToString();
        // Class
        Text charclass = charDetailPanel.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>();
        charclass.text = c.PlayerClass.ToString();
        // CharName
        Text charname = charDetailPanel.transform.GetChild(1).GetChild(2).gameObject.GetComponent<Text>();
        charname.text = c.PlayerName;

        // StatTable
        // Strength
        Text charstr = charDetailPanel.transform.GetChild(1).GetChild(3).GetChild(4).gameObject.GetComponent<Text>();
        charstr.text = c.Strength.ToString();
        // Defense
        Text chardef = charDetailPanel.transform.GetChild(1).GetChild(3).GetChild(5).gameObject.GetComponent<Text>();
        chardef.text = c.Defense.ToString();
        // Agility
        Text charag = charDetailPanel.transform.GetChild(1).GetChild(3).GetChild(6).gameObject.GetComponent<Text>();
        charag.text = c.Agility.ToString();
        // Intellect
        Text charint = charDetailPanel.transform.GetChild(1).GetChild(3).GetChild(7).gameObject.GetComponent<Text>();
        charint.text = c.Intellect.ToString();
        // Health - MAX HEALTH = 100
        RectTransform charhp = charDetailPanel.transform.GetChild(2).GetChild(1).gameObject.GetComponent<RectTransform>();
        charDetailPanel.transform.GetChild(2).GetChild(2).gameObject.GetComponent<Text>().text = c.CurrentHealth + "/" + c.Health;
        charhp.sizeDelta = new Vector2(c.CurrentHealth * 390 / c.Health, 15);

        // EXP
        RectTransform charxp = charDetailPanel.transform.GetChild(3).GetChild(1).gameObject.GetComponent<RectTransform>();
        charDetailPanel.transform.GetChild(3).GetChild(2).gameObject.GetComponent<Text>().text = c.CurrentXP + "/" + c.RequiredXP;
        charxp.sizeDelta = new Vector2(c.CurrentXP * 390 / c.RequiredXP, 15);

        // Mana
        charDetailPanel.transform.GetChild(4).GetChild(2).gameObject.GetComponent<Text>().text = c.Mana + "";

        // weapon button
        Transform B = charDetailPanel.transform.GetChild(5);
        if (c.Weapon != null) // if weapon is equipped
        {
            B.gameObject.GetComponent<Button>().interactable = true;
            B.GetChild(0).gameObject.SetActive(true);
            B.gameObject.GetComponent<Button>().onClick.AddListener(delegate { showInventoryDetailWeapon(c.Weapon, c); });
            Image i = B.GetChild(0).gameObject.GetComponent<Image>();
            switch (c.Weapon.WeaponType)
            {
                case BaseWeapon.WeaponTypes.Sword:
                    switch (c.Weapon.ItemRarity)
                    {
                        case BaseStatItem.ItemRaritys.Legendary:
                            i.sprite = Resources.Load<Sprite>("/Images/Diamond-Sword-Icon");
                            break;
                        case BaseStatItem.ItemRaritys.Flawless:
                            i.sprite = Resources.Load<Sprite>("/Images/Gold-Sword-Icon");
                            break;
                        case BaseStatItem.ItemRaritys.Great:
                            i.sprite = Resources.Load<Sprite>("/Images/Iron-Sword-Icon");
                            break;
                        case BaseStatItem.ItemRaritys.Common:
                            i.sprite = Resources.Load<Sprite>("/Images/Stone-Sword-Icon");
                            break;
                        case BaseStatItem.ItemRaritys.Rusty:
                            i.sprite = Resources.Load<Sprite>("/Images/Wooden-Sword-Icon");
                            break;
                    }
                    break;
                case BaseWeapon.WeaponTypes.Spear:
                    i.sprite = Resources.Load<Sprite>("/Images/Spear");
                    break;
                case BaseWeapon.WeaponTypes.Bow:
                    i.sprite = Resources.Load<Sprite>("/Images/Bow");
                    break;
                case BaseWeapon.WeaponTypes.Dagger:
                    i.sprite = Resources.Load<Sprite>("/Images/Dagger");
                    break;
                case BaseWeapon.WeaponTypes.Tomb:
                    i.sprite = Resources.Load<Sprite>("/Images/Book");
                    break;
            }



        } else // no weapon
        {
            B.gameObject.GetComponent<Button>().interactable = false;
            B.GetChild(0).gameObject.SetActive(false);
        }

        // gauntlets button
        B = charDetailPanel.transform.GetChild(6);
        if (c.Gauntlets != null) // if gauntlets are equipped
        {
            B.gameObject.GetComponent<Button>().interactable = true;
            B.GetChild(0).gameObject.SetActive(true);
            B.gameObject.GetComponent<Button>().onClick.AddListener(delegate { showInventoryDetailEquipment(c.Gauntlets, c); });
        }
        else // no gauntlets
        {
            B.gameObject.GetComponent<Button>().interactable = false;
            B.GetChild(0).gameObject.SetActive(false);
        }

        // armor button
        B = charDetailPanel.transform.GetChild(7);
        if (c.Armor != null) // if gauntlets are equipped
        {
            B.gameObject.GetComponent<Button>().interactable = true;
            B.GetChild(0).gameObject.SetActive(true);
            B.gameObject.GetComponent<Button>().onClick.AddListener(delegate { showInventoryDetailEquipment(c.Armor, c); });
        }
        else // no gauntlets
        {
            B.gameObject.GetComponent<Button>().interactable = false;
            B.GetChild(0).gameObject.SetActive(false);
        }

        // helmet button
        B = charDetailPanel.transform.GetChild(8);
        if (c.Helmet != null) // if helmet is equipped
        {
            B.gameObject.GetComponent<Button>().interactable = true;
            B.GetChild(0).gameObject.SetActive(true);
            B.gameObject.GetComponent<Button>().onClick.AddListener(delegate { showInventoryDetailEquipment(c.Helmet, c); });
        }
        else // no helmet
        {
            B.gameObject.GetComponent<Button>().interactable = false;
            B.GetChild(0).gameObject.SetActive(false);
        }

        // grieves button
        B = charDetailPanel.transform.GetChild(9);
        if (c.Grieves != null) // if helmet is equipped
        {
            B.gameObject.GetComponent<Button>().interactable = true;
            B.GetChild(0).gameObject.SetActive(true);
            B.gameObject.GetComponent<Button>().onClick.AddListener(delegate { showInventoryDetailEquipment(c.Grieves, c); });
        }
        else // no grieves
        {
            B.gameObject.GetComponent<Button>().interactable = false;
            B.GetChild(0).gameObject.SetActive(false);
        }
    }


    private void showInventoryDetailWeapon(BaseWeapon p, BaseCharacter c)
    {
        Image i = inventoryDetailPanel.transform.GetChild(0).gameObject.GetComponent<Image>();

        switch (p.WeaponType)
        {
            case BaseWeapon.WeaponTypes.Sword:
                switch (p.ItemRarity)
                {
                    case BaseStatItem.ItemRaritys.Legendary:
                        i.sprite = Resources.Load<Sprite>("/Images/Diamond-Sword-Icon");
                        break;
                    case BaseStatItem.ItemRaritys.Flawless:
                        i.sprite = Resources.Load<Sprite>("/Images/Gold-Sword-Icon");
                        break;
                    case BaseStatItem.ItemRaritys.Great:
                        i.sprite = Resources.Load<Sprite>("/Images/Iron-Sword-Icon");
                        break;
                    case BaseStatItem.ItemRaritys.Common:
                        i.sprite = Resources.Load<Sprite>("/Images/Stone-Sword-Icon");
                        break;
                    case BaseStatItem.ItemRaritys.Rusty:
                        i.sprite = Resources.Load<Sprite>("/Images/Wooden-Sword-Icon");
                        break;
                }
                break;
            case BaseWeapon.WeaponTypes.Spear:
                i.sprite = Resources.Load<Sprite>("/Images/Spear");
                break;
            case BaseWeapon.WeaponTypes.Bow:
                i.sprite = Resources.Load<Sprite>("/Images/Bow");
                break;
            case BaseWeapon.WeaponTypes.Dagger:
                i.sprite = Resources.Load<Sprite>("/Images/Dagger");
                break;
            case BaseWeapon.WeaponTypes.Tomb:
                i.sprite = Resources.Load<Sprite>("/Images/Book");
                break;
        }

        inventoryDetailPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = p.ItemName + "\n" + p.ItemRarity;
        inventoryDetailPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Price: $ " + p.Price;
        inventoryDetailPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = p.ItemDescription;
        inventoryDetailPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Damage: " + p.Damage + "\nStrength: " + p.Strength + "\nIntellect: " + p.Intellect + "\nAgility : " + p.Agility + " \nDefense: " + p.Defense;

        inventoryDetailPanel.SetActive(true);
        GameObject B = inventoryDetailPanel.transform.GetChild(6).gameObject;
        B.SetActive(true);
        B.transform.GetChild(1).gameObject.SetActive(false);
        B.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Unequip";
        B.GetComponent<Button>().onClick.RemoveAllListeners();
        B.GetComponent<Button>().onClick.AddListener(delegate { unequip(p, c); });

    }

    private void showInventoryDetailEquipment(BaseEquipment e, BaseCharacter c)
    {
        Image i = inventoryDetailPanel.transform.GetChild(0).gameObject.GetComponent<Image>();

        switch (e.EquipmentType)
        {
            case BaseEquipment.EquipmentTypes.Armor:
                i.sprite = Resources.Load<Sprite>("Images/Armor");
                break;
            case BaseEquipment.EquipmentTypes.Gauntlets:
                i.sprite = Resources.Load<Sprite>("Images/Gauntlets");
                break;
            case BaseEquipment.EquipmentTypes.Grieves:
                i.sprite = Resources.Load<Sprite>("Images/Grieves");
                break;
            case BaseEquipment.EquipmentTypes.Helmet:
                i.sprite = Resources.Load<Sprite>("Images/Helmet");
             break;
        }

        inventoryDetailPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = e.ItemName + "\n" + e.ItemRarity;
        inventoryDetailPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Price: $ " + e.Price;
        inventoryDetailPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = e.ItemDescription;
        inventoryDetailPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Resistance: " + e.Resistance + "\nStrength: " + e.Strength + "\nIntellect: " + e.Intellect + "\nAgility : " + e.Agility + " \nDefense: " + e.Defense;

        inventoryDetailPanel.SetActive(true);
        GameObject B = inventoryDetailPanel.transform.GetChild(6).gameObject;
        B.SetActive(true);
        B.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Unequip";
        B.transform.GetChild(1).gameObject.SetActive(false);
        B.GetComponent<Button>().onClick.RemoveAllListeners();
        B.GetComponent<Button>().onClick.AddListener(delegate { unequip(e, c); });

    }

    private void unequip(BaseItem i, BaseCharacter c)
    {
        switch(i.ItemType)
        {
            case BaseItem.ItemTypes.WEAPON:
                GameInformation.PlayerInventory.Weapons.Add(c.Weapon);
                c.Weapon = null;
                break;
            case BaseItem.ItemTypes.EQUIPMENT:
                switch(((BaseEquipment)i).EquipmentType)
                {
                    case BaseEquipment.EquipmentTypes.Armor:
                        GameInformation.PlayerInventory.Equipment.Add(c.Armor);
                        c.Armor = null;
                        break;
                    case BaseEquipment.EquipmentTypes.Gauntlets:
                        GameInformation.PlayerInventory.Equipment.Add(c.Gauntlets);
                        c.Gauntlets = null;
                        break;
                    case BaseEquipment.EquipmentTypes.Grieves:
                        GameInformation.PlayerInventory.Equipment.Add(c.Grieves);
                        c.Grieves = null;
                        break;
                    case BaseEquipment.EquipmentTypes.Helmet:
                        GameInformation.PlayerInventory.Equipment.Add(c.Helmet);
                        c.Helmet = null;
                        break;
                }
                break;
        }
        inventoryDetailPanel.SetActive(false);
        showCharDetailPanel(c);
    }

    public void DoPause()
    {
        //Set isPaused to true
        isPaused = true;
        //Set time.timescale to 0, this will cause animations and physics to stop updating
        Time.timeScale = 0;

        blur.enabled = true;
        pauseMenuPanel.SetActive(true);
        charactersPanel.SetActive(true);
        areaname.SetActive(false);
        mainScroll.SetActive(false);
        reloadCharInfo(); reloadGoldUsername();
    }


    public void UnPause()
    {
        //Set isPaused to false
        isPaused = false;
        //Set time.timescale to 1, this will cause animations and physics to continue updating at regular speed
        Time.timeScale = 1;

        blur.enabled = false;
        // first hide all panels
        charactersPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        questPanel.SetActive(false);
        gamePanel.SetActive(false);
        charDetailPanel.SetActive(false);
        questDetailPanel.SetActive(false);
        inventoryDetailPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);

        areaname.SetActive(true);
        mainScroll.SetActive(true);
    }


}
