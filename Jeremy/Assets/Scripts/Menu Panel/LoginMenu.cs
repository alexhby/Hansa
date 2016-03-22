using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using LitJson;


public class LoginMenu : MonoBehaviour
{


    private string formUsername = ""; //this is the field where the player will put the name to login
    private string formPassword = ""; //this is his password
    string formText = ""; //this field is where the messages sent by PHP script will be in

    string URL = "http://mywebsite/check_scores.php"; //change for your URL
    string hash = "hashcode"; //change your secret code, and remember to change into the PHP file too


    public int sceneToStart = 1;                                        //Index number in build settings of scene to load

    [HideInInspector]
    public bool inMainMenu = true;                  //If true, pause button disabled in main menu (Cancel in input manager, default escape key)
    [HideInInspector]
    public Animator animColorFade;                  //Reference to animator which will fade to and from black when starting game.
    [HideInInspector]
    public Animator animMenuAlpha;                  //Reference to animator that will fade out alpha of MenuPanel canvas group
    public AnimationClip fadeColorAnimationClip;        //Animation clip fading to color (black default) when changing scenes
    [HideInInspector]
    public AnimationClip fadeAlphaAnimationClip;        //Animation clip fading out UI elements alpha


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

    IEnumerator Login(string user, string pass)  // login script
    {
        WWWForm form = new WWWForm(); //here you create a new form connection
        form.AddField("myform_hash", hash); //add your hash code to the field myform_hash, check that this variable name is the same as in PHP file
        form.AddField("myform_user", formUsername);
        form.AddField("myform_pass", formPassword);
        WWW w = new WWW(URL, form); //here we create a var called 'w' and we sync with our URL and the form
        yield return w; //we wait for the form to check the PHP file, so our game dont just hang
        
        if(w.error != null)
        {
            print("Error logging in");
        }
    }
    public void LoginButtonClicked()
    {

        
        if (true) // TODO : replace with login check
        {
            
                //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
                Invoke("LoadDelayed", fadeColorAnimationClip.length * .5f);

                //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
                animColorFade.SetTrigger("fade");
            
        } 
        // when login is failed
        else
        {

        }

    }
    public void NewUserButtonClicked()
    {
        //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
        Invoke("NewUserLoadDelayed", fadeColorAnimationClip.length * .5f);

        //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
        animColorFade.SetTrigger("fade");
    }


    public void LoadDelayed()
    {

        //Hide the login menu
        showPanels.HideMenu(0);
        //Show the main menu
        showPanels.ShowMenu(1);
    }

    public void NewUserLoadDelayed()
    {

        //Hide the login menu
        showPanels.HideMenu(0);
        //Show the new user menu
        showPanels.ShowMenu(2);
    }

  

 
}
