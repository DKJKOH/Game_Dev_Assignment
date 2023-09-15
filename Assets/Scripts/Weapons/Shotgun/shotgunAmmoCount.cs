using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgunAmmoCount : MonoBehaviour
{
    public Shotgun_NoHands currentShotgunScript;

    private int totalBullets;
    private int numberBulletsInMag;
    // Start is called before the first frame update
    void Start()
    {
        // Get bullet in magazine information and stuff
        totalBullets = currentShotgunScript.totalBullets;
        numberBulletsInMag = currentShotgunScript.numberBulletsInMag;
    }


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        // Get bullet in magazine information and stuff
        totalBullets = currentShotgunScript.totalBullets;
        numberBulletsInMag = currentShotgunScript.numberBulletsInMag;

        if (totalBullets <= 0 && numberBulletsInMag <= 0 )
        {
            // tell user is empty
            gameObject.GetComponent<TextMesh>().text = "No Bullets!";
        }
        else
        {
            // Update  ammo counter information
            gameObject.GetComponent<TextMesh>().text = "Ammo: " + numberBulletsInMag + " / " + totalBullets;
        }


    }
}
