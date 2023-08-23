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
        ammoDisplay.text = "Ammo Left: " + ammo.ToString();
    }

    public int getAmmo()
    {
        return ammo;
    }

    public void setAmmo(int NewAmmo)
    {
        ammo = NewAmmo;
    }
}
