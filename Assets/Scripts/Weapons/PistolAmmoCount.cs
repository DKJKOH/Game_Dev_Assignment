using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmoCount : MonoBehaviour
{
    public Pistol currentPistolScript;

    private int totalBullets;
    private int numberBulletsInMag;

    // Start is called before the first frame update
    void Start()
    {
        // Get bullet in magazine information and stuff
        totalBullets = currentPistolScript.totalBullets;
        numberBulletsInMag = currentPistolScript.numberBulletsInMag;
    }


    // Update is called once per frame
    void Update()
    {

        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        // Get bullet in magazine information and stuff
        totalBullets = currentPistolScript.totalBullets;
        numberBulletsInMag = currentPistolScript.numberBulletsInMag;

        // Check if no bullets
        if (totalBullets <= 0 && numberBulletsInMag <= 0)
        {
            // Tell user that there is no bullets in magazine
            gameObject.GetComponent<TextMesh>().text = "No Bullets!";
        }
        else
        {
            // Update ammo count information
            gameObject.GetComponent<TextMesh>().text = "Ammo: " + numberBulletsInMag + " / " + totalBullets;
        }
    }
}
