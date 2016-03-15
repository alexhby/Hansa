using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;

public class LoadPlayerInventory : MonoBehaviour {

    public static JsonData inv;
    private string url = "http://localhost/361/playerInvetory.php";
    // Use this for initialization
    void Start()
    {
        //WWWForm form = new WWWForm();
        //form.AddField("userID", GameInformation.);
        WWW www = new WWW(url);
        StartCoroutine(goDoIt(www));
    }

    IEnumerator goDoIt(WWW www)
    {
        yield return www;
        inv = JsonMapper.ToObject(www.text);
        Debug.Log(inv[0]["NAME"]);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
