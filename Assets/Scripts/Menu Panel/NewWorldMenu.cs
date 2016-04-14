using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class NewWorldMenu : MonoBehaviour
{



    public int sceneToStart = 1;                                        //Index number in build settings of scene to load
    public int kingdomsizeValue = 4;

    string URL = "http://tomaswolfgang.com/hansa361/CreateNewWorld.php"; //change for your URL

    public GameObject kingdomText;
    public GameObject kingdomSlider;
    public GameObject worldNameField;

    private PlayMusic playMusic;                                        //Reference to PlayMusic script
    private float fastFadeIn = .01f;                                    //Very short fade time (10 milliseconds) to start playing music immediately without a click/glitch
    private ShowPanels showPanels;                                      //Reference to ShowPanels script on UI GameObject, to show and hide panels


    void Awake()
    {
        //Get a reference to ShowPanels attached to UI object
        showPanels = GetComponent<ShowPanels>();

        //Get a reference to PlayMusic attached to UI object
        playMusic = GetComponent<PlayMusic>();
    }
    

    public void kingdomChanged()
    {
        Text kingdomTextComponent = kingdomText.GetComponent<Text>();
        kingdomsizeValue = (int)kingdomSlider.GetComponent<Slider>().value;
        kingdomTextComponent.text = kingdomsizeValue + " Kingdoms";
    }

    IEnumerator CreateNewWorld(WWW w)  // login script
    {


        yield return w; //we wait for the form to check the PHP file, so our game dont just hang

        if (w.error != null)
        {
            print("Error");
        }
        else
        {
            Debug.Log("WorldID = " + w.text);
            if (Int32.Parse(w.text) != 1) // user creation success
            {

                
                ErrorWindow.showErrorWindow("World Created");
                //while(GameObject.Find("UI/BasePanel/ErrorWindow").activeInHierarchy) // wait until user closes the window (i.e. ErrorWindow.activeInHierarchy == false)
                //{
                //    System.Threading.Thread.Sleep(250); // pause for 1/4 second
                //}
                WorldInformation.currentWorldID = Int32.Parse(w.text) + ""; // set worldID
                //SceneManager.LoadScene("test");                  // load scene
            }

            else
            {
                ErrorWindow.showErrorWindow("World name already exists");
            }
        }
    }
    public void CreateButtonClicked()
    {
        string worldname = worldNameField.GetComponent<InputField>().text;
        if (worldname.Length > 0)
        {
            Debug.Log(worldname);
            WWWForm form = new WWWForm(); //here you create a new form connection
            form.AddField("kingdomCount", (int)kingdomSlider.GetComponent<Slider>().value);
            form.AddField("userID", WorldInformation.UserID);
            form.AddField("name", worldname);
            WWW w = new WWW(URL, form); //here we create a var called 'w' and we sync with our URL and the form
            StartCoroutine(CreateNewWorld(w));

        }
        else
        {
            // empty character name - error window is shown
            ErrorWindow.showErrorWindow("Empty World name");
        }
    }


}
