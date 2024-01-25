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


    // Gun Sounds here!
    public AudioSource audioSource;

    [SerializeField]
    public AudioClip shoot_sound;

    [SerializeField]
    public AudioClip open_bolt_sound;

    [SerializeField]
    public AudioClip close_bolt_sound;

    [SerializeField]
    public AudioClip load_bullet_sound;

    [SerializeField]
    public AudioClip load_clip_sound;

    [SerializeField]
    public AudioClip reload_stripper_drop_sound;

    [SerializeField]
    public AudioClip dry_fire_sound;

    [SerializeField]
    public AudioClip shell_casing_sound;

    void Shoot_sound()
    {
        audioSource.PlayOneShot(shoot_sound);
    }
    void Open_bolt_sound()
    {
        audioSource.PlayOneShot(open_bolt_sound);
    }
    void Close_bolt_sound()
    {
        audioSource.PlayOneShot(close_bolt_sound);
    }
    void Load_bullet_sound()
    {
        audioSource.PlayOneShot(load_bullet_sound);
    }
    void Load_clip_sound()
    {
        audioSource.PlayOneShot(load_clip_sound);
    }

    void Reload_stripper_drop_sound()
    {
        audioSource.PlayOneShot(reload_stripper_drop_sound);
    }
    void Dry_fire_sound()
    {
        audioSource.PlayOneShot(dry_fire_sound);
    }

    void Shell_casing_sound()
    {
        audioSource.PlayOneShot(shell_casing_sound);
    }

    void add_bullet()
    {
        // Minus total ammo
        totalBullets--;

        // Add number of bullets
        numberBulletsInMag++;
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

        lastShotTime = 0f; // Initialize last shot time
        animator = weapon.GetComponent<Animator>();

        // So that user is able to shoot for the first time
        lastShotTime = -timeBetweeenShots;

        isReloading = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            // This portion is to stop reloading animation
            if (Input.GetButtonDown("Fire1") && numberBulletsInMag <= 0 && !isReloading)
            {
                // Start dry fire sound
                Dry_fire_sound();
            }


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

            if (Input.GetKeyDown(KeyCode.R) && totalBullets > 0)
            {
                // If user runs out of bullet (and has enough ammunition to reload a magazine)
                if (numberBulletsInMag == 0 && totalBullets >= magazineSize)
                {
                    // Do not allow user to fire
                    isReloading = true;

                    // Trigger load clip animation
                    animator.SetBool("load_clip", true);
                }

                else
                {
                    // Do not allow user to fire
                    isReloading = true;

                    // Set load bullet to be true
                    animator.SetBool("load_bullet", true);
                }
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
}
