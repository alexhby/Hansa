using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using LitJson;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class JoinWorldMenu : MonoBehaviour
{



    string URL = "http://tomaswolfgang.com/hansa361/ShowAllWorlds.php"; //change for your URL
    private JsonData WorldData;
    public GameObject joinWorldContent;

    private PlayMusic playMusic;                                        //Reference to PlayMusic script
    private float fastFadeIn = .01f;                                    //Very short fade time (10 milliseconds) to start playing music immediately without a click/glitch
    private ShowPanels showPanels;                                      //Reference to ShowPanels script on UI GameObject, to show and hide panels
                                                                        // Use this for initialization
    void Awake()
    {
        //Get a reference to ShowPanels attached to UI object
        showPanels = GetComponent<ShowPanels>();

        //Get a reference to PlayMusic attached to UI object
        playMusic = GetComponent<PlayMusic>();
    }

    public void enterJoinWorldPanel()
    {
        WWW w = new WWW(URL); //here we create a var called 'w' and we sync with our URL and the form
        StartCoroutine(ShowAllWorlds(w));
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

            // Destroy all contents first
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in joinWorldContent.transform) children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));

            int height = -10;
            for (int i = 0; i < WorldData.Count; i++)
            {
                GameObject worldButton = (GameObject)Instantiate(Resources.Load("JoinWorldButton")); // instantiate the prefab
                worldButton.SetActive(true);
                worldButton.transform.SetParent(joinWorldContent.transform);                         // set joinWorldContent as parent
                SetListener(worldButton.GetComponent<Button>(), WorldData[i]["world_ID"].ToString());// set listener
                RectTransform ButtonRect = (RectTransform)worldButton.transform;                     // get rect transform
                ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);                           // set position, max, min, pivot,localscale
                ButtonRect.anchorMax = new Vector2(0.5f, 1);
                ButtonRect.anchorMin = new Vector2(0.5f, 1);
                ButtonRect.anchorMin = new Vector2(0.5f, 1);
                ButtonRect.pivot = new Vector2(0.5f, 1);
                ButtonRect.localScale = new Vector3(1, 1, 1);
                height -= 80;                                                                        // go down 80 (height = 70, gutter = 10)
                GameObject WorldName = worldButton.transform.GetChild(0).gameObject;                 // get worldname
                WorldName.GetComponent<Text>().text = WorldData[i]["name"].ToString();               // set worldname
                
            }
        }
    }
    private void SetListener(Button B, string id)
    {

        B.onClick.AddListener(delegate { joinWorldButtonClicked(id); });

    }
    void joinWorldButtonClicked(string worldID)
    {
        WorldInformation.currentWorldID = worldID; // set worldID
        SceneManager.LoadScene("test");                  // load scene

    }
}
