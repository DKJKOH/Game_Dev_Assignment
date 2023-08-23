using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public bool isAutomaticRifle = false;


    // How long would the thing wait before firing
    [SerializeField]
    public float timeBetweeenShots = 10;

    // Stores the time where the last shot was taken
    private float lastShotTime;

    private int currentAmmo;

    // If reloading, prevent any other actions from happening
    private bool isReloading;

    // this function will fully execute once the reload animation is finished
    IEnumerator WaitForReload()
    {
        // Wait until reload state has been completed
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Reload_"))
        {
            Debug.Log("Still Reloading!");
            // Return null for the moment
            yield return null;
        }
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
        if (isAutomaticRifle)
        {

            // If the weapon is not reloading
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_AutomaticRifle"))
            {
                // Allow user to fire rifle
                isReloading = false;
            }

            // Retrieve sprites for enabling and disabling for animations (Reloading, Shooting and Idle)
            GameObject Weapon_Single_Shot_Object = weapon.transform.GetChild(1).gameObject;
            GameObject Weapon_Reload_Object = weapon.transform.GetChild(2).gameObject;
            GameObject Weapon_Idle_Object = weapon.transform.GetChild(3).gameObject;
            GameObject Weapon_Last_Shot_Object = weapon.transform.GetChild(4).gameObject;
            GameObject Weapon_Release_Catch_Object = weapon.transform.GetChild(5).gameObject;
            GameObject Weapon_Chamber_Round_Object = weapon.transform.GetChild(6).gameObject;

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("release_catch_AutomaticRifle"))
            {
                // Disable Weapon Idle
                Weapon_Idle_Object.SetActive(false);

                // Disable Weapon Reload
                Weapon_Reload_Object.SetActive(false);

                // Disable last shot sprite
                Weapon_Last_Shot_Object.SetActive(false);

                // Disable Weapon Firing
                Weapon_Single_Shot_Object.SetActive(false);

                // Enable weapon catch
                Weapon_Release_Catch_Object.SetActive(true);

                // Disable Chamber round
                Weapon_Chamber_Round_Object.SetActive(false);
            }


            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Chamber_Round_Automatic_Rifle"))
            {
                // Disable Weapon Idle
                Weapon_Idle_Object.SetActive(false);

                // Disable Weapon Reload
                Weapon_Reload_Object.SetActive(false);

                // Disable last shot sprite
                Weapon_Last_Shot_Object.SetActive(false);

                // Disable Weapon Firing
                Weapon_Single_Shot_Object.SetActive(false);

                // Enable weapon catch
                Weapon_Release_Catch_Object.SetActive(false);

                // Disable Chamber round
                Weapon_Chamber_Round_Object.SetActive(true);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Reload_AutomaticRifle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Reload_Empty_Automatic_Rifle"))
            {
                // Disable Weapon Idle
                Weapon_Idle_Object.SetActive(false);

                // Disable Weapon Reload
                Weapon_Reload_Object.SetActive(true);

                // Disable last shot sprite
                Weapon_Last_Shot_Object.SetActive(false);

                // Disable Weapon Firing
                Weapon_Single_Shot_Object.SetActive(false);

                // Enable weapon catch
                Weapon_Release_Catch_Object.SetActive(false);

                // Disable Chamber round
                Weapon_Chamber_Round_Object.SetActive(false);
            }



            // Set is reloading is false on rifle
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Empty_AutomaticRifle"))
            {
                // Disable Weapon Idle
                Weapon_Idle_Object.SetActive(false);

                // Disable Weapon Reload
                Weapon_Reload_Object.SetActive(false);

                // Enable last shot sprite
                Weapon_Last_Shot_Object.SetActive(true);

                // Disable Weapon Firing
                Weapon_Single_Shot_Object.SetActive(false);

                // Disable weapon catch
                Weapon_Release_Catch_Object.SetActive(false);

                // Disable Chamber round
                Weapon_Chamber_Round_Object.SetActive(false);
            }

            // Set is reloading is false on rifle
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Last_Shot_AutomaticRifle"))
            {
                // Disable Weapon Idle
                Weapon_Idle_Object.SetActive(false);

                // Disable Weapon Reload
                Weapon_Reload_Object.SetActive(false);

                // Enable last shot sprite
                Weapon_Last_Shot_Object.SetActive(true);

                // Disable Weapon Firing
                Weapon_Single_Shot_Object.SetActive(false);

                // Disable weapon catch
                Weapon_Release_Catch_Object.SetActive(false);

                // Disable Chamber round
                Weapon_Chamber_Round_Object.SetActive(false);
            }

            // ======================== Auto weapon ============================
            // If user holds down left mouse button on auto weapons
            if (Time.time - lastShotTime >= timeBetweeenShots)
            {

                // If user presses down button
                if (Input.GetMouseButton(0) && currentAmmo > 1 && !isReloading)
                {
                    // Disable Weapon Idle
                    Weapon_Idle_Object.SetActive(false);

                    // Disable Weapon Reload
                    Weapon_Reload_Object.SetActive(false);

                    // Enable last shot sprite
                    Weapon_Last_Shot_Object.SetActive(false);

                    // Enable Weapon Firing
                    Weapon_Single_Shot_Object.SetActive(true);

                    // Disable weapon catch
                    Weapon_Release_Catch_Object.SetActive(false);

                    // Disable Chamber round
                    Weapon_Chamber_Round_Object.SetActive(false);

                    // Run animation
                    // Animation, set is firing to true
                    animator.SetTrigger("rifle_shoot");

                    //Create bullet object
                    Instantiate(bullet, transform.position, transform.rotation);

                    // Deduct ammo
                    currentAmmo--;

                    // Save the last shot time
                    lastShotTime = Time.time;
                }
                else if (Input.GetMouseButton(0) && currentAmmo == 1 && !isReloading)
                {

                    // Run last shot animation
                    animator.SetTrigger("rifle_last_shot");

                    //Create bullet object
                    Instantiate(bullet, transform.position, transform.rotation);

                    // Deduct ammo
                    currentAmmo--;

                    // Save the last shot time
                    lastShotTime = Time.time;
                }
                // Reload empty weapon
                else if (Input.GetKeyDown(KeyCode.R) && currentAmmo == 0)
                {
                    // Do not allow user to fire
                    isReloading = true;
                    // activate last catch and reload
                    animator.SetTrigger("rifle_reload_empty");

                    // Actual code to refill ammo
                    currentAmmo = magazineSize;

                }
                // If user presses R and is automatic rifle
                else if (Input.GetKeyDown(KeyCode.R) && currentAmmo != magazineSize)
                {
                    isReloading = true;

                    // Set the current ammo as magazine size
                    animator.SetTrigger("rifle_reload");

                    // Waits for reloading animation to be fully complete
                    StartCoroutine(WaitForReload());

                    // Actual code to refill ammo
                    currentAmmo = magazineSize;
                }
            }

        }


        
        //======================== Pistol ============================
        if (!isAutomaticRifle)
        {
            // Set is reloading is fale on pistol
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Pistol"))
            {
                // Allow user to fire pistol
                isReloading = false;
            }
            // Retrieve sprites for enabling and disabling for animations (Reloading, Shooting and Idle)
            GameObject Pistol_Last_Shot_Sprite = weapon.transform.GetChild(1).gameObject;
            SpriteRenderer Pistol_Normal_Sprite = weapon.GetComponent<SpriteRenderer>();


            //upon left click and if there is ammo
            if (currentAmmo > 1 && Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= timeBetweeenShots && !isReloading)
            {
                // Animation, set is firing to true
                animator.SetTrigger("pistol_shoot");

                currentAmmo--;

                // Save the last shot time
                lastShotTime = Time.time;

                //Create bullet object
                Instantiate(bullet, transform.position, transform.rotation);

            }
            // If the gun is shooting the last ammo
            else if (currentAmmo == 1 && Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= timeBetweeenShots && !isReloading)
            {
                // Disable normal pistol firing sprite
                Pistol_Normal_Sprite.enabled = false;
                // Enable last shot pistol sprite
                Pistol_Last_Shot_Sprite.SetActive(true);

                // Animation, set is firing to true
                animator.SetTrigger("pistol_last_shot");

                currentAmmo--;

                // Save the last shot time
                lastShotTime = Time.time;

                //Create bullet object
                Instantiate(bullet, transform.position, transform.rotation);

            }
            // If gun currently is empty, use the load empty weapon animation
            else if (Input.GetKeyDown(KeyCode.R) && currentAmmo == 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Pistol_Empty"))
            {
                // Enable normal pistol sprite for reloading
                Pistol_Normal_Sprite.enabled = true;
                // Disable last shot pistol sprite
                Pistol_Last_Shot_Sprite.SetActive(false);

                // Ensure that players are not able to fire when reloading
                isReloading = true;

                // Trigger the load empty weapon animation
                animator.SetTrigger("pistol_load_empty_chamber");

                // Actual code to refill ammo
                currentAmmo = magazineSize;
            }
            else if (Input.GetKeyDown(KeyCode.R) && currentAmmo != magazineSize && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Pistol"))
            {
                isReloading = true;

                // Set the current ammo as magazine size
                animator.SetTrigger("pistol_reload");

                // Actual code to refill ammo
                currentAmmo = magazineSize;
            }
        }
    }
}
