using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
/*
Class that controls tab buttons in pause menu
*/
public class EscMenu : MonoBehaviour
{

    public GameObject pauseMenuPanel;                   // reference to pause menu panel

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
        blur = GetComponent<BlurOptimized>();
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
                charInfoButtons[i].transform.GetChild(1).gameObject.SetActive(true);
                charInfoButtons[i].transform.GetChild(2).gameObject.SetActive(true);
                charInfoButtons[i].transform.GetChild(3).gameObject.SetActive(true);


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
                charhp.sizeDelta = new Vector2(chars[i].CurrentHealth * 290/chars[i].Health, 15);

                // Mana - MAX MANA = 100
                RectTransform charmp = charInfoButtons[i].transform.GetChild(3).GetChild(0).gameObject.GetComponent<RectTransform>();
                charmp.sizeDelta = new Vector2(chars[i].Mana * 2.9f, 15);
            }
            else
            {
                charInfoButtons[i].GetComponent<Button>().interactable = false; // disable button
                charInfoButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                charInfoButtons[i].transform.GetChild(2).gameObject.SetActive(false);
                charInfoButtons[i].transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }

    public void reloadGoldUsername()
    {
        Text goldtext = goldUsername.GetComponent<Text>();
        goldtext.text = "$ " + GameInformation.Gold + "\n username";
    }
    public void reloadInventory()
    {
        GameObject ItemB = (GameObject)Instantiate(Resources.Load("InventoryButton"));
        ItemB.transform.SetParent(inventoryContent.transform);
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
    }


}
