﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class pickup_throw : MonoBehaviour
{
    [SerializeField] GameObject Pickup_text;
    [SerializeField] GameObject Drop_text;

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

            item_on_hand.transform.position = Hand.transform.position;

            // If user presses q (To drop item in hand)
            if (Input.GetKey("q"))
            {
                // Disable ammo count text for weapons held by player
                if (item_on_hand.name == "Pistol" || item_on_hand.name == "M4 Carbine" || item_on_hand.name == "Kar98K" || item_on_hand.name == "Shotgun")
                {
                    // Disable text object
                    item_on_hand.transform.GetChild(0).gameObject.SetActive(false);
                }


                if (item_on_hand.CompareTag("grenade"))
                {
                    // Unset the hand as the weapon's parent
                    item_on_hand.transform.parent = null;

                    // Change back to dynamic so that grenade do not "Float around"
                    item_on_hand.GetComponent<Rigidbody2D>().isKinematic = false;

                    // Disable grenade scripts
                    weaponDisable(item_on_hand.transform, true);

                    // Change the text
                    Drop_text.GetComponent<TextMesh>().text = item_on_hand.name + " dropped!";

                    // Enable text
                    Drop_text.SetActive(true);

                    // After a few seconds, disable text
                    StartCoroutine(disable_text());
                }
   


                if (item_on_hand.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle") || item_on_hand.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("empty"))
                {

                    // Unset the hand as the weapon's parent
                    item_on_hand.transform.parent = null;

                    // Set sprite to dynamic
                    //item_on_hand.GetComponent<Rigidbody2D>().isKinematic = false;

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
                }
                // Freeze transformation of weapon so that the weapon do not move away from hand
                //weapon_to_pickup.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
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

                weaponDisable(weapon_to_pickup.transform, false);

                // Initially, grenade is not affected by physics 
                //weapon_to_pickup.GetComponent<Rigidbody2D>().isKinematic = true;

                // Set weapon position to hand position
                weapon_to_pickup.transform.position = Hand.transform.position;

                // Set parent of weapon which is going to be picked up to be the hand
                weapon_to_pickup.transform.parent = Hand.transform;

                // Freeze transformation of weapon so that the weapon do not move away from hand
                //weapon_to_pickup.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Pickup_text.SetActive(false);
    }
}
