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

    // Stores information about current ammunition in magazine
    private int currentAmmo;

    // If reloading, prevent any other actions from happening
    private bool isReloading;

    [SerializeField]
    public float reloadTime = 1;

    /* This function adds a whole magazine of bullets to the gun, function can be found in pistol reload animation*/
    void add_magazine_bullet()
    {
        currentAmmo = magazineSize;
    }

    /* This function decrements the current ammo in magazine, function can be found in pistol firing animation*/
    void shoot_bullet()
    {
        if (currentAmmo != 0)
        {
            currentAmmo--;

            // Create bullet object
            Instantiate(bullet, bullet_spawner_object.transform.position, bullet_spawner_object.transform.rotation);
        }
            
    }


    // Start is called before the first frame update
    void Start()
    {
        // Set current ammo
        currentAmmo = magazineSize;

        lastShotTime = 0f; // Initialize last shot time

        // Retrieve animator component for user
        animator = weapon.GetComponent<Animator>();

        // Allows user to shoot first bullet immediately
        lastShotTime = -timeBetweeenShots;

        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current amount of bullets:" + currentAmmo);

        // Enable shooting is weapon is not being reloaded
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            // Allow user to fire
            isReloading = false;
        }

        // Ensures that reload animation does not repeat twice
        if (currentAmmo >= magazineSize)
        {
            isReloading = false;
            // Disable reload animation
            animator.SetBool("reload", false);
        }

        // Fire weapon
        if (currentAmmo > 1 && Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= timeBetweeenShots && !isReloading)
        {
            // Trigger firing animation
            animator.SetTrigger("shoot");

            // Save the last shot time
            lastShotTime = Time.time;
        }

        // Fire last shot
        if (currentAmmo == 1 && Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= timeBetweeenShots && !isReloading)
        {

            // Trigger firing animation
            animator.SetTrigger("last_shot");

            // Save the last shot time
            lastShotTime = Time.time;
        }

        if (currentAmmo < magazineSize && Input.GetKeyDown(KeyCode.R))
        {
            // Do not allow user to fire
            isReloading = true;

            // Set load bullet to be true
            animator.SetBool("reload", true);
        }
    }


}
