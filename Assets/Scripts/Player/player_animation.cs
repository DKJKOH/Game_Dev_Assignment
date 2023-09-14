using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_animation : MonoBehaviour
{
    private Animator player_animator_controller;

    [SerializeField]
    public GameObject hand;

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve player's player controller animator
        player_animator_controller = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int horizontalInput = (int)Input.GetAxisRaw("Horizontal");
        int verticalInput = (int)Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            // If condition to run
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                // Set running to true
                player_animator_controller.SetBool("running", true);
                player_animator_controller.SetBool("walking", false);
            }
            else
            {
                // Set walking to true
                player_animator_controller.SetBool("walking", true);
                player_animator_controller.SetBool("running", false);
            }
        }
        else
        {
            player_animator_controller.SetBool("running", false);
            player_animator_controller.SetBool("walking", false);
        }
    }
}
