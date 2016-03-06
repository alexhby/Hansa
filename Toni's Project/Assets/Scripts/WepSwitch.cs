using UnityEngine;
using System.Collections;

public class WepSwitch : MonoBehaviour {

    public int currentWeapon = 0;
    public int maxWeapons = 3;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("1"))
        {
            currentWeapon = 0;
        }
        else if (Input.GetKeyDown("2"))
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown("3"))
        {
            currentWeapon = 2;
        }
        else if (Input.GetKeyDown("4"))
        {
            currentWeapon = 3;
        }
        selectWeapon(currentWeapon);
	}

    void selectWeapon(int weaponNum)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == weaponNum)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
