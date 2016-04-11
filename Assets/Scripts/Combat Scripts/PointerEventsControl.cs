using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PointerEventsControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public int mouseOnCount = 0;
    private AudioClip audioclip;
    private AudioSource audiosource;
    private Transform abPanel;
    public BaseCharacter myCharacter;

    public void Start()
    {
        audioclip = Resources.Load<AudioClip>("button_hover_sound");
        gameObject.AddComponent<AudioSource>();
        audiosource = gameObject.GetComponent<AudioSource>();
        abPanel = transform.parent.parent.Find("Ability Description");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        abPanel.gameObject.SetActive(true);
        mouseOnCount = mouseOnCount + 1;
        Debug.Log(mouseOnCount);
        GetComponent<AudioSource>().PlayOneShot(audioclip);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        abPanel.gameObject.SetActive(false);
    }

}