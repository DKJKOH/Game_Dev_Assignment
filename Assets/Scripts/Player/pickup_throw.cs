using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class pickup_throw : MonoBehaviour
{
    [SerializeField] GameObject Pickup_text;

    [SerializeField] GameObject Hand;

    GameObject weapon_to_pickup;

    bool holdingGun;
    void Start()
    {
        holdingGun = false;
    }

    void weaponDisable(Transform weapon_object, bool isDisable)
    {
        MonoBehaviour[] allScripts = weapon_object.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in allScripts)
        {
            if (script.name == "LaserSight")
            {
                // Enable laser sight
                script.enabled = true;
                continue;
            }

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

    // Update is called once per frame
    void Update()
    {
        // Ensures that the text is facing the correct direction so that it is readable
        Pickup_text.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        if (Hand.transform.childCount == 1 && Input.GetKey("q"))
        {
            GameObject weapon_to_drop = Hand.transform.GetChild(0).gameObject;

            if (weapon_to_drop.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle") || weapon_to_drop.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("empty"))
            {


                // Unset the hand as the weapon's parent
                weapon_to_drop.transform.parent = null;

                // Set sprite to dynamic
                weapon_to_drop.GetComponent<Rigidbody2D>().isKinematic = false;

                // Disable weapon rotation / shooting scripts
                weaponDisable(weapon_to_drop.transform, true);

                // Disable sprite animation
                weapon_to_drop.GetComponentInChildren<Animator>().enabled = false;
            }
            // Freeze transformation of weapon so that the weapon do not move away from hand
            weapon_to_pickup.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
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
                try
                {
                    // Enable sprite animation
                    weapon_to_pickup.GetComponentInChildren<Animator>().enabled = true;
                }
                catch
                {
                    
                }

                weaponDisable(weapon_to_pickup.transform, false);

                // Initially, grenade is not affected by physics 
                weapon_to_pickup.GetComponent<Rigidbody2D>().isKinematic = true;


                weapon_to_pickup.transform.position = Hand.transform.position;

                // Attach hand to weapon 
                weapon_to_pickup.transform.parent = Hand.transform;

                // Freeze transformation of weapon so that the weapon do not move away from hand
                weapon_to_pickup.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Pickup_text.SetActive(false);
    }
}
