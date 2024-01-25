using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    // This part is for weapon animation, attach animator here
    [SerializeField]
    GameObject weapon;

    [SerializeField]
    // This part is for bullet spawn location, attach bullet spawner here
    GameObject bullet_spawner_object;

    [HideInInspector]
    public Animator animator;

    // What kind of bullet would spawn
    [SerializeField]
    public GameObject bullet;

    // Amount of bullets available in the weapon (Default 10)
    [SerializeField]
    public int magazineSize = 10;

    // How long would the thing wait before firing
    [SerializeField]
    public float timeBetweeenShots = 1;

    // Stores the time where the last shot was taken
    private float lastShotTime;

    // Stores information about current ammunition in magazine\
    [HideInInspector]
    public int numberBulletsInMag;

    // If reloading, prevent any other actions from happening
    private bool isReloading;

    // Total number of bullets allowed for this weapon
    [SerializeField]
    public int totalBullets;

    [SerializeField]
    public float reloadTime = 1;

    // Gun sounds
    public AudioSource audioSource;
    [SerializeField]
    public AudioClip shoot_sound;

    [SerializeField]
    public AudioClip release_slide_sound;

    [SerializeField]
    public AudioClip load_magazine_sound;

    [SerializeField]
    public AudioClip unload_magazine_sound;

    [SerializeField]
    public AudioClip drop_magazine_sound;

    [SerializeField]
    public AudioClip shell_eject_sound;

    [SerializeField]
    public AudioClip shell_bounce_sound;

    [SerializeField]
    public AudioClip dry_fire_sound;

    // Sound functions (Used in animation)
    void fire_sound()
    {
        audioSource.PlayOneShot(shoot_sound);
    }

    void Release_slide_sound()
    {
        audioSource.PlayOneShot(release_slide_sound);
    }

    void Load_magazine_sound()
    {
        audioSource.PlayOneShot(load_magazine_sound);
    }

    void Unload_magazine_sound()
    {
        audioSource.PlayOneShot(unload_magazine_sound);
    }

    void Drop_magazine_sound()
    {
        audioSource.PlayOneShot(drop_magazine_sound);
    }

    void Shell_eject_sound()
    {
        audioSource.PlayOneShot(shell_eject_sound);
    }

    void Shell_bounce_sound()
    {
        audioSource.PlayOneShot(shell_bounce_sound);
    }

    void Dry_fire_sound()
    {
        audioSource.PlayOneShot(dry_fire_sound);
    }

    /* This function adds a whole magazine of bullets to the gun, function can be found in pistol reload animation*/
    void add_magazine_bullet()
    {

        // Calculate the remaining amount of bullets
        totalBullets = numberBulletsInMag + totalBullets;

        // If bullets left is lesser than magazine size
        if (totalBullets <= magazineSize)
        {
            // Set magazine amount to amount of bullets left
            numberBulletsInMag = totalBullets;

            totalBullets = 0;
        }
        // Reload as per normal
        else
        { 
            // Set magazine amount to full
            numberBulletsInMag = magazineSize;

            totalBullets = totalBullets - magazineSize;
        }

        isReloading = false;
        // Disable reload animation
        animator.SetBool("reload", false);

    }

    /* This function decrements the current ammo in magazine, function can be found in pistol firing animation*/
    void shoot_bullet()
    {
        // If magazine is not empty
        if (numberBulletsInMag != 0)
        {
            // Remove 1 bullet from mag
            numberBulletsInMag--;

            // Create bullet object
            Instantiate(bullet, bullet_spawner_object.transform.position, bullet_spawner_object.transform.rotation);
        }
            
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

        // Initialize last shot time
        lastShotTime = 0f;

        // Retrieve animator component for user
        animator = weapon.GetComponent<Animator>();

        // Allows user to shoot first bullet immediately
        lastShotTime = -timeBetweeenShots;

        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Do not execute when game is pausedp
        if (Time.timeScale != 0)
        {
            // Enable shooting is weapon is not being reloaded
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                // Allow user to fire
                isReloading = false;
            }

            // Sound cue for empty mag (if fire is pressed)
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("empty") && Input.GetButtonDown("Fire1"))
            {
                // Start dry fire sound
                Dry_fire_sound();
            }

            // Fire weapon
            if (numberBulletsInMag > 1 && Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= timeBetweeenShots && !isReloading)
            {
                // Trigger firing animation
                animator.SetTrigger("shoot");

                // Save the last shot time
                lastShotTime = Time.time;
            }

            // Fire last shot
            if (numberBulletsInMag == 1 && Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= timeBetweeenShots && !isReloading)
            {

                // Trigger firing animation
                animator.SetTrigger("last_shot");

                // Save the last shot time
                lastShotTime = Time.time;
            }

            if (numberBulletsInMag < magazineSize && Input.GetKeyDown(KeyCode.R) && totalBullets > 0)
            {
                // Do not allow user to fire
                isReloading = true;

                // Set load bullet to be true
                animator.SetBool("reload", true);
            }
        }
    }
}
