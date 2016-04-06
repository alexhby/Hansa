using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {

    private int currentWeapon;
    public int maxWeapons = 5;

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
    public void selectWeapon(int weaponNum)
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


    // Update is called once per frame
	void Update () {

        /*//FOR DEBUG       
        //WEAPON SWITCHING

        //Sword
        if (Input.GetKeyDown("1"))
        {
            currentWeapon = 1;
        }
        //Spear
        else if (Input.GetKeyDown("2"))
        {
            currentWeapon = 2;
            //hasShield = false;
        }
        //CrossBow
        else if (Input.GetKeyDown("3"))
        {
            currentWeapon = 3;
            //hasShield = false;
        }
        //No weapon
        else if (Input.GetKeyDown("4"))
        {
            currentWeapon = 4;
            //hasShield = false;
        }*/
        
        selectWeapon(currentWeapon);
	}
}