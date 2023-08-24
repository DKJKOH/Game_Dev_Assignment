using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_spawn : MonoBehaviour
{
    // What kind of bullet would spawn
    [SerializeField] 
    public GameObject bullet;

    // Sound effect of the gunshot
    private AudioSource audioSource;
    public AudioClip gunshotSound;


    // Amount of bullets available in the weapon
    [SerializeField]
    public int currentAmmo = 10;

    // Automatic fire or Single Fire for gun
    [SerializeField]
    public bool isAuto = false;


    // How long would the thing wait before firing
    [SerializeField]
    public float timeBetweeenShots = 10;

    // Stores the time where the last shot was taken
    private float lastShotTime;

    // Start is called before the first frame update
    void Start()
    {
        lastShotTime = 0f; // Initialize last shot time
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //======================== Single fire ============================
        //upon left click and if there is ammo
        if(currentAmmo > 0 && Input.GetButtonDown("Fire1") && !isAuto && Time.time - lastShotTime >= timeBetweeenShots)
        {
            //Create bullet object
            Instantiate(bullet, transform.position, transform.rotation);
            audioSource.PlayOneShot(gunshotSound);
            currentAmmo--;

            // Save the last shot time
            lastShotTime = Time.time;
        }
        else 
        {
            // ======================== Auto weapon ============================
            // If user holds down left mouse button on auto weapons
            if(currentAmmo > 0 && Input.GetMouseButton(0) && isAuto && Time.time - lastShotTime >= timeBetweeenShots)
            {
                //Create bullet object
                Instantiate(bullet, transform.position, transform.rotation);
                Debug.Log("Playing gunshot sound!");
                audioSource.PlayOneShot(gunshotSound);

                // Deduct ammo
                currentAmmo--;

                // Save the last shot time
                lastShotTime = Time.time;
            }
        }

        // If Run Out of ammo
        if(currentAmmo == 0)
        {
            // Inform player ammo ran out
            Debug.Log("Ammo ran out!");
        }
        // ======================= burst fire weapon =========================

    }
}
