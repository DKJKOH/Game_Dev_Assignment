using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_animation : MonoBehaviour
{
    // Controls player walking / running / idle animation
    private Animator player_animator_controller;

    // Triggers running animation
    private bool running;


    // Thingy which will emit sound
    private AudioSource audioSource;

    // To simulate walking and running
    [SerializeField]
    public AudioClip walk_sound;

    [SerializeField]
    public AudioClip run_sound;

    // These functions will execute in the animation itself
    void Walk_sound()
    {
        audioSource.PlayOneShot(walk_sound);
    }
    void Run_sound()
    {
        audioSource.PlayOneShot(run_sound);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get audio source
        audioSource = gameObject.GetComponent<AudioSource>();

        // By default, disable running
        running = false;
        // Retrieve player's player controller animator
        player_animator_controller = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get user's horizontal / vertical input
        int horizontalInput = (int)Input.GetAxisRaw("Horizontal");
        int verticalInput = (int)Input.GetAxisRaw("Vertical");

        // If user presses shift to run
        if (Input.GetButton("Shift"))
        {
            // Set running to true
            running = true;
        }
        else
        {
            // Set running to false
            running = false;
        }

        // Not moving
        if (horizontalInput == 0 && verticalInput == 0)
        {
            // Set walking to true
            player_animator_controller.SetBool("walking", false);
            player_animator_controller.SetBool("running", false);
        }
        else
        {
            // Trigger running animation
            if (running)
            {
                // Set running to true
                player_animator_controller.SetBool("running", true);
                player_animator_controller.SetBool("walking", false);
            }
            // Trigger walking animation
            else
            {
                // Set walking to true
                player_animator_controller.SetBool("walking", true);
                player_animator_controller.SetBool("running", false);
            }
        }

    }
}
