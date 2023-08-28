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

    // Amount of bullets available in the weapon
    [SerializeField]
    public int magazineSize = 10;

    // How long would the thing wait before firing
    [SerializeField]
    public float timeBetweeenShots = 1;

    // Stores the time where the last shot was taken
    private float lastShotTime;

    // Stores information about current ammunition in magazine
    private int currentAmmo;

    // This part is to determine if the gun needs to be pumped after loading shell
    bool pumpShotgun;

    // If reloading, prevent any other actions from happening
    private bool isReloading;
    [SerializeField]
    public float reloadTime = 1;


    // Start is called before the first frame update
    void Start()
    {
        // Set current ammo
        currentAmmo = magazineSize;

        lastShotTime = 0f; // Initialize last shot time
        animator = weapon.GetComponent<Animator>();

        // So that user is able to shoot for the first time
        lastShotTime = -timeBetweeenShots;

        pumpShotgun = false;
    }

    // Function is being used in animation clip for shotgun (Reload_Bullet_Shotgun) clip
    void add_bullet()
    {
        currentAmmo++;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("insert_bullet"))
=======
        Debug.Log("Current amount of bullets:" + currentAmmo);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Reload_Bullet_Shotgun"))
>>>>>>> a3a878a99ad8161da9425afb2dbc2ff802f7fa82
        {
            isReloading = true;
        }
        else
        {
            isReloading = false;
        }
        
        if (currentAmmo == 0)
        {
            // Animation purposes where you would need to pump shotgun to load round into chamber
            pumpShotgun = true;

            // Automatically reload shotgun
            isReloading = true;

            // Stops reload animation
            animator.SetBool("load_bullet", true);
        }


        // If maximum amount of ammo in magazine size reached
        if (currentAmmo >= magazineSize)
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
        if (currentAmmo > 0 && Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= timeBetweeenShots && !isReloading)
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
            currentAmmo--;
        }

        if (currentAmmo < magazineSize && Input.GetKeyDown(KeyCode.R))
        {
            // Do not allow user to fire
            isReloading = true;

            // Set load bullet to be true
            animator.SetBool("load_bullet", true);
        }
    }
}
