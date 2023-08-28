using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class player_movement : MonoBehaviour
{
    // Create private rigid body for player
    private Rigidbody2D player_rb;

    // Set player's movement speed (By default is 3f)
    [SerializeField]
    float movementSpeed = 3f;

    // Player movement
    private Vector2 playerMovement;

    private void Start()
    {
        // Get player's rigid body
        player_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {

        // forward and backward movement
        float vertical = Input.GetAxisRaw("Vertical");
        // left and right movement
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Get the new player movement vector value
        playerMovement = new Vector2(horizontal, vertical);

        // Normalize vector so that horizontal and vertical transformation is equal
        playerMovement.Normalize();
    }

    private void FixedUpdate()
    {


        // If user does not want to move at all
        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            // Get player model current position
            Vector2 playerPosition = player_rb.position;

            // Set player's velocity, angular velocity and interia to zero (to prevent movement of playerawaw)
            player_rb.velocity = Vector2.zero;
            player_rb.angularVelocity = 0;
            player_rb.inertia = 0;

            // Set player to current position
            player_rb.position = playerPosition;
        }
        else
        {
            // Move player
            player_rb.velocity = playerMovement * movementSpeed;
        }
    }
}
