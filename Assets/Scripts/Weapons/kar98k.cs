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

    // Start is called before the first frame update
    void Start()
    {
        // Set current ammo
        currentAmmo = magazineSize;

        lastShotTime = 0f; // Initialize last shot time
        animator = weapon.GetComponent<Animator>();

        // So that user is able to shoot for the first time
        lastShotTime = -timeBetweeenShots;

        isReloading = false;
    }

    void add_bullet()
    {
        currentAmmo++;
    }

    void add_magazine_bullet()
    {
        currentAmmo = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current amount of bullets:" + currentAmmo);

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

        // If there are bullets in magazine and user fires
        if (currentAmmo > 0 && Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= timeBetweeenShots && !isReloading)
        {

            // Trigger firing animation
            animator.SetTrigger("shoot");

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

        // If user runs out of bullet
        if (currentAmmo == 0)
        {
            // Trigger load clip animation
            animator.SetBool("load_clip", true);
        }


        // If maximum amount of ammo in magazine size reached
        if (currentAmmo >= magazineSize)
        {
            // Set reloading to false
            isReloading = false;

            // Stops reload animation
            animator.SetBool("load_bullet", false);
        }
    }
}
