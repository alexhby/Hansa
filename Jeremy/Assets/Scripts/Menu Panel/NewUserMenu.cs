using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class NewUserMenu : MonoBehaviour
{



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


    public void CreateButtonClicked()
    {
        if (true) // TODO : replace with username / confirm password check
        {

            //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
            Invoke("CreateLoadDelayed", fadeColorAnimationClip.length * .5f);

            //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
            animColorFade.SetTrigger("fade");

        }
        // when  failed
        else
        {

        }

    }

    public void BackButtonClicked()
    {
        //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
        Invoke("BackLoadDelayed", fadeColorAnimationClip.length * .5f);

        //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
        animColorFade.SetTrigger("fade");

    }


    public void CreateLoadDelayed()
    {

        //Hide the newuser menu UI element
        showPanels.HideMenu(2);
        // Show the main menu UI
        showPanels.ShowMenu(1);
    }

    public void BackLoadDelayed()
    {

        //Hide the newuser menu UI element
        showPanels.HideMenu(2);
        //Show the login menu
        showPanels.ShowMenu(0);
    }
    public void HideDelayed()
    {
        //Hide the main menu UI element after fading out menu for start game in scene
        showPanels.HideMenu(2);
    }


}

