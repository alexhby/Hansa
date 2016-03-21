using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
    private string tester = "hey there";
    private string url = "http://tomaswolfgang.com/hansa361/test.php";
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 200, 24), "Fetch data from php"))
        {
            WWW www = new WWW(url);
            StartCoroutine(goDoIt(www));
        }
        GUILayout.Toggle(true, tester);
    }

    IEnumerator goDoIt(WWW www)
    {
        yield return www;

        tester = www.text;
    }
}
