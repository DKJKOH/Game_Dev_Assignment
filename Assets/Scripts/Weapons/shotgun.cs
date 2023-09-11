using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : MonoBehaviour
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






    // Ammo counter stuff Ammo counter stuff Ammo counter stuff Ammo counter stuff


    // Stores information about current ammunition in magazine
    [HideInInspector]
    public int numberBulletsInMag;

    // Total number of bullets allowed for this weapon
    [SerializeField]
    public int totalBullets;

    // Amount of bullets available in the weapon
    [SerializeField]
    public int magazineSize = 10;



    // Ammo counter stuff Ammo counter stuff Ammo counter stuff Ammo counter stuff





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

        Debug.Log("Total bullets in mag: " + numberBulletsInMag + "Total Bullets: " + totalBullets);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Reload_Bullet_Shotgun"))
        {
            isReloading = true;
        }
        else
        {
            isReloading = false;
        }
        
        if (numberBulletsInMag == 0)
        {
            // Animation purposes where you would need to pump shotgun to load round into chamber
            pumpShotgun = true;

            // Automatically reload shotgun
            isReloading = true;

            // Stops reload animation
            animator.SetBool("load_bullet", true);
        }


        // If maximum amount of ammo in magazine size reached
        if (numberBulletsInMag >= magazineSize)
        {
            // Set reloading to false
            isReloading = false;

            // Stops reload animation
            animator.SetBool("load_bullet", false);

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
        if (Input.GetButtonDown("Fire1"))
        {
            // Stop reload coroutine
            animator.SetBool("load_bullet", false);
        }

        // If there are bullets in magazine and user fires
        if (numberBulletsInMag > 0 && Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= timeBetweeenShots && !isReloading)
        {
            // Set load bullet to be false
            animator.SetBool("load_bullet", false);

            // Trigger firing animation
            animator.SetTrigger("fire_shotgun");

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
    }
}
