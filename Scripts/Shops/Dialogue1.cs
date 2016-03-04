using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Dialogue1 : MonoBehaviour
{

    private Text textComponent;
    private int stringIndex = 0;
    //Make if statement: if you own this region, they address you by name
    //Make separate starting welcome to ... for different stores
    //First time add separate dialogue
    public static string[] DialogueStrings = new string[] { "Hello! Welcome to my non-generic shop! ", "What do you need?" };

    public KeyCode DialogueInput = KeyCode.Return;
    private bool isStringBeingRevealed = false;
    private bool isEndOfDialogue = false;
    private bool lastSentence = false;
    private float SecondsBetweenChars = 0.05f;
    //public float CharRateMultiplier;
    public GameObject shopOption1;
    public GameObject shopOption2;
    public GameObject shopOption3;


    // Use this for initialization
    void Start()
    {

        textComponent = GetComponent<Text>();
        textComponent.text = "";
        //Debug.Log(DialogueStrings[0]);
        HideIcons();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) && !lastSentence && !isStringBeingRevealed)
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

