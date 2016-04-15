using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using LitJson;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

    string ShowURL = "http://tomaswolfgang.com/hansa361/ShowYourWorld.php"; //change for your URL
    string JoinURL = "http://tomaswolfgang.com/hansa361/ShowAllWorlds.php"; //change for your URL
    string DeleteURL = "http://tomaswolfgang.com/hansa361/DeleteWorld.php"; //change for your URL

    private JsonData WorldData;

    public GameObject newWorldButton;                                   // new world / continue with your world
    public GameObject deleteWorldButton;                                // delete your world
    public GameObject MainMenuPanel;
    public GameObject NewWorldMenuPanel;

    string userID = "";


    public void enterMainMenu()
    {

        userID = WorldInformation.UserID;
        WWWForm form = new WWWForm(); //here you create a new form connection
        form.AddField("userID", userID);
        WWW w = new WWW(ShowURL, form); //here we create a var called 'w' and we sync with our URL and the form
        StartCoroutine(ShowYourWorld(w));
    }

    IEnumerator ShowYourWorld(WWW w)  // login script
    {


        yield return w; //we wait for the form to check the PHP file, so our game dont just hang
        if (w.error != null)
        {
            print("Error logging in");
            ErrorWindow.showErrorWindow("w error");
            Debug.Log(w.error);
        }
        else
        {
            Debug.Log(w.text);
            if (w.text.Length < 10) // user does not have a world
            {
                
                newWorldButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = "New World";
                RectTransform ButtonRect = (RectTransform)newWorldButton.transform;                     // get rect transform
                ButtonRect.anchoredPosition3D = new Vector3(0, 50, 0);                           // set position to 0, 50, 0;
                deleteWorldButton.SetActive(false);                                              // remove deleteWorldButton

                newWorldButton.GetComponent<Button>().onClick.RemoveAllListeners();
                newWorldButton.GetComponent<Button>().onClick.AddListener(delegate { newWorld(); });
            }

            else
            {
                newWorldButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Continue";
                RectTransform ButtonRect = (RectTransform)newWorldButton.transform;                     // get rect transform
                ButtonRect.anchoredPosition3D = new Vector3(0, 100, 0);                           // set position to 0, 100, 0;
                deleteWorldButton.SetActive(true);                                              // remove deleteWorldButton

                newWorldButton.GetComponent<Button>().onClick.RemoveAllListeners();
                newWorldButton.GetComponent<Button>().onClick.AddListener(delegate { continueWorld(); });
            }
        }
    }

    private void newWorld()
    {
        NewWorldMenuPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    private void continueWorld()
    {
        WWW w = new WWW(JoinURL); //here we create a var called 'w' and we sync with our URL and the form
        StartCoroutine(ShowAllWorlds(w));
    
    }

    public void deleteWorld()
    {
        WWW w = new WWW(JoinURL); //here we create a var called 'w' and we sync with our URL and the form
        StartCoroutine(ShowAllWorldstoDelete(w));
    }

    IEnumerator ShowAllWorlds(WWW w)
    {


        yield return w; //we wait for the form to check the PHP file, so our game dont just hang
        WorldData = JsonMapper.ToObject(w.text);

        if (w.error != null)
        {
            print("Error getting world info");
        }
        else
        {
            Debug.Log(w.text);
            string worldID = "";
            for (int i = 0; i < WorldData.Count; i++)
            {
                if (WorldData[i]["owner_ID"].ToString() == userID)
                {
                    worldID = WorldData[i]["world_ID"].ToString();
                    break;
                    
                }
            }
            if (!worldID.Equals(""))
            {
                WorldInformation.currentWorldID = worldID; // set worldID
                GameObject.Find("/GameInformation").GetComponent<WorldInformation>().StartWorld();  //Start world
            }
            
        }
    }

    IEnumerator ShowAllWorldstoDelete(WWW w)
    {


        yield return w; //we wait for the form to check the PHP file, so our game dont just hang
        WorldData = JsonMapper.ToObject(w.text);

        if (w.error != null)
        {
            print("Error getting world info");
        }
        else
        {
            Debug.Log(w.text);
            string worldID = "";
            for (int i = 0; i < WorldData.Count; i++)
            {
                if (WorldData[i]["owner_ID"].ToString() == userID)
                {
                    worldID = WorldData[i]["world_ID"].ToString();
                    break;

                }
            }
            if (!worldID.Equals(""))
            {
                
                WWWForm form = new WWWForm(); //here you create a new form connection
                form.AddField("worldID", worldID);
                WWW w2 = new WWW(DeleteURL, form); //here we create a var called 'w' and we sync with our URL and the form
                StartCoroutine(DeleteWorld(w2));
            }

        }
    }

    IEnumerator DeleteWorld (WWW w)
    {
        yield return w; //we wait for the form to check the PHP file, so our game dont just hang
        WorldData = JsonMapper.ToObject(w.text);

        if (w.error != null)
        {
            print("Error getting world info");
        }
        else
        {
            Debug.Log(w.text);
            if(Int32.Parse(w.text) != 1) // delete success
            {

                string worldID = "";
                enterMainMenu();
                
            }

            else
            {
                ErrorWindow.showErrorWindow("Error deleting world");
            }
           
            

        }
    }
}
