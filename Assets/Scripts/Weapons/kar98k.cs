using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kar98k : MonoBehaviour
{
    // This part is for weapon animation, attach animator here
    [SerializeField]
    GameObject weapon;

    [SerializeField]
    // This part is for bullet spawn location, attach bullet spawner here
    GameObject bullet_spawner_object;

    Animator animator;

    // What kind of bullet would spawn
    [SerializeField]
    public GameObject bullet;

    // How long would the thing wait before firing
    [SerializeField]
    public float timeBetweeenShots = 1;

    // Stores the time where the last shot was taken
    private float lastShotTime;

    // If reloading, prevent any other actions from happening
    private bool isReloading;

    [SerializeField]
    public float reloadTime = 1;



    // Ammo stuff
    // Stores information about current ammunition in magazine
    //public int currentAmmo;
    // Amount of bullets available in the weapon (Default 10)
    [SerializeField]
    public int magazineSize = 10;
    // Stores information about current ammunition in magazine\
    [HideInInspector]
    public int numberBulletsInMag;
    // Total number of bullets allowed for this weapon
    [SerializeField]
    public int totalBullets;

    void add_bullet()
    {
        //// If there is ammo and magazine is not full
        //if (totalBullets > 0 && numberBulletsInMag <= magazineSize)
        //{
        // Minus total ammo
        totalBullets--;
            // Add number of bullets
        numberBulletsInMag++;
        //}
    }

    void add_magazine_bullet()
    {
        // Minus the magazine size
        totalBullets = totalBullets - magazineSize;

        // Restet number of ammunitions in mag
        numberBulletsInMag = magazineSize;
    }

    // Start is called before the first frame update
    void Start()
    {
        // If number of bullets is lesser than magazine capacity
        if (totalBullets <= magazineSize)
        {

            // Set current ammo
            numberBulletsInMag = totalBullets;

            totalBullets = 0;
        }
        else
        {
            // Set current ammo
            numberBulletsInMag = magazineSize;

            // Number of bullets in magazine
            totalBullets = totalBullets - magazineSize;
        }

        // Set current ammo
        //totalBullets = magazineSize;

        lastShotTime = 0f; // Initialize last shot time
        animator = weapon.GetComponent<Animator>();

        // So that user is able to shoot for the first time
        lastShotTime = -timeBetweeenShots;

        isReloading = false;
    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log("Total bullets in mag: " + numberBulletsInMag + "Total Bullets: " + totalBullets);

        // Disable 
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            isReloading = false;

            // Trigger load clip animation
            animator.SetBool("load_clip", false);
        }
        else
        {
            isReloading = true;
        }

        // This portion is to stop reloading animation
        if (Input.GetButtonDown("Fire1"))
        {

            // Stop reload coroutine
            animator.SetBool("load_bullet", false);
        }

        // If user fires (magazine is not empty)
        if (numberBulletsInMag > 0 && Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= timeBetweeenShots && !isReloading)
        {

            // Trigger firing animation
            animator.SetTrigger("shoot");

            // Save the last shot time
            lastShotTime = Time.time;

            //Create bullet object
            Instantiate(bullet, bullet_spawner_object.transform.position, bullet_spawner_object.transform.rotation);

            // Decrease current ammo in clip
            numberBulletsInMag--;
        }

        if (numberBulletsInMag < magazineSize && Input.GetKeyDown(KeyCode.R))
        {
            // Do not allow user to fire
            isReloading = true;

            // Set load bullet to be true
            animator.SetBool("load_bullet", true);
        }

        // If user runs out of bullet (and has enough ammunition to reload a magazine)
        if (numberBulletsInMag == 0 && totalBullets >= magazineSize)
        {
            // Trigger load clip animation
            animator.SetBool("load_clip", true);
        }
        else if (numberBulletsInMag == 0 && totalBullets < magazineSize)
        {
            // Do not allow user to fire
            isReloading = true;

            // Set load bullet to be true
            animator.SetBool("load_bullet", true);
        }


        // If maximum amount of ammo in magazine size reached
        if (numberBulletsInMag >= magazineSize)
        {
            // Set reloading to false
            isReloading = false;

            // Stops reload animation
            animator.SetBool("load_bullet", false);
        }

        if (totalBullets <= 0)
        {
            // Set reloading to false
            isReloading = false;
            // Stops reload animation
            animator.SetBool("load_bullet", false);
        }
    }
}
