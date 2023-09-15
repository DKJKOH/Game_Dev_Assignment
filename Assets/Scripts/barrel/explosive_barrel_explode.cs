using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class explosive_barrel_explode : MonoBehaviour
{
    [SerializeField]
    Animator grenadeAnimator;

    public float explosion_radius;

    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // This portion is to show the explosion radius (So that we can see what is happening)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosion_radius);
    }

    // Update is called once per frame
    void Update()
    {
        if (grenadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Done"))
        {
            Destroy(gameObject);
        }

        // If grenade is currently exploding
        if (!grenadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
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

                // If the grenade hits Player
                if (col.attachedRigidbody.tag == "Person")
                {
                    // Send to home screen
                    if (scene.name == "TutorialLevel")
                    {

                        // Link the gameover scene 
                        SceneManager.LoadScene("GameOverTutorial");
                    }
                    else
                    {
                        // Link the gameover scene 
                        SceneManager.LoadScene("GameOverMain");
                    }
                }
            }
        }
    }
}
