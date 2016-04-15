using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using LitJson;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{

    public GameObject usernameField;
    public GameObject passwordField;
    public GameObject loggedinUsername;
    public GameObject loginMenuPanel;
    public GameObject mainMenuPanel;
    public GameObject newCharacterMenuPanel;

    string URL = "http://tomaswolfgang.com/hansa361/Login.php"; //change for your URL
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoginButtonClicked();

        }
    }
    public void logout()
    {
        loggedinUsername.SetActive(false);
        loggedinUsername.GetComponent<Text>().text = "";
        WorldInformation.UserID = "";
        usernameField.GetComponent<InputField>().text = ""; // empty all fields
        passwordField.GetComponent<InputField>().text = "";
    }
    IEnumerator Login(WWW w, string user, string pass)  // login script
    {
        
        
        yield return w; //we wait for the form to check the PHP file, so our game dont just hang
        if (w.error != null)
        {
            print("Error logging in");
            ErrorWindow.showErrorWindow("w error");
            Debug.Log(w.error);
        } else
        {
            Debug.Log(w.text);
            if (Int32.Parse(w.text) != 1) // login success
            {
                
                WorldInformation.UserID = Int32.Parse(w.text) + "";
                Debug.Log("UserID = " + Int32.Parse(w.text));
                loggedinUsername.SetActive(true);
                loggedinUsername.GetComponent<Text>().text = "Welcome " + user + "!";
                LoadInformation.LoadAllInformation();        // load all the info within computer.
                if (GameInformation.PlayerCharacter == null) // no characters yet
                {
                    loginMenuPanel.SetActive(false);
                    newCharacterMenuPanel.SetActive(true);
                }
                else {
                    loginMenuPanel.SetActive(false);
                    mainMenuPanel.SetActive(true);
                    GameObject.Find("/UI").GetComponent<MainMenu>().enterMainMenu();
                }
            }

            else
            {
                ErrorWindow.showErrorWindow("Incorrect username or password");
            } 
        }
    }
    public void LoginButtonClicked()
    {
        string u = usernameField.GetComponent<InputField>().text;
        string p = passwordField.GetComponent<InputField>().text;
        

        if (u.Length > 0 && p.Length > 0) // if user entered 
        {
            WWWForm form = new WWWForm(); //here you create a new form connection
            form.AddField("username", u);
            form.AddField("password", p);
            WWW w = new WWW(URL, form); //here we create a var called 'w' and we sync with our URL and the form
            StartCoroutine(Login(w, u, p));

        } 
        else
        {
            // empty character name - error windows is shown
            ErrorWindow.showErrorWindow("Empty Character Name");
        }

        

    }
    


}
