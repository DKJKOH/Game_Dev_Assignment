using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Throw : MonoBehaviour
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
            Debug.Log(script.name);
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

        Debug.Log(Hand.transform.childCount);


        if (Hand.transform.childCount == 1 && Input.GetKey("q"))
        {
            GameObject weapon_to_drop = Hand.transform.GetChild(0).gameObject;

            if (weapon_to_drop.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                weaponDisable(weapon_to_drop.transform, true);

                weapon_to_drop.transform.parent = null;


                weapon_to_drop.GetComponent<Rigidbody2D>().isKinematic = false;

                weapon_to_drop.GetComponentInChildren<Animator>().enabled = true;
            }
            else if (weapon_to_drop.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("empty"))
            {
                weaponDisable(weapon_to_drop.transform, true);

                weapon_to_drop.transform.parent = null;


                weapon_to_drop.GetComponent<Rigidbody2D>().isKinematic = false;

                weapon_to_drop.GetComponentInChildren<Animator>().enabled = true;
            }

        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("gun") || collision.gameObject.CompareTag("grenade")) && !holdingGun && Hand.transform.childCount < 1)
        {
            // Retrieve details for weapon to pickup
            weapon_to_pickup = collision.gameObject;

            Pickup_text.SetActive(true);

            if (Input.GetKey("e"))
            {
                weapon_to_pickup.GetComponentInChildren<Animator>().enabled = true;

                weaponDisable(weapon_to_pickup.transform, false);

                // Initially, grenade is not affected by physics 
                weapon_to_pickup.GetComponent<Rigidbody2D>().isKinematic = true;


                weapon_to_pickup.transform.position = Hand.transform.position;

                // Attach hand to weapon 
                weapon_to_pickup.transform.parent = Hand.transform;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Pickup_text.SetActive(false);
    }
}
