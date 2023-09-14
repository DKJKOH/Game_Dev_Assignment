using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class player_movement : MonoBehaviour
{
    // Create private rigid body for player
    private Rigidbody2D player_rb;

    // Set player's walking speed (By default is 3f)
    [SerializeField]
    float walkingSpeed = 3f;


    // Set player's running speed (By default is 6f)
    [SerializeField]
    float runningSpeed = 6f;

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
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            // Player runs
            player_rb.velocity = playerMovement * runningSpeed;
        }
        else
        {
            // Player walks
            player_rb.velocity = playerMovement * walkingSpeed;
        }
    }
}
