using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class pickup_throw : MonoBehaviour
{
    // Gun Sounds here!
    private AudioSource audioSource;

    [SerializeField]
    public AudioClip drop_weapon_sound;

    [SerializeField]
    public AudioClip pickup_weapon_sound;



    [SerializeField] GameObject Pickup_text;
    [SerializeField] GameObject Drop_text;

    [SerializeField] GameObject Hand;

    GameObject weapon_to_pickup;

    bool holdingGun;

    // How fast does the weapon follow the user
    [SerializeField]
    public float weapon_follow_speed = 6.0f;


    void Drop_weapon_sound()
    {
        audioSource.PlayOneShot(drop_weapon_sound);
    }
    void Pickup_weapon_sound()
    {
        audioSource.PlayOneShot(pickup_weapon_sound);
    }

    void Start()
    {
        // Get audio source
        audioSource = gameObject.GetComponent<AudioSource>();

        holdingGun = false;

    }

    void weaponDisable(Transform weapon_object, bool isDisable)
    {
        MonoBehaviour[] allScripts = weapon_object.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in allScripts)
        {
            if (isDisable)
            {
                script.enabled = false;
            }
            else script.enabled = true;
        }

        // Recursively call the function for each child
        for (int i = 0; i < weapon_object.transform.childCount; i++)
        {
            weaponDisable(weapon_object.transform.GetChild(i), isDisable);
        }
    }

    /* This function will be used in conjunction with invoke so that it will take a few seconds to disable/enable text */
    IEnumerator disable_text()
    {
        yield return new WaitForSeconds(1);

        Drop_text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Ensures that the texts are facing the correct direction (upwards positive y direction)
        Pickup_text.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        Drop_text.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        if (Hand.transform.childCount == 1)
        {

            GameObject item_on_hand = Hand.transform.GetChild(0).gameObject;

            float distance_gun_hand = Vector3.Distance(item_on_hand.transform.position, Hand.transform.position);

            Vector3 smoothedPosition = Vector3.Lerp(item_on_hand.transform.position, Hand.transform.position, weapon_follow_speed * Time.deltaTime);

            item_on_hand.transform.position = smoothedPosition;

            if (distance_gun_hand > 1)
            {
                item_on_hand.transform.position = Hand.transform.position;
            }


            // If user presses q (To drop item in hand)
            if (Input.GetKey("q"))
            {


                // Disable ammo count text for weapons held by player
                if (item_on_hand.name == "Pistol" || item_on_hand.name == "M4 Carbine" || item_on_hand.name == "Kar98K" || item_on_hand.name == "Shotgun")
                {
                    if (item_on_hand.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle") || item_on_hand.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("empty"))
                    {
                        // Disable text object
                        item_on_hand.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }

 


                if (item_on_hand.CompareTag("grenade"))
                {
                    // Unset the hand as the weapon's parent
                    item_on_hand.transform.parent = null;

                    // Change back to dynamic so that grenade do not "Float around"
                    item_on_hand.GetComponent<Rigidbody2D>().isKinematic = false;

                    // So that player is able to pick the grenade up!
                    item_on_hand.GetComponent<BoxCollider2D>().enabled = true;

                    // Enable grenade scripts
                    weaponDisable(item_on_hand.transform, false);

                    // Enable grenade animation
                    item_on_hand.GetComponentInChildren<Animator>().enabled = true;

                    // Change the text
                    Drop_text.GetComponent<TextMesh>().text = item_on_hand.name + " dropped!";

                    // Enable text
                    Drop_text.SetActive(true);

                    // After a few seconds, disable text
                    StartCoroutine(disable_text());

                    // Drop weapon sound
                    audioSource.PlayOneShot(drop_weapon_sound);
                }
                else if (item_on_hand.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle") || item_on_hand.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("empty"))
                {

                    // Unset the hand as the weapon's parent
                    item_on_hand.transform.parent = null;

                    // Disable weapon rotation / shooting scripts
                    weaponDisable(item_on_hand.transform, true);

                    // Disable sprite animation
                    item_on_hand.GetComponentInChildren<Animator>().enabled = false;


                    // Change the text
                    Drop_text.GetComponent<TextMesh>().text = item_on_hand.name + " dropped!";

                    // Enable text
                    Drop_text.SetActive(true);

                    // After a few seconds, disable text
                    StartCoroutine(disable_text());

                    // Drop weapon sound
                    audioSource.PlayOneShot(drop_weapon_sound);
                }
            }

        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("gun")|| collision.gameObject.CompareTag("grabbable") || collision.gameObject.CompareTag("grenade")) && !holdingGun && Hand.transform.childCount < 1)
        {               
            // Retrieve details for weapon to pickup
            weapon_to_pickup = collision.gameObject;

            // Get weapon name
            string weapon_name = weapon_to_pickup.name;

            // Change the text
            Pickup_text.GetComponent<TextMesh>().text = "E - Pick up " + weapon_name;

            if (Hand.transform.childCount == 0)
            {
                Pickup_text.SetActive(true);
            }
            else
            {
                Pickup_text.SetActive(false);
            }


            if (Input.GetKey("e"))
            {

                audioSource.PlayOneShot(pickup_weapon_sound);

                if (weapon_to_pickup.CompareTag("grenade"))
                {

                    // Change back to dynamic so that grenade do not "Float around"
                    weapon_to_pickup.GetComponent<Rigidbody2D>().isKinematic = true;
                    weapon_to_pickup.GetComponent<BoxCollider2D>().enabled = false;

                }

                // If is pistol
                if (weapon_to_pickup.name == "Pistol" || weapon_to_pickup.name == "M4 Carbine" || weapon_to_pickup.name == "Kar98K" || weapon_to_pickup.name == "Shotgun")
                {
                    // Enable ammo count for pistol
                    weapon_to_pickup.transform.GetChild(0).gameObject.SetActive(true);
                }

                try
                {
                    // Enable sprite animation
                    weapon_to_pickup.GetComponentInChildren<Animator>().enabled = true;
                }
                catch
                {
                    
                }
                
                if (weapon_to_pickup.name == "Pistol")
                {
                    // Set weapon position to hand position
                    weapon_to_pickup.transform.rotation = Hand.transform.rotation;
                }
                weaponDisable(weapon_to_pickup.transform, false);

                // Set weapon position to hand position
                weapon_to_pickup.transform.position = Hand.transform.position;

                // Set parent of weapon which is going to be picked up to be the hand
                weapon_to_pickup.transform.parent = Hand.transform;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Pickup_text.SetActive(false);
    }
}
