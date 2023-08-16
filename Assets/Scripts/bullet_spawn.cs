using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_spawn : MonoBehaviour
{
    public GameObject bullet;
    public int currentAmmo;  // Current available bullets
    public bool isAuto;
    private float lastShotTime; // Time when the last shot was fired
    public float fireRate; // Time between shots in full auto mode

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = 2;  // Initialize ammo
        lastShotTime = 0f; // Initialize last shot time

    }

    // Update is called once per frame
    void Update()
    {
        //======================== Single fire ============================
        //upon left click and if there is ammo
        if(currentAmmo > 0 && Input.GetButtonDown("Fire1") && !isAuto)
        {
            //Create bullet object
            Instantiate(bullet, transform.position, transform.rotation);
            currentAmmo--;
        }
        else 
        {
            // ======================== Auto weapon ============================
            // Left mouse button held down
            if(currentAmmo > 0 && Input.GetMouseButton(0) && isAuto && Time.time - lastShotTime >= fireRate)
            {
                //Create bullet object
                Instantiate(bullet, transform.position, transform.rotation);
                currentAmmo--;
                lastShotTime = Time.time;

            }
        }

        if(currentAmmo == 0)
        {
            Debug.Log("Ammo ran out!");
        }
       


        // ======================= burst fire weapon =========================

    }
}
