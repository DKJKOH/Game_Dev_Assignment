using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class player_controls : MonoBehaviour
{
    // Create private rigid body for player
    private Rigidbody2D player_rb;

    // Set player's movement speed
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

        playerMovement.Normalize();

        // This portion of the code rotates the player

        // Get current mouse position 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from playermodel to mouse
        Vector3 directionToMouse = mousePos - transform.position;

        // Calculate the angle to rotate the playermodel towards the mouse
        float rotationAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg - 90f;

        // Rotate player
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotationAngle));
    }

    private void FixedUpdate()
    {
        // Move player
        player_rb.velocity = playerMovement * movementSpeed;
    }
}
