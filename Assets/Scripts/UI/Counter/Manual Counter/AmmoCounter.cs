using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    public int ammo;
    public Text ammoDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Display the amount of ammo left
        ammoDisplay.text = "Ammo Left: " + ammo.ToString();
    }

    // Get the amount of ammo for the display
    public int getAmmo()
    {
        return ammo;
    }

    // Update the ammo count
    public void setAmmo(int NewAmmo)
    {
        ammo = NewAmmo;
    }
}
