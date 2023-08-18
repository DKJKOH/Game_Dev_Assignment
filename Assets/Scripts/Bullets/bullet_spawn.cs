using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_spawn : MonoBehaviour
{
    // This part is for weapon animation, attach animator here
    [SerializeField]
    GameObject weapon;

    Animator animator;

    // What kind of bullet would spawn
    [SerializeField] 
    public GameObject bullet;

    // Amount of bullets available in the weapon
    [SerializeField]
    public int magazineSize = 10;

    // Automatic fire or Single Fire for gun
    [SerializeField]
    public bool isAuto = false;


    // How long would the thing wait before firing
    [SerializeField]
    public float timeBetweeenShots = 10;

    // Stores the time where the last shot was taken
    private float lastShotTime;

    private int currentAmmo;

    // this function will fully execute once the reload animation is finished
    IEnumerator WaitForReload()
    {
        // Wait until reload state has been completed
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Reload_AutomaticRifle"))
        {
            Debug.Log("Still Reloading!");
            // Return null for the moment
            yield return null;
        }

        Debug.Log("Apple");
    }



    // Start is called before the first frame update
    void Start()
    {
        // Set current ammo
        currentAmmo = magazineSize;

        lastShotTime = 0f; // Initialize last shot time
        animator = weapon.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //======================== Single fire ============================
        //upon left click and if there is ammo
        if(currentAmmo > 0 && Input.GetButtonDown("Fire1") && !isAuto && Time.time - lastShotTime >= timeBetweeenShots)
        {
            // Animation, set is firing to true
            animator.SetTrigger("pistol_shoot");

            currentAmmo--;

            // Save the last shot time
            lastShotTime = Time.time;

            //Create bullet object
            Instantiate(bullet, transform.position, transform.rotation);


  
        }
        // ======================== Auto weapon ============================
        // If user holds down left mouse button on auto weapons
        else if (isAuto && Time.time - lastShotTime >= timeBetweeenShots)
        {
            GameObject Weapon_Single_Shot_Object = weapon.transform.GetChild(1).gameObject;
            GameObject Weapon_Reload_Object = weapon.transform.GetChild(2).gameObject;
            GameObject Weapon_Idle_Object = weapon.transform.GetChild(3).gameObject;

            // If user presses down button
            if (Input.GetMouseButton(0) && currentAmmo > 0)
            {
                // Disable Weapon Idle
                Weapon_Idle_Object.SetActive(false);

                // Disable Weapon Reload
                Weapon_Reload_Object.SetActive(false);

                // Enable Weapon Firing
                Weapon_Single_Shot_Object.SetActive(true);



                // Run animation
                // Animation, set is firing to true
                animator.SetTrigger("rifle_shoot");

                //Create bullet object
                Instantiate(bullet, transform.position, transform.rotation);

                // Deduct ammo
                currentAmmo--;

                // Save the last shot time
                lastShotTime = Time.time;

                WaitForReload();
            }
            // If user presses R and is automatic rifle
            else if (Input.GetKeyDown(KeyCode.R) && currentAmmo != magazineSize)
            {
                // Disable Weapon Idle
                Weapon_Idle_Object.SetActive(false);

                // Disable Weapon Reload
                Weapon_Single_Shot_Object.SetActive(false);

                // Enable Reload
                Weapon_Reload_Object.SetActive(true);

                // Set the current ammo as magazine size
                animator.SetTrigger("rifle_reload");

                // Waits for reloading animation to be fully complete
                StartCoroutine(WaitForReload());

                // Actual code to refill ammo
                currentAmmo = magazineSize;
            }

        }



        // If Run Out of ammo
        if (currentAmmo == 0)
        {
            // Inform player ammo ran out
            Debug.Log("Ammo ran out!");
        }
        // ======================= burst fire weapon =========================

    }
}
