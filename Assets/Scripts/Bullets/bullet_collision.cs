﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bullet_collision : MonoBehaviour
{
    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
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

        // If bullet hits gun
        if (hitObject.tag == "Person")
        {

            // Pause game
            Time.timeScale = 0f;

            if( scene.name == "TutorialLevel")
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

        if(hitObject.tag == "gun")
        {
             // Destroy gun
            Destroy(hitObject);
        }

        // If bullet hits grenade
        if (hitObject.tag == "grenade" || hitObject.tag == "explosive_barrel")
        {
            // Grenade Explosion Animation
            hitObject.GetComponent<Animator>().SetTrigger("isExplode");

            // Enable Explosion Collider
            hitObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        // Destroy self (aka bullet)
        Destroy(transform.gameObject);
    }
}

