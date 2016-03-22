using UnityEngine;
using System.Collections;

public class EscMenu : MonoBehaviour
{

    public GameObject pauseMenuPanel;

    private bool isPaused;                              //Boolean to check if the game is paused or not
    private StartOptions startScript;                   //Reference to the StartButton script

    // reference to four menu panels
    public GameObject charactersPanel;
    public GameObject inventoryPanel;
    public GameObject questPanel;
    public GameObject gamePanel;

    public GameObject mainCamera;
    private NewBehaviourScript blurOptimized;

    //Awake is called before Start()
    void Awake()
    {
    }

    void Start()
    {
        isPaused = false;
        blurOptimized = (NewBehaviourScript)mainCamera.GetComponent("BlurOptimized");
    }
    // Update is called once per frame
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

    public void tabButtonPressed(int panelToGo)
    {
        // first hide all panels
        charactersPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        questPanel.SetActive(false);
        gamePanel.SetActive(false);

        switch(panelToGo)
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
        pauseMenuPanel.SetActive(false);
    }


}
