using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Reference to main camera
    Camera cam;

    Rigidbody rb;

    [SerializeField]
    float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //  Get main camera
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        // forward and backward movement
        float forward = Input.GetAxisRaw("Vertical");
        // left and right movement
        float sideways = Input.GetAxisRaw("Horizontal");

        // Update the player's translation over time
        Vector3 newPos = new Vector3(sideways, 0, forward) * movementSpeed * Time.deltaTime;

        // Add the new position with the current position of the object the script is attached to
        rb.MovePosition(transform.position + newPos);

        // Prevents bouncing player stops drifting
        if (forward == 0 && sideways == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}
