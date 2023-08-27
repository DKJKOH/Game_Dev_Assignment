﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade_throw : MonoBehaviour
{
    [SerializeField]
    float throw_force = 10f; // Force with which the grenade is thrown

    private Rigidbody2D grenade;
    Animator animator;

    private float start_time;
    private bool isStarted;

	// Time before grenade explodes
    [SerializeField]
    float explosion_time = 1; 

    // Delay time after explosion before destruction
    double delay_time = 1.6; 

    void Start()
    {
        grenade = GetComponent<Rigidbody2D>();
        // Initially, grenade is not affected by physics 
        grenade.isKinematic = true; 
        animator = GetComponent<Animator>();
        // Grenade hasn't been thrown yet
        isStarted = false; 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Grenade throwing process started
            isStarted = true; 
            start_time = Time.time;

            grenade.isKinematic = false; // Now, physics affect the grenade

            transform.parent = null; // Detach grenade from its parent

            // Apply force to simulate the throw
            grenade.AddForce(transform.up * throw_force * Time.deltaTime, ForceMode2D.Impulse);
        }

        // Debug.Log("Time.time: " + Time.time);
        // Debug.Log("Start.time: " + start_time);

        if (Time.time - start_time >= explosion_time && isStarted == true)
        {
        	// Stop grenade's movement
            grenade.velocity = new Vector3(0, 0, 0); 
            // Trigger explosion animation
            animator.SetTrigger("isExplode"); 

            // Wait for animation to finish before destroying the grenade
            if (Time.time - start_time >= delay_time)
            {
            	// Destroy the grenade game object
                Destroy(gameObject); 
            }
        }
    }
}
