using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {

    private int currentWeapon;
    public int maxWeapons = 5;
    public bool showDagger = false;
    public Animator anim;

    //getters and setters
    public int getCurrentWeapon()
    {
        return currentWeapon;
    }
    public void setCurrentWeapon(int weaponNum)
    {
        currentWeapon = weaponNum;
    }

    //functions
    public void toggleDagger()
    {
        if ( showDagger )
            transform.GetChild(0).gameObject.SetActive(true);
        else 
            transform.GetChild(0).gameObject.SetActive(false);
  
    }

    void Start()
    {
        anim = transform.parent.parent.parent.parent.parent.parent.parent.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (anim.GetFloat("Run") == 2.0f)
        {
            showDagger = false;
            toggleDagger();
        }
        else
        {
            showDagger = true;
            toggleDagger();
        }
    }
}