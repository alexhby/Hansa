using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//[RequireComponent(typeof(Text))]
public class Dialogue2 : MonoBehaviour
{

    private Text textComponent;
    private int stringIndex = 0;
    //Make if statement: if you own this region, they address you by name
    //Make separate starting welcome to ... for different stores
    //First time add separate dialogue
    public static string[] DialogueStrings = new string[] { "The roads are dangerous and full of terrors", "I can use some help if you are interested in a bit of coin" };

    public KeyCode DialogueInput = KeyCode.Return;
    private bool isStringBeingRevealed = false;
    private bool isEndOfDialogue = false;
    private bool lastSentence = false;
    private float SecondsBetweenChars = 0.05f;
    //public float CharRateMultiplier;
    public GameObject shopOption1;
    public GameObject shopOption2;
    public GameObject shopOption3;
    public GameObject initPanel;
    public GameObject texty;
    public GameObject theOtherDude;
    public GameObject LeaveShop;

    private bool init = false;

    // Use this for initialization
    void Start()
    {
        StateManager.ShopState = StateManager.ShopStates.OUTSIDE;

        textComponent = texty.GetComponent<Text>();
        textComponent.text = "";
        //Debug.Log(DialogueStrings[0]);
        HideIcons();

    }

    public void shopQuestkeep()
    {
        HideIcons();
        stringIndex = 0;
        if (init)
        {
            DialogueStrings[0] = "Do I look like a damn shopkeep to you?";
            DialogueStrings[1] = "Go bother my partner if you're lookin to buy";
        }
        textComponent.text = "";
        
        lastSentence = false;
        isEndOfDialogue = false;
        if (!lastSentence && !isStringBeingRevealed)
        {
            textComponent.text = "";
            if (!isStringBeingRevealed)
            {
                isStringBeingRevealed = true;
                StartCoroutine(DisplayString(DialogueStrings[stringIndex]));


            }


        }
    }

    public void initShopkeep()
    {
        if (StateManager.ShopState == StateManager.ShopStates.OUTSIDE)
        {
            LeaveShop.SetActive(false);
            StateManager.ShopState = StateManager.ShopStates.QUEST;
            HideIcons();
            stringIndex = 0;
            if (init)
            {
                DialogueStrings[0] = "Thought a bit more about my offers?";
                DialogueStrings[1] = "It's good pay for good work";
            }
            textComponent.text = "";
            init = true;
            lastSentence = false;
            isEndOfDialogue = false;
            theOtherDude.SetActive(false);
            initPanel.SetActive(true);
            if (!lastSentence && !isStringBeingRevealed)
            {
                textComponent.text = "";
                if (!isStringBeingRevealed)
                {
                    isStringBeingRevealed = true;
                    StartCoroutine(DisplayString(DialogueStrings[stringIndex]));


                }


            }
        }

    }

    public void closeShopkeep()
    {
        LeaveShop.SetActive(true);
        theOtherDude.SetActive(true);
        StateManager.ShopState = StateManager.ShopStates.OUTSIDE;
        initPanel.SetActive(false);
    }

   

    // Update is called once per frame
    void Update()
    {

        if (init && Input.GetKeyDown(KeyCode.Return) && !lastSentence && !isStringBeingRevealed)
        {
            textComponent.text = "";
            if (!isStringBeingRevealed)
            {
                isStringBeingRevealed = true;
                StartCoroutine(DisplayString(DialogueStrings[stringIndex]));


            }


        }

    }
    private IEnumerator DisplayString(string StringToDisplay)
    {
        //HideIcons();
        int stringLength = StringToDisplay.Length;
        int currentCharIndex = 0;
        textComponent.text = "";
        while (currentCharIndex < stringLength)
        {
            textComponent.text += StringToDisplay[currentCharIndex];
            currentCharIndex++;
            if (currentCharIndex < stringLength)
            {
                if (Input.GetKey("space"))
                {
                    textComponent.text = StringToDisplay;
                    isEndOfDialogue = true;
                    isStringBeingRevealed = false;
                    break;
                    //yield return new WaitForSeconds(SecondsBetweenChars * CharRateMultiplier);
                }
                else
                {
                    yield return new WaitForSeconds(SecondsBetweenChars);
                }

            }
            else
            {
                isStringBeingRevealed = false;
                isEndOfDialogue = true;
                break;
            }
        }
        //ShowIcons();

        isEndOfDialogue = false;

        if (stringIndex == 1)
        {
            showOptions();
        }
        else stringIndex++;


    }

    private void showOptions()
    {
        lastSentence = true;
        shopOption1.SetActive(true);
        shopOption2.SetActive(true);
        shopOption3.SetActive(true);


    }

    private void HideIcons()
    {
        shopOption1.SetActive(false);
        shopOption2.SetActive(false);
        shopOption3.SetActive(false);

    }

    private void ShowIcons()
    {
        //if(isEndOfDialogue)
        //{
        //    StopIcon.SetActive(true);
        //    return;
        //}
        //ContinueIcon.SetActive(true);
    }
}


