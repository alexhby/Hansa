using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerEventsControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private AudioClip audioclip;
    private AudioSource audiosource;
    private Transform abPanel;
    public Transform charac;
    private CharController c;
    public BaseCharacter myCharacter;
    public int i;

    public void Start()
    {
        audioclip = Resources.Load<AudioClip>("button_hover_sound");
        gameObject.AddComponent<AudioSource>();
        audiosource = gameObject.GetComponent<AudioSource>();
        abPanel = transform.parent.parent.Find("Ability Description");
       

    }
    public void Update()
    {
        try
        {
            c = charac.GetComponent<CharController>();
            myCharacter = c.myClass;
        }
        catch (Exception e)
        {
            return;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        abPanel.gameObject.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(audioclip);

        i = int.Parse(gameObject.name.Substring(2, 1));

        Text ability = abPanel.Find("Ability").GetComponent<Text>();
        Text range = abPanel.Find("Range").GetComponent<Text>();
        Text damage = abPanel.Find("Damage").GetComponent<Text>();
        Text weapon = abPanel.Find("Weapon").GetComponent<Text>();

        ability.text = myCharacter.skills[i - 1].description;
        range.text = "Range: " + myCharacter.skills[i - 1].minRange + "-" + myCharacter.skills[i - 1].maxRange;
        damage.text = "Damage: " + (myCharacter.skills[i - 1].isPhysical ? c.myPhysicalDamage : c.myMagicDamage);
        weapon.text = "Weapon: " + c.weapon;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        abPanel.gameObject.SetActive(false);
    }

}