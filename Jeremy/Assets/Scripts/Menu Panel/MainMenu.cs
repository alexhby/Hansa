using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
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


    public void NewGameButtonClicked()
    {
            //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
            Invoke("NewGameLoadDelayed", fadeColorAnimationClip.length * .5f);

            //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
            animColorFade.SetTrigger("fade");
        
    }

    public void LoadGameButtonClicked()
    {
        //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
        Invoke("LoadGameLoadDelayed", fadeColorAnimationClip.length * .5f);

        //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
        animColorFade.SetTrigger("fade");

    }

    public void BackButtonClicked()
    {
        //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
        Invoke("BackLoadDelayed", fadeColorAnimationClip.length * .5f);

        //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
        animColorFade.SetTrigger("fade");

    }



    public void NewGameLoadDelayed()
    {

        //Hide the login menu UI element
        showPanels.HideMenu(1);
        //Show the world menu
        showPanels.ShowMenu(4);
    }

    public void LoadGameLoadDelayed()
    {

        //Hide the login menu UI element
        showPanels.HideMenu(1);
        //Show the load game menu
        showPanels.ShowMenu(3);
    }

    public void BackLoadDelayed()
    {

        //Hide the login menu UI element
        showPanels.HideMenu(1);
        //Show the login menu
        showPanels.ShowMenu(0);
    }

}
