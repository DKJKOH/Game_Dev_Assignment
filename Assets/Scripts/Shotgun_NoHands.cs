using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_NoHands : MonoBehaviour
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



    // This part is to determine if the gun needs to be pumped after loading shell
    bool pumpShotgun;

    // If reloading, prevent any other actions from happening
    private bool isReloading;
    [SerializeField]
    public float reloadTime = 1;








    // Ammo count stuff


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


    // Shotgun sound stuff
    // Gun sounds
    private AudioSource audioSource;

    [SerializeField]
    public AudioClip shoot_sound;

    [SerializeField]
    public AudioClip insert_shell_sound;


    [SerializeField]
    public AudioClip pump_back_sound;

    [SerializeField]
    public AudioClip pump_forward_sound;

    [SerializeField]
    public AudioClip shell_drop_sound;

    [SerializeField]
    public AudioClip dry_fire_sound;


    void Shoot_sound()
    {
        audioSource.PlayOneShot(shoot_sound);
    }

    void Insert_shell_sound()
    {
        audioSource.PlayOneShot(insert_shell_sound);
    }
    void Pump_back_sound()
    {
        audioSource.PlayOneShot(pump_back_sound);
    }
    void Pump_forward_sound()
    {
        audioSource.PlayOneShot(pump_forward_sound);
    }

    void Shell_drop_sound()
    {
        audioSource.PlayOneShot(shell_drop_sound);
    }

    void Dry_fire_sound()
    {
        audioSource.PlayOneShot(dry_fire_sound);
    }



    // Start is called before the first frame update
    void Start()
    {

        // Find audio listener (for weapon sounds)
        audioSource = gameObject.GetComponent<AudioSource>();

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

        pumpShotgun = false;
    }

    // Function is being used in animation clip for shotgun (Reload_Bullet_Shotgun) clip
    void add_bullet()
    {
        // Minus total ammo
        totalBullets--;
        // Add number of bullets
        numberBulletsInMag++;
    }

    // Update is called once per frame
    void Update()
    {

        // Sound cue for no bullets in magazine
        if (numberBulletsInMag <= 0 && Input.GetButtonDown("Fire1") && !isReloading)
        {
            // Start dry fire sound
            Dry_fire_sound();
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("insert_bullet"))
        {
            isReloading = true;
        }
        else
        {
            isReloading = false;
        }

        if (numberBulletsInMag == 0 && totalBullets > 0 && Input.GetKeyDown(KeyCode.R))
        {
            // Animation purposes where you would need to pump shotgun to load round into chamber
            pumpShotgun = true;

            // Automatically reload shotgun
            isReloading = true;

            // Starts reload animation
            animator.SetBool("load_slug", true);
        }


        // If maximum amount of ammo in magazine size reached
        if (numberBulletsInMag >= magazineSize || totalBullets <= 0)
        {
            // Set reloading to false
            isReloading = false;

            // Stops reload animation
            animator.SetBool("load_slug", false);

            // If number of bullets in magazine is full and it is during the reloading phase
            if (pumpShotgun)
            {
                // Set pumpshotgun to false as you would not need to pump shotgun
                pumpShotgun = false;

                // Trigger Insert_Last_bullet
                animator.SetTrigger("load_last_bullet");
            }
        }


        // If user presses fire during reload animation
        if (Input.GetButtonDown("Fire1") && isReloading)
        {
            // Stop reload coroutine
            animator.SetBool("load_slug", false);

            if (pumpShotgun)
            {
                // Prevent pump shotgun from happening again
                pumpShotgun = false;

                // Trigger Insert_Last_bullet
                animator.SetTrigger("load_last_bullet");
            }
        }

        // If there are bullets in magazine and user fires
        if (numberBulletsInMag > 0 && Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= timeBetweeenShots && !isReloading)
        {
            // Set load bullet to be false
            animator.SetBool("load_slug", false);

            // Trigger firing animation
            animator.SetTrigger("fire");

            // Save the last shot time
            lastShotTime = Time.time;

            //Create bullet object
            Instantiate(bullet, bullet_spawner_object.transform.position, bullet_spawner_object.transform.rotation);

            // Decrease current ammo in clip
            numberBulletsInMag--;
        }

        if (numberBulletsInMag < magazineSize && Input.GetKeyDown(KeyCode.R) && totalBullets > 0)
        {
            // Do not allow user to fire
            isReloading = true;

            // Set load bullet to be true
            animator.SetBool("load_slug", true);
        }
    }
}
