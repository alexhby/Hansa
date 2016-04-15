using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // when "new user" button is pressed
    public void NewUser()
    {
        System.Diagnostics.Process.Start("http://google.com"); // update to our own web page
    }
}
