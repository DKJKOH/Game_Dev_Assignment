using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    Animator grenadeAnimator;

    public float explosion_radius;
    // This portion is to show the explosion radius (So that we can see what is happening)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosion_radius);
    }

    // Update is called once per frame
    void Update()
    {
        // If grenade is currently exploding
        if (!grenadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("grenade_idle"))
        {
            // Get all objects hit by the circle2d collider
            Collider2D[] enemyHit = Physics2D.OverlapCircleAll(transform.position, explosion_radius);

            // Iterate through all collisions 
            foreach (Collider2D col in enemyHit)
            {                       
                // If the grenade hits enemy
                if (col.attachedRigidbody.tag == "Enemy")
                {
                    // Start enemy death animation
                    col.gameObject.GetComponent<Animator>().SetTrigger("die");

                    col.gameObject.GetComponent<Enemy_FOV>().enabled = false;
                }
            }
        }
    }
}
