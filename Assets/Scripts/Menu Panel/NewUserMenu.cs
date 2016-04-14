using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class NewUserMenu : MonoBehaviour
{

    public GameObject usernameField;
    public GameObject passwordField;
    public GameObject passwordConfirmField;
    public GameObject newUserMenuPanel;
    public GameObject loginMenuPanel;
    

    string URL = "http://tomaswolfgang.com/hansa361/createNewUser.php"; //change for your URL



    private PlayMusic playMusic;                                        //Reference to PlayMusic script
    private float fastFadeIn = .01f;                                    //Very short fade time (10 milliseconds) to start playing music immediately without a click/glitch
    

    void Awake()
    {

        //Get a reference to PlayMusic attached to UI object
        playMusic = GetComponent<PlayMusic>();
    }
    public void enterNewUserPanel() // when you enter the scene...
    {
        usernameField.GetComponent<InputField>().text = ""; // empty all fields
        passwordField.GetComponent<InputField>().text = "";
        passwordConfirmField.GetComponent<InputField>().text = "";
    }
    IEnumerator CreateNewUser(WWW w, string user, string pass)  // login script
    {


        yield return w; //we wait for the form to check the PHP file, so our game dont just hang

        if (w.error != null)
        {
            print("Error logging in");
        }
        else
        {
            Debug.Log(w.text);
            if (Int32.Parse(w.text) != 1) // user creation success
            {
                
                newUserMenuPanel.SetActive(false);
                loginMenuPanel.SetActive(true);
                ErrorWindow.showErrorWindow("User created : Please login again");
            }

            else
            {
                ErrorWindow.showErrorWindow("Username already exists");
            }
        }
    }
    public void CreateButtonClicked()
    {
        string u = usernameField.GetComponent<InputField>().text;
        string p = passwordField.GetComponent<InputField>().text;
        string pc = passwordConfirmField.GetComponent<InputField>().text;


        if (u.Length > 0 && p.Length > 0) // if user entered 
        {
            if (p.Equals(pc))
            {
                WWWForm form = new WWWForm(); //here you create a new form connection
                form.AddField("username", u);
                form.AddField("password", p);
                WWW w = new WWW(URL, form); //here we create a var called 'w' and we sync with our URL and the form
                StartCoroutine(CreateNewUser(w, u, p));
            } else
            {
                // passowrds do not match - error window is shown
                ErrorWindow.showErrorWindow("Passwords do not match");
            }
            
        }
        else
        {
            // empty character name - error window is shown
            ErrorWindow.showErrorWindow("Empty Username or Password");
        }

    }

}

