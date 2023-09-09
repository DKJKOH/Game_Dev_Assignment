using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_collision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When bullet collides with objects with Collider2D shapes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // This is the game object that is hit by the bullet
        GameObject hitObject = collision.gameObject;

        Debug.Log(hitObject.name);

        // If bullet hits enemy
        if (hitObject.tag == "Enemy")
        {
            // Find script which makes enemy face the player (looks weird if enabled)
            Enemy_FOV script_to_disable = hitObject.GetComponent<Enemy_FOV>();

            // Actually disable the enemy rotation
            script_to_disable.enabled = false;

            // Start enemy death animation
            hitObject.GetComponent<Animator>().SetTrigger("die");
        }

        // Destroy self (aka bullet)
        Destroy(transform.gameObject);
    }
}
