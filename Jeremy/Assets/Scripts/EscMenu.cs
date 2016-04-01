using UnityEngine;
using System.Collections;
/*
    Class that controls tab buttons in pause menu
*/
public class EscMenu : MonoBehaviour
{

    public GameObject pauseMenuPanel;                   // reference to pause menu panel

    private bool isPaused;                              //Boolean to check if the game is paused or not

    // reference to menu panels
    public GameObject charactersPanel;                  
    public GameObject inventoryPanel;
    public GameObject questPanel;
    public GameObject gamePanel;
    public GameObject charDetailPanel;
    public GameObject questDetailPanel;
    public GameObject inventoryDetailPanel;

    // reference to main camera
    public GameObject mainCamera;

    //Awake is called before Start()
    void Awake()
    {

    }

    void Start()
    {
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
            case 1: charactersPanel.SetActive(true); break;
            case 2: inventoryPanel.SetActive(true); break;
            case 3: questPanel.SetActive(true); break;
            case 4: gamePanel.SetActive(true); break;
        }
        

    }


    public void DoPause()
    {
        //Set isPaused to true
        isPaused = true;
        //Set time.timescale to 0, this will cause animations and physics to stop updating
        Time.timeScale = 0;

        //blurOptimized.enabled = true;
        pauseMenuPanel.SetActive(true);
        charactersPanel.SetActive(true);
    }


    public void UnPause()
    {
        //Set isPaused to false
        isPaused = false;
        //Set time.timescale to 1, this will cause animations and physics to continue updating at regular speed
        Time.timeScale = 1;

        //blurOptimized.enabled = false;
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
