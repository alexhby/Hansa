using UnityEngine;
using System.Collections;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint
	
    public GameObject loginMenuPanel;                            //Store a reference to the Game Object MenuPanel 
    public GameObject mainMenuPanel;                            //Store a reference to the Game Object MenuPanel
    public GameObject newUserMenuPanel;                      //Store a reference to the Game Object MenuPanel 
    public GameObject loadGameMenuPanel;                            //Store a reference to the Game Object MenuPanel
    public GameObject worldMenuPanel;                            //Store a reference to the Game Object MenuPanel 
    public GameObject joinWorldMenuPanel;                      //Store a reference to the Game Object MenuPanel 
    public GameObject newWorldMenuPanel;                      //Store a reference to the Game Object MenuPanel 

    public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 


	//Call this function to activate and display the Options panel during the main menu
	public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
		optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu(int i)
	{
        switch (i)
        {
            case 0:
                loginMenuPanel.SetActive(true);
                break;
            case 1:
                mainMenuPanel.SetActive(true);
                break;
            case 2:
                newUserMenuPanel.SetActive(true);
                break;
            case 3:
                loadGameMenuPanel.SetActive(true);
                break;
            case 4:
                worldMenuPanel.SetActive(true);
                break;
            case 5:
                joinWorldMenuPanel.SetActive(true);
                break;
            case 6:
                newWorldMenuPanel.SetActive(true);
                break;
        }
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu(int i)
	{
        switch (i)
        {
            case 0:
                loginMenuPanel.SetActive(false);
                break;
            case 1:
                mainMenuPanel.SetActive(false);
                break;
            case 2:
                newUserMenuPanel.SetActive(false);
                break;
            case 3:
                loadGameMenuPanel.SetActive(false);
                break;
            case 4:
                worldMenuPanel.SetActive(false);
                break;
            case 5:
                joinWorldMenuPanel.SetActive(false);
                break;
            case 6:
                newWorldMenuPanel.SetActive(false);
                break;
        }
    }
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);

	}
}
