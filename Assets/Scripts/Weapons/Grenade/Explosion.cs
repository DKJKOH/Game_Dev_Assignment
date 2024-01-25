using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    Animator grenadeAnimator;

    public AudioClip grenade_explode_sound;

    public float explosion_radius;
    // This portion is to show the explosion radius (So that we can see what is happening)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosion_radius);
    }

    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
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
                if (col.tag == "Enemy")
                {
                    // Start enemy death animation
                    col.gameObject.GetComponent<Animator>().SetTrigger("die");
                    
                    // If the grenade hits enemy
                    col.gameObject.GetComponent<Enemy_FOV>().enabled = false;
                }     
                else if (col.tag == "Person")
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
                else if (col.tag == "grenade" || col.tag == "explosive_barrel")
                {
                    if (gameObject.transform.childCount > 0)
                    {
                        // Enable Explosion Collider
                        col.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    // Grenade Explosion Animation
                    col.GetComponent<Animator>().SetTrigger("isExplode");


                }

            }
        }
    }
}
