using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ErrorWindow : MonoBehaviour {
   
    
    public static void showErrorWindow(string errorMsg)
    {
        GameObject errorWindow = GameObject.Find("UI/BasePanel/ErrorWindow");
        errorWindow.SetActive(true);
        errorWindow.transform.GetChild(1).GetComponent<Text>().text = errorMsg;
    }
    
}
