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
    }

    private IEnumerator reload_with_delay()
    {
        // If magazine is not full
        while (currentAmmo < magazineSize)
        {

            // Load a slug animation
            animator.SetTrigger("load_bullet");

            // Add 1 bullet to magazine
            currentAmmo++;

            // Wait for a few seconds before reloading
            yield return new WaitForSeconds(reloadTime);
        }


    }

    // Update is called once per frame
    void Update()
    {
        // If the weapon is not being reloaded
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Shotgun"))
        {
            // Allow user to fire
            isReloading = false;
        }

        // If user presses fire during reload animation
        if (Input.GetButtonDown("Fire1") && isReloading)
        {
            // Stop reload coroutine
            StopCoroutine(reload_with_delay());
        }

        // If there are bullets in magazine and user fires
        if (currentAmmo > 0 && Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= timeBetweeenShots && !isReloading)
        {
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

            // Start reloading bullets (with delay)
            StartCoroutine(reload_with_delay());

        }

        // Check if round 
        if (currentAmmo == magazineSize && animator.GetCurrentAnimatorStateInfo(0).IsName("Reload_Bullet_Shotgun"))
        {
            // Chamber slug into 
            animator.SetTrigger("chamber_bullet_slug");
        }
        else if (currentAmmo != magazineSize && animator.GetCurrentAnimatorStateInfo(0).IsName("Reload_Bullet_Shotgun"))
        {
            // Go back to idle for reloading
            animator.SetTrigger("idle_shotgun");
        }

    }
}
